using System.Threading.Tasks;
using Lynx.Domain.ViewModels;

namespace Lynx.Interfaces
{
    public interface IJwtSignInManager : ISignInManager
    {
        Task<UserSessionVM> RefreshUserTokenAsync(string token, string refreshToken);

        Task<bool> JwtSignOut(string token);
    }
}
