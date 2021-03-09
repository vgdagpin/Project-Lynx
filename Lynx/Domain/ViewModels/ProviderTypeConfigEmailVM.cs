using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class ProviderTypeConfigEmailVM : IMapFrom<ProviderTypeConfigEmail>
    {
        public string ClientEmailAddress { get; set; }
        public string ReceiverEmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(ProviderTypeConfigEmail), GetType());
        }
    }
}
