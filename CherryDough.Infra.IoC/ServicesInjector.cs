using CherryDough.Application.Interface;
using CherryDough.Application.Services;
using CherryDough.Domain.Interfaces;
using CherryDough.Infra.Data.Context;
using CherryDough.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CherryDough.Infra.IoC
{
    public static class ServicesInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IShowcaseAppService, ShowcaseAppService>();

            services.AddScoped<IShowcaseRepository, ShowcaseRepository>();
            services.AddScoped<CherryDoughContext>();
        }
    }
}