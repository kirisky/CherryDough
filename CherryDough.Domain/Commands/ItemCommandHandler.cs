using System.Threading;
using System.Threading.Tasks;
using CherryDough.Domain.Events;
using CherryDough.Domain.Interfaces;
using CherryDough.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;

namespace CherryDough.Domain.Commands
{
    public class ItemCommandHandler : CommandHandler,
        IRequestHandler<AddItemCommand, ValidationResult>,
        IRequestHandler<UpdateItemCommand, ValidationResult>,
        IRequestHandler<RemoveItemCommand, ValidationResult>
    {
        private readonly IShowcaseRepository _showcaseRepository;

        public ItemCommandHandler(IShowcaseRepository showcaseRepository)
        {
            _showcaseRepository = showcaseRepository;
        }
        
        public async Task<ValidationResult> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var item = new Item(request.Id, request.Name, request.Description, request.Category);

            if (await _showcaseRepository.GetByNameWithoutTracking(request.Name) != null)
            {
                AddError("The name of the item has been existed.");
                return ValidationResult;
            }

            item.AddDomainEvent(new AddedItemEvent(item.Id, item.Name, item.Description, item.Category));
            
            _showcaseRepository.Add(item);

            return await Commit(_showcaseRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            var item = await _showcaseRepository.GetByIdWithoutTracking(request.Id);
            if (item is null)
            {
                AddError("The showcase cannot update the item, because it does not exist in this showcase.");
                return ValidationResult;
            }

            var updatedItem = new Item(
                request.Id,
                request.Name ?? item.Name,
                request.Description ?? item.Description,
                request.Category ?? item.Category
            );
            
            updatedItem.AddDomainEvent(new UpdatedItemEvent(
                updatedItem.Id, updatedItem.Name, updatedItem.Description, updatedItem.Category
            ));

            _showcaseRepository.Update(updatedItem);
            
            return await Commit(_showcaseRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var item = await _showcaseRepository.GetById(request.Id);
            
            if (item is null)
            {
                AddError("The showcase cannot delete the item, because it does not exist in this showcase.");
                return ValidationResult;
            }
            
            item.AddDomainEvent(new RemovedItemEvent(request.Id));
            
            _showcaseRepository.Remove(item);
            
            return await Commit(_showcaseRepository.UnitOfWork);
        }
    }
}