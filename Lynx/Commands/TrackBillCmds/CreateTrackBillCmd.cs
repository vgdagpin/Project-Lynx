using System;
using System.Collections.Generic;
using System.Text;
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
}