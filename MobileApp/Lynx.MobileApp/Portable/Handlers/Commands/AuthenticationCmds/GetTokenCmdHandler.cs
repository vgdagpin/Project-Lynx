using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.AuthenticationCmds;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using TasqR;

namespace Lynx.MobileApp.Portable.Handlers.Commands.AuthenticationCmds
{
    public class GetTokenCmdHandler : TasqHandlerAsync<GetTokenCmd, UserSessionVM>
    {
        private readonly ILynxDbContext p_DbContext;

        public GetTokenCmdHandler(ILynxDbContext dbContext)
        {
            p_DbContext = dbContext;
        }

        public override Task<UserSessionVM> RunAsync(GetTokenCmd request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new UserSessionVM
            {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDIiLCJ1bmlxdWVfbmFtZSI6IlZpbmNlbnQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIxNDVkM2EzYS1iZDMzLTQ2MWItYWE0YS1kM2RmYzEwNjFiMzkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoie1wiSURcIjpcIjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMlwiLFwiVXNlcm5hbWVcIjpcInZnZGFncGluXCIsXCJGaXJzdE5hbWVcIjpcIlZpbmNlbnRcIixcIkxhc3ROYW1lXCI6XCJEYWdwaW5cIn0iLCJuYmYiOjE2MTQ0ODk1MDIsImV4cCI6MTYxNTA5NDMwMiwiaWF0IjoxNjE0NDg5NTAyLCJpc3MiOiJMWU5YLkNPTSJ9._jIbFJHZIq0s7KVf9CrxPPDxtUQJaaKlT6FLnuFtJfY"
            });
        }
    }
}
