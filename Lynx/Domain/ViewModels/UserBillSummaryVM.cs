using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class UserBillSummaryVM : IMapFrom<UserBill>
    {
        public Guid ID { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserBill, UserBillSummaryVM>()
                .ForMember(t => t.ShortDesc, s => s.MapFrom(sprop => sprop.N_TrackBill.ShortDesc ?? sprop.N_TrackBill.N_Bill.ShortDesc))
                .ForMember(t => t.LongDesc, s => s.MapFrom(sprop => sprop.N_TrackBill.LongDesc ?? sprop.N_TrackBill.N_Bill.LongDesc));
        }
    }
}
