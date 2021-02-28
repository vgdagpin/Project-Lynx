using System.Threading.Tasks;
using Lynx.Interfaces;
using Lynx.WebAPI.Controllers.User.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasqR;

namespace Lynx.WebAPI.Controllers.Users
{
    public class UserController : LynxBaseController
    {
        private readonly IJwtSignInManager p_SignInManager;

        public UserController(ITasqR tasqR,
            IAppUser appUser,
            IJwtSignInManager signInManager)
            : base(tasqR, appUser)
        {
            p_SignInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Login([FromBody] LoginRequest data)
        {
            var _token = await p_SignInManager.SignInAsync(data.EmailOrUsername, data.Password);

            if (_token == null)
            {
                return Unauthorized();
            }


            return Ok(_token);
        }


        [AllowAnonymous]
        [HttpPost("Re-Authenticate")]
        public async Task<IActionResult> ReLogin([FromBody] RefreshTokenRequest data)
        {
            var token = await p_SignInManager.RefreshUserTokenAsync(data.AccessToken, data.RefreshToken);

            if (token == null)
            {
                return Challenge();
            }

            return Ok(token);
        }
    }
}
