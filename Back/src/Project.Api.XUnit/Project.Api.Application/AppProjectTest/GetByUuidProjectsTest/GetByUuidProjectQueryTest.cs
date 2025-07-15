using Project.Api.Application.Commands.AppProject;
using Xunit;

namespace Project.Api.XUnit.Project.Api.Application.AppProject
{
    public class GetByUuidProjectQueryTest
    {
        [Fact]
        public void Validate_WhenNotImplemented_ShouldBeValid()
        {

            var query = new GetByUuidProjectQuery() {
                Uuid = Guid.Parse("d2f8b1c4-3c5e-4f9a-8b6e-7f8c9d0e1f2a")
            };

            query.Validate();

            Assert.True(query.Valid);
            Assert.False(query.Invalid);
            Assert.True(query.Notifications.Count < 1);
        }

        [Fact]
        public void Validate_UuidProject_GuidNOk_Zero()
        {

            var query = new GetByUuidProjectQuery()
            {
                Uuid = Guid.Parse("00000000-0000-0000-0000-000000000000"),
            };
            var _notification = (List<Flunt.Notifications.Notification>)query.Notifications;

            query.Validate();


            Assert.False(query.Valid);
            Assert.True(query.Invalid);
            Assert.True(query.Notifications.Count == 1);

            Assert.True(_notification[0].Property == "Uuid");
            Assert.True(_notification[0].Message.Contains("Project UUID must be a valid GUID"));
        }
    }
}

