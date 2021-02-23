using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Lynx.Commands.TrackBillCmds;
using Lynx.Domain.Entities;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Commands.TrackBillsCmds
{
    public class CreateTrackBillCmdHandler : TasqHandlerAsync<CreateTrackBillCmd, CreateResult<TrackBillVM>>
    {
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


        public override Task InitializeAsync(CreateTrackBillCmd tasq, CancellationToken cancellationToken = default)
        {
            var validationResult = tasq.Validator.ValidateUsing<TrackBillVMValidator>(tasq.Entry);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return Task.FromResult(0);
        }

        public override Task<CreateResult<TrackBillVM>> RunAsync(CreateTrackBillCmd process, CancellationToken cancellationToken = default)
        {
            Task<CreateResult<TrackBillVM>> retVal = null;

            if (process.Entry == null)
            {
                return Task.FromResult(new CreateResult<TrackBillVM>
                {
                    IsCreated = false,
                    Error = new LynxException("Parameter is null")
                });
            }

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
                            NewEntry = Mapper.Map<TrackBillVM>(process.Entry)
                        };
                    });
            }

            return retVal;
        }
    }
}