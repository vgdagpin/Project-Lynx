using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TasqR;

namespace Lynx.WebAPI.Controllers
{
    [Route("[controller]")]
    public class TestController : LynxBaseController
    {
        public TestController(ITasqR tasqR, IAppUser appUser) : base(tasqR, appUser)
        {
        }

        [HttpGet]
        public Task<string> Get(CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                for (int i = 20; i > 0; i--)
                {
                    Thread.Sleep(1000);

                    Console.WriteLine($"Count: {i}");

                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancellation requested");

                        break;
                    }
                }


                Console.WriteLine("Request Completed");

                return "hi";

            }, cancellationToken);
        }
    }
}
