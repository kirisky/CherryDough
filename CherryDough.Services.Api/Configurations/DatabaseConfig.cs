using System;
using CherryDough.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CherryDough.Services.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<CherryDoughContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("SqliteLocal")));

            services.AddDbContext<StoredEventContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("SqliteEventLocal")));
        }
    }
}