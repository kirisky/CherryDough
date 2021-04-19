using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CherryDough.Application.EventSourcedNormalizers;
using CherryDough.Application.ViewModels;
using FluentValidation.Results;

namespace CherryDough.Application.Interface
{
    public interface IShowcaseAppService : IDisposable
    {
        Task<IEnumerable<ShowcaseViewModel>> GetAll();
        Task<IEnumerable<ShowcaseViewModel>> GetById(Guid id);
        Task<ValidationResult> AddItemAsync(ShowcaseViewModel showcaseViewModel);
        Task<ValidationResult> UpdateItemAsync(ShowcaseViewModel showcaseViewModel);
        Task<ValidationResult> RemoveItemAsync(Guid id);
        Task<IList<ItemHistoryData>> GetAllHistory(Guid id);
    }
}