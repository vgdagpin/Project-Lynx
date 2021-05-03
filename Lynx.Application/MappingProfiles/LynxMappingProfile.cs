using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lynx.Domain.Entities;
using Lynx.Domain.Models;

namespace Lynx.Application.MappingProfiles
{
    public class LynxMappingProfile : Profile
    {
        public LynxMappingProfile()
        {
            RecognizePrefixes("N_");

            CreateMap<User, UserBO>();
            CreateMap<UserBO, User>();

            CreateMap<UserSession, UserSessionBO>()
                .ForMember(a => a.UserData, a => a.MapFrom(b => b.User))
                .ForMember(a => a.RefreshToken, a => a.MapFrom(b => b.Token)); //Session token as Refresh token in JWT;

            CreateMap<UserSessionBO, UserSession>();

            CreateMap<UserBill, UserBillSummaryBO>()
                .ForMember(t => t.ShortDesc, s => s.MapFrom(sprop => sprop.N_TrackBill.ShortDesc ?? sprop.N_TrackBill.N_Bill.ShortDesc))
                .ForMember(t => t.LongDesc, s => s.MapFrom(sprop => sprop.N_TrackBill.LongDesc ?? sprop.N_TrackBill.N_Bill.LongDesc));
            CreateMap<UserBill, UserBillBO>()
                .ForMember(a => a.ShortDesc, b => b.MapFrom(x => x.N_TrackBill.ShortDesc ?? x.N_TrackBill.N_Bill.ShortDesc))
                .ForMember(a => a.LongDesc, b => b.MapFrom(x => x.N_TrackBill.LongDesc ?? x.N_TrackBill.N_Bill.LongDesc))
                .ForMember(a => a.TrackBill, b => b.MapFrom(x => x.N_TrackBill));

            CreateMap<TrackBill, TrackBillBO>()
                .ForMember(t => t.ShortDesc, s => s.MapFrom(sprop => sprop.ShortDesc ?? $"{sprop.N_Bill.ShortDesc} - {sprop.N_ProviderType.ShortDesc}"))
                .ForMember(t => t.LongDesc, s => s.MapFrom(sprop => sprop.LongDesc ?? $"{sprop.N_Bill.LongDesc} - {sprop.N_ProviderType.LongDesc}"))
                .ForMember(t => t.Bill, s => s.MapFrom(sprop => sprop.N_Bill))
                .ForMember(t => t.ProviderTypeConfigEmail, s => s.MapFrom(sprop => sprop.N_ProviderTypeConfigEmail))
                .ForMember(t => t.ProviderTypeConfigScheduler, s => s.MapFrom(sprop => sprop.N_ProviderTypeConfigScheduler))
                .ForMember(t => t.ProviderTypeConfigWebService, s => s.MapFrom(sprop => sprop.N_ProviderTypeConfigWebService));
            CreateMap<TrackBill, TrackBillSummaryBO>()
                .ForMember(t => t.ShortDesc, s => s.MapFrom(sprop => sprop.ShortDesc ?? $"{sprop.N_Bill.ShortDesc} - {sprop.N_ProviderType.ShortDesc}"))
                .ForMember(t => t.LongDesc, s => s.MapFrom(sprop => sprop.LongDesc ?? $"{sprop.N_Bill.LongDesc} - {sprop.N_ProviderType.LongDesc}"))
                .ForMember(t => t.ProviderType, s => s.MapFrom(sprop => sprop.N_ProviderType));

            CreateMap<ProviderType, ProviderTypeBO>();

            CreateMap<Bill, BillBO>();
            CreateMap<Bill, BillSummaryBO>();
        }
    }
}
