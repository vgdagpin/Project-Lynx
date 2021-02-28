using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class ProviderTypeConfigEmailVM : IMapFrom<ProviderTypeConfigEmail>
    {
        public string ClientEmailAddress { get; set; }
        public string ReceiverEmailAddress { get; set; }
    }
}
