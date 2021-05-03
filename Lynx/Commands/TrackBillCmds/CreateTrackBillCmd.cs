using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Constants;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Commands.TrackBillCmds
{
    public class CreateTrackBillCmd : ITasq<CreateResult<TrackBillBO>>
    {
        public CreateTrackBillCmd(TrackBillBO entry, bool autoSave = false)
        {
            Entry = entry;
            AutoSave = autoSave;
        }

        public TrackBillBO Entry { get; }
        public bool AutoSave { get; }
    }

    //public class TrackBillVMValidator : AbstractValidator<TrackBillBO>
    //{
    //    public TrackBillVMValidator()
    //    {
    //        RuleFor(a => a.Bill).NotNull();
    //        RuleFor(a => a.BillProvider).NotNull();
    //        RuleFor(a => a.ProviderTypeConfigEmail)
    //            .NotNull()
    //            .When(a => a.BillProvider.ProviderTypeID == ProviderTypeConstants.Email);
    //        RuleFor(a => a.ProviderTypeConfigScheduler)
    //             .NotNull()
    //             .When(a => a.BillProvider.ProviderTypeID == ProviderTypeConstants.Scheduled);
    //        RuleFor(a => a.ProviderTypeConfigWebService)
    //            .NotNull()
    //            .When(a => a.BillProvider.ProviderTypeID == ProviderTypeConstants.WebService);
    //    }
    //}
}