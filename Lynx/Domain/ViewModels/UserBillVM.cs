using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AutoMapper;
using Lynx.Domain.Entities;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;

namespace Lynx.Common.ViewModels
{
    public class UserBillVM : IMapFrom<UserBill>
    {
        public Guid TrackBillID { get; set; }
        public Guid UserID { get; set; }

        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public BillPaymentStatus Status { get; set; }

        public TrackBillVM TrackBill { get; set; }
        public UserVM User { get; set; }




        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserBill, UserBillVM>()
                .ForMember(a => a.ShortDesc, b => b.MapFrom(x => x.N_TrackBill.ShortDesc ?? x.N_TrackBill.N_Bill.ShortDesc))
                .ForMember(a => a.LongDesc, b => b.MapFrom(x => x.N_TrackBill.LongDesc ?? x.N_TrackBill.N_Bill.LongDesc))
                .ForMember(a => a.TrackBill, b=> b.MapFrom(x => x.N_TrackBill));
        }


        public static UserBillVM Null()
        {
            return null;
        }
    }
}
