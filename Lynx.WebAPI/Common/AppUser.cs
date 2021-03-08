using System;
using System.Security.Claims;
using AutoMapper;
using Lynx.Common.ViewModels;
using Lynx.Domain.Entities;
using Lynx.Interfaces;
using Lynx.WebAPI.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Lynx.WebAPI.Common
{
    public class AppUser : IAppUser
    {
        public UserVM Details { get; set; }
        public Guid UserID { get; set; }
        public Guid SessionUID { get; set; }

        public AppUser(IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor.HttpContext == null)
            {
                Details = null;
                return;
            }

            var _currentUser = contextAccessor.HttpContext.User;

            if (_currentUser == null)
            {
                Details = null;
                return;
            }

            ClaimsIdentity _claimsIdentity = _currentUser.Identity as ClaimsIdentity;
            Claim _claim = _claimsIdentity?.FindFirst(ClaimTypes.UserData);
            string _userData = _claim?.Value;

            if (string.IsNullOrWhiteSpace(_userData))
            {
                Details = null;
                return;
            }

            try
            {
                Details = JsonConvert.DeserializeObject<UserVM>(_userData);
            }
            catch
            {
                Details = null;
            }

            Claim _sid = _claimsIdentity?.FindFirst(ClaimTypes.Sid);
            string _sidData = _sid?.Value;

            if (Guid.TryParse(_sidData, out Guid _sessionUID))
            {
                SessionUID = _sessionUID;
            }

            if (contextAccessor.HttpContext.User != null)
            {
                Details = contextAccessor.HttpContext.User.GetUserData<UserVM>();
            }

            UserID = Details.ID;
        }

        public T GetUserDetail<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
