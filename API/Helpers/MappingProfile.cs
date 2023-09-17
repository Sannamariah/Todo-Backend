using Api.Dtos;
using AutoMapper;
using Entity;

namespace Api.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Todo, TodoDto>();
        }

    
    }
}
