using Flunt.Notifications;
using Flunt.Validations;
using Project.Api.Application.Configuration.Commands;
using Project.Api.Shared.Extension;

namespace Project.Api.Application.Commands.AppProject
{
    public class CreateProjectCommand : Notifiable, ICommand
    {
        public Guid AuthorUuid { get; set; }
        public string Description { get; set; } = string.Empty;

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(AuthorUuid, "AuthorUuid", "Author UUID is required")
                .IsNotNullOrEmpty(AuthorUuid.ToString(), "AuthorUuid", "Author UUID cannot be empty")
                .IsTrue(AuthorUuid.ToString().IsGuid(), "AuthorUuid", "Author UUID must be a valid GUID")
                .IsNotNullOrEmpty(Description, "Description", "Description cannot be empty")
                .HasMinLen(Description, 3, "Description", "Description must be at least 3 characters long")
                .HasMaxLen(Description, 500, "Description", "Description must not exceed 500 characters")
            );
        }
    }
}