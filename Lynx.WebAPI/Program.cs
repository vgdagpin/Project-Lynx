using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Lynx.WebAPI
{
    public class Program
    {
        //static int port = 53795;
        //public static bool InsideIIS => Environment.GetEnvironmentVariable("APP_POOL_ID") != null;

        public static void Main(string[] args)
        {
            //Console.WriteLine("Is inside IIS: {0}", InsideIIS);

            //if (!InsideIIS)
            //{
            //    string localIP = GetLocalIPAddress();

            //    if (!string.IsNullOrWhiteSpace(localIP))
            //    {
            //        string url = $"http://{localIP}:{port}";

            //        Console.Title = "Lynx Web API";

            //        Console.WriteLine(url);
            //        Console.WriteLine();
            //    }
            //}            

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //if (!InsideIIS)
                    //{
                    //    webBuilder.UseUrls($"http://*:{port}");
                    //}

                    webBuilder.UseStartup<Startup>();
                });

        //public static string GetLocalIPAddress()
        //{
        //    try
        //    {
        //        var host = Dns.GetHostEntry(Dns.GetHostName());
        //        foreach (var ip in host.AddressList)
        //        {
        //            if (ip.AddressFamily == AddressFamily.InterNetwork)
        //            {
        //                return ip.ToString();
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("No network adapters with an IPv4 address in the system!");
        //    }

        //    return null;
        //}
    }
}
