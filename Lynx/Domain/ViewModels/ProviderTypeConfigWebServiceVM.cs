using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class ProviderTypeConfigWebServiceVM : IMapFrom<ProviderTypeConfigWebService>
    {
        public string Indentity { get; set; }

    }
}
