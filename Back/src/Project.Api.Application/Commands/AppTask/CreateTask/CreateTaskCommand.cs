using Flunt.Notifications;
using Flunt.Validations;
using Project.Api.Application.Configuration.Commands;
using Project.Api.Domain.Enum;
using Project.Api.Shared.Extension;

namespace Project.Api.Application.Commands.AppProject
{
    public class CreateTaskCommand : Notifiable, ICommand
    {
        public Guid ProjectUuid { get; set; }
        public Guid AuthorUuid { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }

        public StatusTaskType Status { get; set; } = StatusTaskType.Pending;

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(AuthorUuid, "AuthorUuid", "Author UUID is required")
                .IsNotNullOrEmpty(AuthorUuid.ToString(), "AuthorUuid", "Author UUID cannot be empty")
                .IsTrue(AuthorUuid.ToString().IsGuid(), "AuthorUuid", "Author UUID must be a valid GUID")
                .IsNotNull(ProjectUuid, "ProjectUuid", "Project UUID is required")
                .IsNotNullOrEmpty(ProjectUuid.ToString(), "ProjectUuid", "Project UUID cannot be empty")
                .IsTrue(ProjectUuid.ToString().IsGuid(), "ProjectUuid", "Project UUID must be a valid GUID")
                .IsNotNullOrEmpty(Title, "Title", "Title is required")
                .HasMinLen(Title, 3, "Title", "Title must be at least 3 characters long")
                .HasMaxLen(Title, 100, "Title", "Title must not exceed 100 characters")
                .IsNotNullOrEmpty(Description, "Description", "Description is required")
                .HasMinLen(Description, 10, "Description", "Description must be at least 10 characters long")
                .HasMaxLen(Description, 500, "Description", "Description must not exceed 500 characters")
                .IsGreaterThan(ExpirationDate, DateTime.UtcNow, "DueDate", "Due date must be in the future")
            );
        }
    }
}