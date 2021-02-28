using System;
using AutoMapper;
using Lynx.Common.ViewModels;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class UserSessionVM : IMapFrom<UserSession>
    {
        public UserVM UserData { get; set; }

        public string Token { get; set; }
        public string RefreshToken { get; set; }

        void Mapping(Profile profile)
        {
            profile.CreateMap<UserSession, UserSessionVM>()
                .ForMember(a => a.UserData, a => a.MapFrom(b => b.User))
                .ForMember(a => a.RefreshToken, a => a.MapFrom(b => b.Token)); //Session token as Refresh token in JWT
        }
    }
}
