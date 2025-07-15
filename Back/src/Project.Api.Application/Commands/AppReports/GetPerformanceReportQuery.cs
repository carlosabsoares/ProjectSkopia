using Flunt.Notifications;
using Flunt.Validations;
using Project.Api.Application.Configuration.Queries;
using Project.Api.Shared.Extension;

namespace Project.Api.Application.Commands.AppReports
{
    public class GetPerformanceReportQuery : Notifiable, IQuery
    {

        public Guid UserUuid { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(UserUuid, "UserUuid", "User UUID is required")
                .IsNotNullOrEmpty(UserUuid.ToString(), "UserUuid", "User UUID cannot be empty")
                .IsTrue(UserUuid.ToString().IsGuid(), "UserUuid", "User UUID must be a valid GUID")
            );
        }
    }
}