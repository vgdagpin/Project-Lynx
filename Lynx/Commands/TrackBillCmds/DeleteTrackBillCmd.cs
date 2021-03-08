using System;
using System.Collections.Generic;
using System.Text;
using TasqR;

namespace Lynx.Commands.TrackBillCmds
{
    public class DeleteTrackBillCmd : ITasq
    {
        public DeleteTrackBillCmd(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
