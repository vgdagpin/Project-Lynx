using System;
using AutoMapper;
using Lynx.Common.ViewModels;
using Lynx.Domain.Entities;
using Lynx.Enums;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class UserSessionVM : IMapFrom<UserSession>
    {
        public Guid SessionID { get; set; }
        public UserVM UserData { get; set; }

        public SessionStatus Status { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public bool IsExpired { get; set; }

        public string Remarks { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserSession, UserSessionVM>()
                .ForMember(a => a.UserData, a => a.MapFrom(b => b.User))
                .ForMember(a => a.RefreshToken, a => a.MapFrom(b => b.Token)); //Session token as Refresh token in JWT
        }
    }
}
