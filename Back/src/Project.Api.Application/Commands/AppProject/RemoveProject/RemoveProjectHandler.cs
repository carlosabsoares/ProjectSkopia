﻿using AutoMapper;
using Flunt.Notifications;
using Project.Api.Application.Configuration.Events;
using Project.Api.Application.Configuration.Queries;
using Project.Api.Domain.Repositories;

namespace Project.Api.Application.Commands.AppProject
{
    public class RemoveProjectHandler : Notifiable, IQueryHandler<RemoveProjectQuery>
    {
        private IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public RemoveProjectHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IEvent> Handle(RemoveProjectQuery request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
                return new ResultEvent(false, request.Notifications);

            var projet = await _projectRepository.GetByUuid(request.Uuid);

            if (projet == null)
                return new ResultEvent(false, "Project not found or not active");

            if (projet.Tasks.Any(x => x.Status != Domain.Enum.StatusTaskType.Completed))
                return new ResultEvent(false, "Project has active tasks, cannot remove.");

            bool result = false;

            try
            {
                projet.Status = Domain.Enum.StatusProjectType.Inactive;

                result = await _projectRepository.Update(projet);

                return new ResultEvent(true, result ? result : null);
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
        }
    }
}