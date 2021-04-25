using CherryDough.Application.Interface;
using CherryDough.Application.Services;
using CherryDough.Doamin.Core.Events;
using CherryDough.Domain.Commands;
using CherryDough.Domain.Events;
using CherryDough.Domain.Interfaces;
using CherryDough.Infra.Bus;
using CherryDough.Infra.Data.Context;
using CherryDough.Infra.Data.EventSourcing;
using CherryDough.Infra.Data.Repository;
using CherryDough.Infra.Data.Repository.EventSourcing;
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
            // Mediator (Event bus)
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            
            // Application
            services.AddScoped<IShowcaseAppService, ShowcaseAppService>();
            
            // Domain: events
            services.AddScoped<INotificationHandler<AddedItemEvent>, ItemEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedItemEvent>, ItemEventHandler>();
            services.AddScoped<INotificationHandler<RemovedItemEvent>, ItemEventHandler>();

            // Domain: commands
            services.AddScoped<IRequestHandler<AddItemCommand, ValidationResult>, ItemCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateItemCommand, ValidationResult>, ItemCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveItemCommand, ValidationResult>, ItemCommandHandler>();
            
            // Infra: data
            services.AddScoped<IShowcaseRepository, ShowcaseRepository>();
            services.AddScoped<CherryDoughContext>();
            
            // Infra: EventSourcing
            services.AddScoped<IStoredEventRepository, StoredStoredEventRepository>();
            services.AddScoped<StoredEventContext>();
            services.AddScoped<IEventStore, EventStore>();


        }
    }
}