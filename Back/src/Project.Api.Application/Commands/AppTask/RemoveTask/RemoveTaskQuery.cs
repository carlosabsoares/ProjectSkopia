using Flunt.Notifications;
using Flunt.Validations;
using Project.Api.Application.Configuration.Commands;
using Project.Api.Application.Configuration.Queries;
using Project.Api.Domain.Enum;
using Project.Api.Shared.Extension;

namespace Project.Api.Application.Commands.AppProject
{
    public class RemoveTaskQuery : Notifiable, IQuery
    {
        public Guid Uuid { get; set; }

        public Guid UserUuid { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Uuid, "Uuid", "Task UUID is required")
                .IsNotNullOrEmpty(Uuid.ToString(), "Uuid", "Task UUID cannot be empty")
                .IsTrue(Uuid.ToString().IsGuid(), "Uuid", "Task UUID must be a valid GUID")
                .IsNotNull(UserUuid, "UserUuid", "User UUID is required")
                .IsNotNullOrEmpty(UserUuid.ToString(), "UserUuid", "User UUID cannot be empty")
                .IsTrue(UserUuid.ToString().IsGuid(), "UserUuid", "User UUID must be a valid GUID")
            );
        }
    }
}