using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class ProviderTypeConfigWebServiceVM : IMapFrom<ProviderTypeConfigWebService>
    {
        public string Indentity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(ProviderTypeConfigWebService), GetType());
        }
    }
}
