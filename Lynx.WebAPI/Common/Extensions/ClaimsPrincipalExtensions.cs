using System.Security.Claims;
using Lynx.Domain.Entities;
using Newtonsoft.Json;

namespace Lynx.WebAPI.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static T GetUserData<T>(this ClaimsPrincipal principal)
        {
            Claim _claimUserData = principal.FindFirst(ClaimTypes.UserData);

            return JsonConvert.DeserializeObject<T>(_claimUserData.Value);
        }
    }
}
