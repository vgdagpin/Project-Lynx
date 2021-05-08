using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Enums;

namespace Lynx.Domain.Models
{
    public class UserSessionBO
    {
        public Guid UserID { get; set; }
        public Guid SessionID { get; set; }
        public UserBO UserData { get; set; }

        public SessionStatus Status { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public bool IsExpired { get; set; }

        public string Remarks { get; set; }



        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<UserSession, UserSessionBO>()
        //        .ForMember(a => a.UserData, a => a.MapFrom(b => b.User))
        //        .ForMember(a => a.RefreshToken, a => a.MapFrom(b => b.Token)); //Session token as Refresh token in JWT
        //}
    }
}
