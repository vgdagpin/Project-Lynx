using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Commands.UserLoginCmds;
using Lynx.Commands.UserSessionCmds;
using Lynx.Common.ViewModels;
using Lynx.Domain.Entities;
using Lynx.Domain.ViewModels;
using Lynx.Exceptions;
using Lynx.Interfaces;
using Lynx.Queries.UserQrs;
using Lynx.Queries.UserSessionQrs;
using Lynx.WebAPI.Common.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TasqR;

namespace Lynx.WebAPI.Common
{
    public class JwtSignInManager : IJwtSignInManager
    {
        private readonly ITasqR p_TasqR;
        private readonly IConfiguration p_Configuration;
        private readonly IMapper p_Mapper;
        private readonly IDateTime p_DateTime;
        private readonly IDataSecure p_DataSecure;

        public JwtSignInManager(ITasqR tasqR,
            IConfiguration configuration,
            IMapper mapper,
            IDateTime dateTime,
            IDataSecure dataSecure)
        {
            p_TasqR = tasqR;
            p_Configuration = configuration;
            p_Mapper = mapper;
            p_DateTime = dateTime;
            p_DataSecure = dataSecure;
        }

        public async Task<UserSessionVM> SignInAsync(string username, string password)
        {
            var validate = await p_TasqR.RunAsync(new ValidateUserLoginCmd(username, password));

            if (!validate.IsSuccess)
                throw validate.Error;


            var user = await p_TasqR.RunAsync(new GetUserDetailQr(username));

            var userVm = p_Mapper.Map<UserVM>(user);
            Guid newSession = Guid.NewGuid();

            //Random string
            var sessionToken = p_DataSecure.Protect(new byte[32]);

            var session = await p_TasqR.RunAsync(new CreateSessionCmd(sid: newSession,
                                                    username: username,
                                                    token: sessionToken,
                                                    expiration: p_DateTime.Now.AddDays(30)));

            var tokenObject = TokenBuilder(session);

            tokenObject.RefreshToken = session.Token; //The session token as refresh token in JWT
            tokenObject.UserData = userVm;

            return tokenObject;
        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserSessionVM> RefreshUserTokenAsync(string token, string refreshToken)
        {
            var user = ExtractUserFromToken(token);

            if(await ValidateRefreshToken(user: user, refreshToken: refreshToken))
            {
                var session = await p_TasqR.RunAsync(new GetSessionFromTokenQr(refreshToken));

                var newToken = TokenBuilder(session);
                newToken.RefreshToken = refreshToken;
                newToken.UserData = user;

                return newToken;
            }

            return null;
        }

        private UserSessionVM TokenBuilder(UserSession session)
        {
            var user = p_Mapper.Map<UserVM>(session.User);

            string userData = JsonConvert.SerializeObject(user);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString(), ClaimValueTypes.String),
                    new Claim(ClaimTypes.Name, user.FirstName, ClaimValueTypes.String),
                    new Claim(ClaimTypes.Sid, session.SessionID.ToString(), ClaimValueTypes.String),
                    new Claim(ClaimTypes.UserData, userData, ClaimValueTypes.String)
                };

            //Configuration
            var expireAt = p_DateTime.Now.AddDays(7); //7 Minutes

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(p_Configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = p_Configuration["Jwt:Issuer"],
                IssuedAt = p_DateTime.Now,
                NotBefore = p_DateTime.Now,
                Expires = expireAt,
                SigningCredentials
                    = new SigningCredentials(new SymmetricSecurityKey(tokenKey), JwtConstant.SecurityAlgo)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            return new UserSessionVM
            {
                Token = accessToken
            };
        }

        public UserVM ExtractUserFromToken(string accessToken)
        {
            var tokenValidationParam = Startup.TokenValidationParameters(p_Configuration);
            var tokenHandler = new JwtSecurityTokenHandler();

            #region Disabling this validation to check token validity
            tokenValidationParam.ValidateLifetime = false;
            #endregion

            var principle = tokenHandler.ValidateToken(token: accessToken,
                                validationParameters: tokenValidationParam,
                                out SecurityToken securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;


            if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(JwtConstant.SecurityAlgo))
            {
                Claim claimUserData = principle.FindFirst(ClaimTypes.UserData);

                return JsonConvert.DeserializeObject<UserVM>(claimUserData?.Value);
            }


            return null;
        }

        public Guid? ExtractSessionIDFromToken(string accessToken)
        {
            var tokenValidationParam = Startup.TokenValidationParameters(p_Configuration);
            var tokenHandler = new JwtSecurityTokenHandler();

            #region Disabling this validation to check token validity
            tokenValidationParam.ValidateLifetime = false;
            #endregion

            var principle = tokenHandler.ValidateToken(token: accessToken,
                                validationParameters: tokenValidationParam,
                                out SecurityToken securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;


            if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(JwtConstant.SecurityAlgo))
            {
                Claim claimUserData = principle.FindFirst(ClaimTypes.Sid);

                return Guid.Parse(claimUserData?.Value);
            }


            return null;
        }

        private async Task<bool> ValidateRefreshToken(UserVM user, string refreshToken)
        {
            var session = await p_TasqR.RunAsync(new GetSessionFromTokenQr(refreshToken));

            if (session == null)
                return false;

            if (user.ID == session.User.ID
                && !session.IsExpired)
                return true;

            return false;
        }
    }
}
