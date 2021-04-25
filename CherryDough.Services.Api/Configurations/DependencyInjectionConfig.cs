using System;
using CherryDough.Infra.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace CherryDough.Services.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            
            ServicesInjector.RegisterServices(services);
        }
    }
}