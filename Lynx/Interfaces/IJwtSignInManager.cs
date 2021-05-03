using System.Threading.Tasks;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;

namespace Lynx.Interfaces
{
    public interface IJwtSignInManager : ISignInManager
    {
        Task<UserSessionBO> RefreshUserTokenAsync(string token, string refreshToken);

        Task<bool> JwtSignOut(string token);
    }
}
