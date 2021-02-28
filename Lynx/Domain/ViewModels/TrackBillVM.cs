using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FluentValidation;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class TrackBillVM : IMapFrom<TrackBill>
    {
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string AccountNumber { get; set; }
        public bool IsEnabled { get; set; }
        public BillVM Bill { get; set; }
        public BillProviderVM BillProvider { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TrackBill, TrackBillVM>()
                .ForMember(t => t.ShortDesc, s => s.MapFrom(sprop => sprop.ShortDesc ?? $"{sprop.N_Bill.ShortDesc} - {sprop.N_ProviderType.ShortDesc}"))
                .ForMember(t => t.LongDesc, s => s.MapFrom(sprop => sprop.LongDesc ?? $"{sprop.N_Bill.LongDesc} - {sprop.N_ProviderType.LongDesc}"))
                .ForMember(t => t.Bill, s => s.MapFrom(sprop => sprop.N_Bill));
        }
    }    
}
