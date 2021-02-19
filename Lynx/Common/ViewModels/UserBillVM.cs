using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Common.ViewModels
{
    public class UserBillVM : UserBill, IMapFrom<UserBill>
    {
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserBill, UserBillVM>()
                .ForMember(a => a.ShortDesc, b => b.MapFrom(x => x.N_TrackBill.ShortDesc ?? x.N_TrackBill.N_Bill.ShortDesc))
                .ForMember(a => a.LongDesc, b => b.MapFrom(x => x.N_TrackBill.LongDesc ?? x.N_TrackBill.N_Bill.LongDesc));
        }
    }
}
