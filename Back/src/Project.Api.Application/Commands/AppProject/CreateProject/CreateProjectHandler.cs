using Flunt.Notifications;
using Project.Api.Application.Configuration.Commands;
using Project.Api.Application.Configuration.Events;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Repositories;

namespace Project.Api.Application.Commands.AppProject
{
    public class CreateTaskHandler : Notifiable, ICommandHandler<CreateProjectCommand>
    {
        private IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public CreateTaskHandler(
                IProjectRepository projectRepository,
                IUserRepository userRepository
            )
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<IEvent> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
                return new ResultEvent(false, request.Notifications);

            var user = await _userRepository.GetByUuid(request.AuthorUuid);

            if (user == null)
                return new ResultEvent(false, "User not found or not active");

            bool result = false;

            try
            {
                var entity = new ProjectEntity()
                {
                    AuthorId = user.Id,
                    Description = request.Description
                };

                result = await _projectRepository.Add(entity);

                return new ResultEvent(true, result ? result : null);
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
        }
    }
}