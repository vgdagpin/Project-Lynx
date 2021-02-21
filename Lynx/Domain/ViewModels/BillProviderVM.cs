using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class BillProviderVM : IMapFrom<BillProvider>
    {
        public short ID { get; set; }

        public short BillID { get; set; }
        public short ProviderTypeID { get; set; }

        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }

        public ProviderTypeVM ProviderType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BillProvider, BillProviderVM>()
                .ForMember(t => t.ProviderType, s => s.MapFrom(sprop => sprop.N_ProviderType));
        }
    }
}
