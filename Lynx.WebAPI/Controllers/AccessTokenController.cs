using System.Threading.Tasks;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasqR;

namespace Lynx.WebAPI.Controllers.Users
{
    [Route("[controller]")]
    public class AccessTokenController : LynxBaseController
    {
        private readonly IJwtSignInManager p_SignInManager;

        public AccessTokenController(ITasqR tasqR,
            IAppUser appUser,
            IJwtSignInManager signInManager)
            : base(tasqR, appUser)
        {
            p_SignInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("/AccessToken")]
        public async Task<IActionResult> Generate([FromBody] LoginRequestVM data)
        {
            var _token = await p_SignInManager.SignInAsync(data.Username, data.Password);

            if (_token == null)
            {
                return Unauthorized();
            }


            return Ok(_token);
        }


        [AllowAnonymous]
        [HttpPost("/AccessToken/Regenerate")]
        public async Task<IActionResult> Regenerate([FromBody] RefreshTokenRequestVM data)
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
