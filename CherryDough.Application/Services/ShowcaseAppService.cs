using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CherryDough.Application.Interface;
using CherryDough.Application.ViewModels;
using CherryDough.Domain.Commands;
using CherryDough.Domain.Interfaces;
using FluentValidation.Results;
using NetDevPack.Mediator;

namespace CherryDough.Application.Services
{
    public class ShowcaseAppService : IShowcaseAppService
    {
        private readonly IMapper _mapper;
        private readonly IShowcaseRepository _showcaseRepository;
        private readonly IMediatorHandler _meditor;

        public ShowcaseAppService(IMapper mapper, IShowcaseRepository showcaseRepository, IMediatorHandler meditor)
        {
            _mapper = mapper;
            _showcaseRepository = showcaseRepository;
            _meditor = meditor;
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
            var addItemCommand = _mapper.Map<AddItemCommand>(showcaseViewModel);
            return await _meditor.SendCommand(addItemCommand);
        }

        public async Task<ValidationResult> UpdateItemAsync(ShowcaseViewModel showcaseViewModel)
        {
            var updateItemCommand = _mapper.Map<UpdateItemCommand>(showcaseViewModel);
            return await _meditor.SendCommand(updateItemCommand);
        }

        public async Task<ValidationResult> RemoveItemAsync(Guid id)
        {
            var removeItemCommand = new RemoveItemCommand(id);
            return await _meditor.SendCommand(removeItemCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}