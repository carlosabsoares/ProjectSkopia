using Flunt.Notifications;
using Project.Api.Application.Commands.AppProject;
using Project.Api.Application.Configuration.Commands;
using Project.Api.Application.Configuration.Events;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Repositories;

namespace Project.Api.Application.Commands.AppTask
{
    public class UpdateTaskHandler : Notifiable, ICommandHandler<UpdateTaskCommand>
    {
        private ITaskRepository _taskRepository;
        private readonly ITaskAuditRepository _taskAuditRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public UpdateTaskHandler(
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

        public async Task<IEvent> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
                return new ResultEvent(false, request.Notifications);

            var user = await _userRepository.GetByUuid(request.EditorUuid);

            if (user == null)
                return new ResultEvent(false, "User not found or not active");

            var task = await _taskRepository.GetByUuid(request.Uuid);

            if (task == null)
                return new ResultEvent(false, "Task not found or not active");

            var project = await _projectRepository.GetById(task.ProjectId);

            if (project == null)
                return new ResultEvent(false, "Project not found or not active");

            if (project.Tasks.Count() > 20)
                return new ResultEvent(false, "Project already has 20 tasks, cannot create more.");

            try
            {
                bool result = false;

                await _taskRepository.BeginTransactionAsync();

                var taskAuditEntity = new TaskAuditEntity()
                {
                    AuthorId = task.AuthorId,
                    Description = task.Description,
                    CreateAt = task.CreateAt,
                    Date = DateTime.Now,
                    EditorId = user.Id,
                    ProjectId = task.ProjectId,
                    Status = task.Status,
                    Title = task.Title,
                    ExpirationDate = task.ExpirationDate,
                    TaskId = task.Id
                };

                result = await _taskAuditRepository.Add(taskAuditEntity);

                if (!result)
                    return new ResultEvent(false, "Failed to create task audit");

                task.Title = request?.Title == null ? task.Title : request.Title;
                task.Description = request?.Description == null ? task.Description : request.Description;
                task.ExpirationDate = request?.ExpirationDate == null ? task.ExpirationDate : request.ExpirationDate;
                task.Status = request?.Status == null ? request.Status : task.Status;

                await _taskRepository.Update(task);

                await _taskRepository.CommitTransactionAsync();

                return new ResultEvent(true, result ? result : null);
            }
            catch (Exception ex)
            {
                _taskRepository.RollbackTransactionAsync();

                return new ResultEvent(false, ex.Message);
            }
        }
    }
}