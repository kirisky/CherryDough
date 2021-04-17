using CherryDough.Application.Interface;
using CherryDough.Application.Services;
using CherryDough.Domain.Commands;
using CherryDough.Domain.Interfaces;
using CherryDough.Infra.Bus;
using CherryDough.Infra.Data.Context;
using CherryDough.Infra.Data.Repository;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;

namespace CherryDough.Infra.IoC
{
    public static class ServicesInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            
            // Application
            services.AddScoped<IShowcaseAppService, ShowcaseAppService>();

            // Domain: commands
            services.AddScoped<IRequestHandler<AddItemCommand, ValidationResult>, ItemCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateItemCommand, ValidationResult>, ItemCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveItemCommand, ValidationResult>, ItemCommandHandler>();
            
            // Infra: data
            services.AddScoped<IShowcaseRepository, ShowcaseRepository>();
            services.AddScoped<CherryDoughContext>();
            
            
        }
    }
}