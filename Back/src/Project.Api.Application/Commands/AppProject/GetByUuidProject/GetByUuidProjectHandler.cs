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
    public class GetByUuidProjectHandler : Notifiable, IQueryHandler<GetByUuidProjectQuery>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetByUuidProjectHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IEvent> Handle(GetByUuidProjectQuery request, CancellationToken cancellationToken)
        {

            request.Validate();
            if (request.Invalid)
                return new ResultEvent(false, request.Notifications);


            try
            {
                var baseEntity = await _projectRepository.GetByUuid(request.Uuid);

                var result = _mapper.Map<ProjectDto>(baseEntity);

                return new ResultEvent(true, result != null ? result : new List<ProjectDto>());
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
        }
    }
}