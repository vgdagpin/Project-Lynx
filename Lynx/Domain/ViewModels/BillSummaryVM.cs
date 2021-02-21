using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class BillSummaryVM : IMapFrom<Bill>
    {
        public short ID { get; set; }

        public string Code { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }

        public IEnumerable<BillProviderVM> Providers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Bill, BillSummaryVM>()
                .ForMember(t => t.Providers, s => s.MapFrom(sprop => sprop.N_BillProviders));
        }
    }
}
