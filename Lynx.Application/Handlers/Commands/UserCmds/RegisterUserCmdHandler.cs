using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Application.Common.Extensions;
using Lynx.Commands.UserCmds;
using Lynx.Common.ViewModels;
using Lynx.Domain.Entities;
using Lynx.Domain.Models;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Commands.UserCmds
{
    public class RegisterUserCmdHandler : TasqHandlerAsync<RegisterUserCmd, RegisterResultBO>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IPasswordHasher p_PasswordHasher;
        private readonly IMapper p_Mapper;

        public RegisterUserCmdHandler(ILynxDbContext dbContext, IPasswordHasher passwordHasher, IMapper mapper)
        {
            p_DbContext = dbContext;
            p_PasswordHasher = passwordHasher;
            p_Mapper = mapper;
        }

        public async override Task<RegisterResultBO> RunAsync(RegisterUserCmd process, CancellationToken cancellationToken = default)
        {
            if (await p_DbContext.UserLogins.AnyAsync(a => a.Username == process.Email))
            {
                return RegisterResultBO.Failed("Username already exists!");
            }

            Guid newUserID = Guid.NewGuid();
            byte[] newSalt = p_PasswordHasher.GenerateSalt();

            User newUser = new User
            {
                ID = newUserID,
                FirstName = process.FirstName,
                LastName = process.LastName,
                UserLogin = new UserLogin
                {
                    ID = newUserID,
                    Salt = newSalt,
                    Username = process.Email,
                    Password = p_PasswordHasher.HashPassword(newSalt, process.Password)
                }
            };

            p_DbContext.Users.Add(newUser);

            return RegisterResultBO.Ready(p_Mapper.Map<UserBO>(newUser));
        }
    }
}
