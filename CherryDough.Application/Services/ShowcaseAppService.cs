using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CherryDough.Application.Interface;
using CherryDough.Application.ViewModels;
using CherryDough.Domain.Interfaces;
using CherryDough.Domain.Models;
using CherryDough.Domain.Validations;
using FluentValidation.Results;

namespace CherryDough.Application.Services
{
    public class ShowcaseAppService : IShowcaseAppService
    {
        private readonly IMapper _mapper;
        private readonly IShowcaseRepository _showcaseRepository;

        public ShowcaseAppService(IMapper mapper, IShowcaseRepository showcaseRepository)
        {
            _mapper = mapper;
            _showcaseRepository = showcaseRepository;
        }
        
        public async Task<IEnumerable<ShowcaseViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ShowcaseViewModel>>(await _showcaseRepository.GetAll());
        }

        public async Task<IEnumerable<ShowcaseViewModel>> GetById(Guid id)
        {
            return _mapper.Map<IEnumerable<ShowcaseViewModel>>(await _showcaseRepository.GetById(id));
        }

        public async Task<ValidationResult> AddItemAsync(ShowcaseViewModel showcaseViewModel)
        {
            var item = _mapper.Map<Item>(showcaseViewModel);
            
            var validationResult = await new AddItemValidation().ValidateAsync(item);
            if (!validationResult.IsValid) return validationResult;
            
            if (!await _showcaseRepository.Add(item))
                validationResult.Errors
                    .Add(new ValidationFailure(string.Empty, "Add new item failed"));

            return validationResult;
        }

        public async Task<ValidationResult> UpdateItemAsync(ShowcaseViewModel showcaseViewModel)
        {
            var item = _mapper.Map<Item>(showcaseViewModel);
            var validationResult = await new UpdateItemValidation().ValidateAsync(item);
            if (!validationResult.IsValid) return validationResult;

            if (!await _showcaseRepository.Update(item))
                validationResult.Errors
                    .Add(new ValidationFailure(string.Empty, "Update item failed"));

            return validationResult;
        }

        public async Task<ValidationResult> RemoveItemAsync(ShowcaseViewModel showcaseViewModel)
        {
            var item = _mapper.Map<Item>(showcaseViewModel);
            var validationResult = await new RemoveItemValidation().ValidateAsync(item);
            if (!validationResult.IsValid) return validationResult;
            
            if (!await _showcaseRepository.Remove(item))
                validationResult.Errors
                    .Add(new ValidationFailure(string.Empty, "Remove the item failed"));

            return validationResult;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}