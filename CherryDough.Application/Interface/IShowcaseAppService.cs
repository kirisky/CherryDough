using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CherryDough.Application.ViewModels;
using FluentValidation.Results;

namespace CherryDough.Application.Interface
{
    public interface IShowcaseAppService : IDisposable
    {
        Task<IEnumerable<ShowcaseViewModel>> GetAll();
        Task<IEnumerable<ShowcaseViewModel>> GetByCategory(string categoryName);
        Task<ValidationResult> AddItemAsync(ShowcaseViewModel showcaseViewModel);
        Task<ValidationResult> UpdateItemAsync(ShowcaseViewModel showcaseViewModel);
        Task<ValidationResult> RemoveItemAsync(ShowcaseViewModel showcaseViewModel);
    }
}