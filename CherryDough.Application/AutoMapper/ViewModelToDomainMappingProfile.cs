using AutoMapper;
using CherryDough.Application.ViewModels;
using CherryDough.Domain.Models;

namespace CherryDough.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ShowcaseViewModel, Item>();
        }
    }
}