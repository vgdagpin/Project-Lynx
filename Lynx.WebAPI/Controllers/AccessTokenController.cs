using System.Linq;
using System.Threading.Tasks;
using Lynx.Commands.UserSessionCmds;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.WebAPI.Common;
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

        [HttpPost("/AccessToken/VerifyValidity")]
        public Task<TokenVerificationResult> VerifyValidity([FromBody]string firebaseToken)
        {
            return Task.FromResult(new TokenVerificationResult
            {
                TokenStatus = TokenStatus.Active
            });

            string authorization = Request.Headers["Authorization"];
            string token = authorization?.Split(' ').LastOrDefault();

            var tokenMgr = (JwtSignInManager)p_SignInManager;
            var sessionID = tokenMgr.ExtractSessionIDFromToken(token);

            return TasqR.RunAsync(new VerifyTokenValidityCmd(sessionID.GetValueOrDefault()));
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
