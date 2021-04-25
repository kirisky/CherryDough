using AutoMapper;
using CherryDough.Application.ViewModels;
using CherryDough.Domain.Models;

namespace CherryDough.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Item, ShowcaseViewModel>();
        }
    }
}