using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Common.ViewModels
{
    public class UserVM : IMapFrom<User>
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserVM>()
                .ForMember(a => a.Username, a => a.MapFrom(b => b.UserLogin.Username));
        }
    }

}
