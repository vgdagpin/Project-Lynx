using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class TrackBillSummaryVM : IMapFrom<TrackBill>
    {
        public Guid ID { get; set; }

        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string AccountNumber { get; set; }
        public bool IsEnabled { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TrackBill, TrackBillSummaryVM>()
                .ForMember(t => t.ShortDesc, s => s.MapFrom(sprop => sprop.ShortDesc ?? $"{sprop.N_Bill.ShortDesc} - {sprop.N_ProviderType.ShortDesc}"))
                .ForMember(t => t.LongDesc, s => s.MapFrom(sprop => sprop.LongDesc ?? $"{sprop.N_Bill.LongDesc} - {sprop.N_ProviderType.LongDesc}"));
        }
    }
}