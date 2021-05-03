using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Commands.TrackBillCmds;
using Lynx.Constants;
using Lynx.Domain.Entities;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Commands.TrackBillsCmds
{
    public class CreateTrackBillCmdHandler : TasqHandlerAsync<CreateTrackBillCmd, CreateResult<TrackBillBO>>
    {
        private readonly IDateTime p_DateTime;

        protected CreateTrackBillCmdHandler() { }

        public CreateTrackBillCmdHandler(ILynxDbContext dbContext, IAppUser appUser, IMapper mapper, IDateTime dateTime)
        {
            DbContext = dbContext;
            AppUser = appUser;
            Mapper = mapper;
            p_DateTime = dateTime;
        }

        protected ILynxDbContext DbContext { get; }
        protected IAppUser AppUser { get; }
        protected IMapper Mapper { get; }
        protected DbContext BaseDbContext => (DbContext)DbContext;
        protected DbSet<TrackBill> TrackBillDbSet => (DbSet<TrackBill>)DbContext.TrackBills;


        public override Task InitializeAsync(CreateTrackBillCmd tasq, CancellationToken cancellationToken)
        {
            //var validationResult = tasq.Validator.ValidateUsing<TrackBillBOValidator>(tasq.Entry);

            //if (!validationResult.IsValid)
            //{
            //    throw new LynxException(new ValidationException(validationResult.Errors));
            //}

            //return base.InitializeAsync(tasq, cancellationToken);
            throw new NotImplementedException();
        }

        public override Task<CreateResult<TrackBillBO>> RunAsync(CreateTrackBillCmd request, CancellationToken cancellationToken = default)
        {
            Task<CreateResult<TrackBillBO>> retVal = null;

            if (request.Entry == null)
            {
                return Task.FromResult(new CreateResult<TrackBillBO>
                {
                    IsCreated = false,
                    Error = new LynxObjectNotFoundException<TrackBillBO>("Parameter is null")
                });
            }

            var bill = DbContext.Bills.SingleOrDefault(a => a.ID == request.Entry.Bill.ID);
            var billProvider = DbContext.BillProviders
                .Include(a => a.N_ProviderType)
                .SingleOrDefault(a => a.BillID == request.Entry.Bill.ID
                    && a.ProviderTypeID == request.Entry.BillProvider.ProviderTypeID);

            AssertObject.IsNotNull(bill);
            AssertObject.IsNotNull(billProvider);

            var newEntry = new TrackBill
            {
                BillID = request.Entry.Bill.ID,
                AccountNumber = request.Entry.AccountNumber,
                IsEnabled = true,
                LongDesc = request.Entry.LongDesc,
                ShortDesc = request.Entry.ShortDesc,
                ProviderTypeID = request.Entry.BillProvider.ProviderTypeID,
                UserID = AppUser.UserID
            };

            var entityEntry = TrackBillDbSet.Add(newEntry);

            if (newEntry.ProviderTypeID == ProviderTypeConstants.Scheduled)
            {
                newEntry.N_ProviderTypeConfigScheduler = new ProviderTypeConfigScheduler
                {
                    LongDesc = request.Entry.ProviderTypeConfigScheduler.LongDesc,
                    Amount = request.Entry.ProviderTypeConfigScheduler.Amount,
                    DayFrequency = request.Entry.ProviderTypeConfigScheduler.DayFrequency,
                    StartDate = request.Entry.ProviderTypeConfigScheduler.StartDate,
                    EndDate = request.Entry.ProviderTypeConfigScheduler.EndDate,
                    Frequency = request.Entry.ProviderTypeConfigScheduler.Frequency,
                    ShortDesc = request.Entry.ProviderTypeConfigScheduler.ShortDesc,
                    SkipTimes = request.Entry.ProviderTypeConfigScheduler.SkipTimes
                };

                if (newEntry.N_ProviderTypeConfigScheduler.DayFrequency != null
                    && newEntry.N_ProviderTypeConfigScheduler.DayFrequency > 0)
                {
                    var dates = GetDates
                        (
                            newEntry.N_ProviderTypeConfigScheduler.DayFrequency.Value,
                            newEntry.N_ProviderTypeConfigScheduler.StartDate,
                            newEntry.N_ProviderTypeConfigScheduler.EndDate
                        );

                    foreach (var date in dates)
                    {
                        newEntry.N_ProviderTypeConfigScheduler.N_ScheduleEntries.Add(new SchedulerEntry
                        {
                            Amount = request.Entry.ProviderTypeConfigScheduler.Amount,
                            DueDate = date
                        });

                        newEntry.N_UserBills.Add(new UserBill
                        {
                            Amount = request.Entry.ProviderTypeConfigScheduler.Amount,
                            DueDate = date,
                            Status = BillPaymentStatus.Pending
                        });
                    }
                }
            }

            if (request.AutoSave)
            {
                return BaseDbContext.SaveChangesAsync(cancellationToken)
                    .ContinueWith(result =>
                    {
                        if (result.IsFaulted)
                        {
                            return new CreateResult<TrackBillBO>
                            {
                                IsCreated = false,
                                NewEntry = request.Entry,
                                Error = new LynxException(result.Exception)
                            };
                        }

                        return new CreateResult<TrackBillBO>
                        {
                            IsCreated = true,
                            NewEntry = Mapper.Map<TrackBillBO>(entityEntry.Entity)
                        };
                    });
            }
            else
            {
                var entry = Mapper.Map<TrackBillBO>(newEntry);

                entry.Bill = Mapper.Map<BillBO>(bill);
                entry.BillProvider = Mapper.Map<BillProviderBO>(billProvider);

                retVal = Task.FromResult(new CreateResult<TrackBillBO>
                {
                    NewEntry = entry
                });
            }

            return retVal;
        }

        protected virtual IEnumerable<DateTime> GetDates(int dayFrequency, DateTime start, DateTime? end)
        {
            List<DateTime> dt = new List<DateTime>();

            DateTime schedStart = Utility.Max(start, p_DateTime.Now);
            DateTime schedEnd = end ?? schedStart.AddYears(1);

            int startMonth = schedStart.Month;
            int startYear = schedStart.Year;

            DateTime scanStart = new DateTime(startYear, startMonth, 1);

            while (scanStart < schedEnd)
            {
                if (scanStart.Day == dayFrequency)
                {
                    dt.Add(scanStart);

                    var nextM = scanStart.AddMonths(1);

                    scanStart = new DateTime(nextM.Year, nextM.Month, 1);
                }
                else
                {
                    if (scanStart.AddDays(1).Month > scanStart.Month) // see if tomorrow is another month then use this day)
                    {
                        dt.Add(scanStart);

                        var nextM = scanStart.AddMonths(1);

                        scanStart = new DateTime(nextM.Year, nextM.Month, 1);
                    }
                    else
                    {
                        scanStart = scanStart.AddDays(1);
                    }
                }
            }


            return dt;
        }
    }
}