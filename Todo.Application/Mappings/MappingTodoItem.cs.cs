using AutoMapper;
using ToDoList.Core.Entities;
using ToDoList.Shared.DTOs;

namespace Todo.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoItem, ToDoItemDTO>().ReverseMap();
        }
    }
}
