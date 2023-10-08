using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Knowledge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                    {
                        var env = context.HostingEnvironment;

                        // adding appsettings explicitly here and using the directory to ensure this works
                        // with extending Apps like SafetyManager and SafetyManager.Api.Dev without having to
                        // move appsettings to a shared folder at the solution level or using an in-memory config

                        var apiDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

                        config.AddJsonFile(Path.Combine(apiDir, "appsettings.json"), optional: true, reloadOnChange: true)
                            .AddJsonFile(Path.Combine(apiDir, $"appsettings.{env.EnvironmentName}.json"), optional: true, reloadOnChange: true);

                        config.AddEnvironmentVariables("KNOWLEDGE_");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
