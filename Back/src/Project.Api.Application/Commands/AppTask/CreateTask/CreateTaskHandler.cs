using Flunt.Notifications;
using Project.Api.Application.Commands.AppProject;
using Project.Api.Application.Configuration.Commands;
using Project.Api.Application.Configuration.Events;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Repositories;

namespace Project.Api.Application.Commands.AppTask
{
    public class CreateTaskHandler : Notifiable, ICommandHandler<CreateTaskCommand>
    {
        private ITaskRepository _taskRepository;
        private readonly ITaskAuditRepository _taskAuditRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public CreateTaskHandler(
                ITaskRepository taskRepository,
                ITaskAuditRepository taskAuditRepository,
                IProjectRepository projectRepository,
                IUserRepository userRepository
            )
        {
            _taskRepository = taskRepository;
            _taskAuditRepository = taskAuditRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<IEvent> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
                return new ResultEvent(false, request.Notifications);

            var user = await _userRepository.GetByUuid(request.AuthorUuid);

            if (user == null)
                return new ResultEvent(false, "User not found or not active");

            var project = await _projectRepository.GetByUuid(request.ProjectUuid);

            if (project == null)
                return new ResultEvent(false, "Project not found or not active");

            if (project.Tasks.Count() > 20)
                return new ResultEvent(false, "Project already has 20 tasks, cannot create more.");

            try
            {
                bool result = false;

                var taskEntity = new TaskEntity()
                {
                    ProjectId = project.Id,
                    AuthorId = user.Id,
                    Title = request.Title,
                    Description = request.Description,
                    ExpirationDate = request.ExpirationDate,
                    Status = request.Status
                };

                result = await _taskRepository.Add(taskEntity);

                if (!result)
                    return new ResultEvent(false, "Failed to create task");

                return new ResultEvent(true, result ? result : null);
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
        }
    }
}