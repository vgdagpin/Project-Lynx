using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Lynx.Constants;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Commands.TrackBillCmds
{
    public class CreateTrackBillCmd : ITasq<CreateResult<TrackBillVM>>
    {
        public CreateTrackBillCmd(TrackBillVM entry, bool autoSave = false)
        {
            Entry = entry;
            AutoSave = autoSave;
        }

        public TrackBillVM Entry { get; }
        public bool AutoSave { get; }
        public Validator<TrackBillVM> Validator { get; } = new Validator<TrackBillVM>();
    }

    public class TrackBillVMValidator : AbstractValidator<TrackBillVM>
    {
        public TrackBillVMValidator()
        {
            RuleFor(a => a.Bill).NotNull();
            RuleFor(a => a.BillProvider).NotNull();
            RuleFor(a => a.ProviderTypeConfigEmail)
                .NotNull()
                .When(a => a.BillProvider.ProviderTypeID == ProviderTypeConstants.Email);
            RuleFor(a => a.ProviderTypeConfigScheduler)
                 .NotNull()
                 .When(a => a.BillProvider.ProviderTypeID == ProviderTypeConstants.Scheduled);
            RuleFor(a => a.ProviderTypeConfigWebService)
                .NotNull()
                .When(a => a.BillProvider.ProviderTypeID == ProviderTypeConstants.WebService);
        }
    }
}