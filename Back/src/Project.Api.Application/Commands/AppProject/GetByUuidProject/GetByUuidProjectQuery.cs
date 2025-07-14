using Flunt.Notifications;
using Flunt.Validations;
using Project.Api.Application.Configuration.Queries;
using Project.Api.Shared.Extension;

namespace Project.Api.Application.Commands.AppProject
{
    public class GetByUuidProjectQuery : Notifiable, IQuery
    {
        public Guid Uuid { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Uuid, "Uuid", "Project UUID is required")
                .IsNotNullOrEmpty(Uuid.ToString(), "Uuid", "Project UUID cannot be empty")
                .IsTrue(Uuid.ToString().IsGuid(), "Uuid", "Project UUID must be a valid GUID")
            );
        }
    }
    
}