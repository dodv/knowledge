using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Knowledge.Models.Database;
using System;

namespace Knowledge.Migrations
{
    public class BaseAppDbContextDesignTimeFactory : IDesignTimeDbContextFactory<BaseAppDbContext>
    {
        private const string ConfigSettingPlatformContext = "ConnectionStrings:PlatformContext";

        public BaseAppDbContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddEnvironmentVariables("KNOWLEDGE_ ");
            var builder = new DbContextOptionsBuilder<BaseAppDbContext>();
            var config = configBuilder.Build();

            var connectionString = @"Server=localhost,8081;Database=LocalDevDb;User Id=sa;Password=<YourStrong!Passw0rd>";
                //config[ConfigSettingPlatformContext] ?? "use env var KNOWLEDGE__ConnectionStrings__PlatformContext";
            builder
                .UseSqlServer(connectionString, o => o.MigrationsAssembly("Knowledge.Migrations"));
            return new BaseAppDbContext(builder.Options);
        }
    }
}
