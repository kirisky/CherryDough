using System;
using CherryDough.Infra.Identity.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CherryDough.Infra.Identity.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityEntityFrameworkContextConfiguration(
            this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            if (services == null) throw new ArgumentException(nameof(services));
            if (options == null) throw new ArgumentException(nameof(options));
            return services.AddDbContext<CustomIdentityDbContext>(options);
        }

        public static IdentityBuilder AddIdentityConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentException(nameof(services));

            return services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CustomIdentityDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}