using AutoMapper;
using Project.Api.Domain.Dto;
using Project.Api.Domain.Entities;

namespace Project.Api.Application.Configuration.Mapper
{
    public class DomainToDtoMap : Profile
    {
        public DomainToDtoMap()
        {
            // Usar o .ReverseMap apenas quando necessário
            CreateMap<ProjectEntity, ProjectDto>().ReverseMap();
            CreateMap<UserEntity, UserDto>().ReverseMap();
            CreateMap<TaskEntity, TaskDto>().ReverseMap();
        }
    }
}