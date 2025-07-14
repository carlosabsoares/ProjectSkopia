using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Application.Commands.AppProject;
using Project.Api.Application.Configuration.Commands;
using Project.Api.Application.Configuration.Events;
using Project.Api.Application.Configuration.Queries;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Repositories;

namespace Project.Api.Application.Commands.AppTask
{
    public class RemoveTaskHandler : Notifiable, IQueryHandler<RemoveTaskQuery>
    {
        private ITaskRepository _taskRepository;
        private readonly ITaskAuditRepository _taskAuditRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;


        public RemoveTaskHandler(
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

        public async Task<IEvent> Handle(RemoveTaskQuery request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
                return new ResultEvent(false, request.Notifications);

            var task = await _taskRepository.GetByUuid(request.Uuid);

            if (task == null)
                return new ResultEvent(false, "Task not found or not active");

            var user = await _userRepository.GetByUuid(request.UserUuid);

            if (user == null)
                return new ResultEvent(false, "User not found or not active");

            var project = await _projectRepository.GetById(task.ProjectId);

            if (project == null)
                return new ResultEvent(false, "Project not found or not active");

            if (project.Tasks.Any(x => x.Status != Domain.Enum.StatusTaskType.Completed))
                return new ResultEvent(false, "Project has active tasks, cannot remove.");

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

                await _taskAuditRepository.Add(taskAuditEntity);


                task.Status = Domain.Enum.StatusTaskType.Removed;

                await _taskRepository.Update(task);

                await _taskRepository.CommitTransactionAsync();

                return new ResultEvent(true, result ? result : null);

            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }



        }
    }
}