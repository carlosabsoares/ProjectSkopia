using AutoMapper;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Application.Commands.AppProject;
using Project.Api.Application.Configuration.Events;
using Project.Api.Application.Configuration.Queries;
using Project.Api.Domain.Dto;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Repositories;

namespace Project.Api.Application.Commands.AppProject
{
    public class GetAllProjectHandler : Notifiable, IQueryHandler<GetAllProjectQuery>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetAllProjectHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IEvent> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var baseEntity = await _projectRepository.GetAll();

                var result = _mapper.Map<IEnumerable<ProjectDto>>(baseEntity);

                return new ResultEvent(true, result != null && result.Any() ? result : new List<ProjectDto>());
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
        }
    }
}