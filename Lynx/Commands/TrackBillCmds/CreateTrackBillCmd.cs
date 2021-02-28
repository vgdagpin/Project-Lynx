using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
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
        }
    }
}