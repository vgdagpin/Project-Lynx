using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Lynx.Commands.TrackBillCmds;
using Lynx.Constants;
using Lynx.Domain.Entities;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Commands.TrackBillsCmds
{
    public class CreateTrackBillCmdHandler : TasqHandlerAsync<CreateTrackBillCmd, CreateResult<TrackBillVM>>
    {
        protected CreateTrackBillCmdHandler() { }

        public CreateTrackBillCmdHandler(ILynxDbContext dbContext, IAppUser appUser, IMapper mapper)
        {
            DbContext = dbContext;
            AppUser = appUser;
            Mapper = mapper;
        }

        protected ILynxDbContext DbContext { get; }
        protected IAppUser AppUser { get; }
        protected IMapper Mapper { get; }
        protected DbContext BaseDbContext => (DbContext)DbContext;
        protected DbSet<TrackBill> TrackBillDbSet => (DbSet<TrackBill>)DbContext.TrackBills;


        public override Task InitializeAsync(CreateTrackBillCmd tasq, CancellationToken cancellationToken)
        {
            var validationResult = tasq.Validator.ValidateUsing<TrackBillVMValidator>(tasq.Entry);

            if (!validationResult.IsValid)
            {
                throw new LynxException(new ValidationException(validationResult.Errors));
            }

            return base.InitializeAsync(tasq, cancellationToken);
        }

        public override Task<CreateResult<TrackBillVM>> RunAsync(CreateTrackBillCmd process, CancellationToken cancellationToken = default)
        {
            Task<CreateResult<TrackBillVM>> retVal = null;

            if (process.Entry == null)
            {
                return Task.FromResult(new CreateResult<TrackBillVM>
                {
                    IsCreated = false,
                    Error = new LynxObjectNotFoundException<TrackBillVM>("Parameter is null")
                });
            }

            var bill = DbContext.Bills.SingleOrDefault(a => a.ID == process.Entry.Bill.ID);
            var billProvider = DbContext.BillProviders
                .Include(a => a.N_ProviderType)
                .SingleOrDefault(a => a.BillID == process.Entry.Bill.ID 
                    && a.ProviderTypeID == process.Entry.BillProvider.ProviderTypeID);

            AssertObject.IsNotNull(bill);
            AssertObject.IsNotNull(billProvider);

            var newEntry = new TrackBill
            {
                BillID = process.Entry.Bill.ID,
                AccountNumber = process.Entry.AccountNumber,
                IsEnabled = true,
                LongDesc = process.Entry.LongDesc,
                ShortDesc = process.Entry.ShortDesc,
                ProviderTypeID = process.Entry.BillProvider.ProviderTypeID,
                UserID = AppUser.UserID
            };

            var entityEntry = TrackBillDbSet.Add(newEntry);

            if (newEntry.ProviderTypeID == ProviderTypeConstants.Scheduled)
            {

            }

            if (process.AutoSave)
            {


                return BaseDbContext.SaveChangesAsync(cancellationToken)
                    .ContinueWith(result =>
                    {
                        if (result.IsFaulted)
                        {
                            return new CreateResult<TrackBillVM>
                            {
                                IsCreated = false,
                                NewEntry = process.Entry,
                                Error = new LynxException(result.Exception)
                            };
                        }

                        return new CreateResult<TrackBillVM>
                        {
                            IsCreated = true,
                            NewEntry = Mapper.Map<TrackBillVM>(entityEntry.Entity)
                        };
                    });
            }
            else
            {
                var entry = Mapper.Map<TrackBillVM>(newEntry);

                entry.Bill = Mapper.Map<BillVM>(bill);
                entry.BillProvider = Mapper.Map<BillProviderVM>(billProvider);

                retVal = Task.FromResult(new CreateResult<TrackBillVM>
                {
                    NewEntry = entry
                });
            }

            return retVal;
        }
    }
}