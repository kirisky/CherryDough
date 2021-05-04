using CherryDough.Infra.Identity.Configurations;
using CherryDough.Infra.Identity.Configurations.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace CherryDough.Services.Api.Configurations
{
    public static class ApiIdentityConfig
    {
        public static void AddApiIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityEntityFrameworkContextConfiguration(options =>
                options.UseSqlite(configuration.GetConnectionString("SqliteIdentityLocal"),
                    b => b.MigrationsAssembly("CherryDough.Infra.Identity")));

            services.AddIdentityConfiguration();
            services.AddJwtConfiguration(configuration, "AppSettings");
        }
    }
}