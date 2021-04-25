using AutoMapper;
using CherryDough.Application.ViewModels;
using CherryDough.Domain.Commands;

namespace CherryDough.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ShowcaseViewModel, AddItemCommand>()
                .ConstructUsing(c => new AddItemCommand(c.Name, c.Description, c.Category));

            CreateMap<ShowcaseViewModel, UpdateItemCommand>()
                .ConstructUsing(c => new UpdateItemCommand(c.Id, c.Name, c.Description, c.Category));
        }
    }
}