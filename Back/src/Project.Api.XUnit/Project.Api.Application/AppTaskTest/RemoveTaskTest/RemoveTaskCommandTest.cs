using Project.Api.Application.Commands.AppProject;
using Project.Api.Domain.Enum;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Project.Api.XUnit.Project.Api.Application.AppProject
{
    public class RemoveTaskCommandTest
    {
        [Fact]
        public void Validate_WithValidData_ShouldBeValid()
        {
            // Arrange
            var query = new RemoveTaskQuery
            {
                UserUuid = Guid.Parse("c95ae459-757f-4e6a-9d64-c943146f138c"),
                Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7")

            };

            // Act
            query.Validate();

            // Assert
            Assert.True(query.Valid);
            Assert.False(query.Invalid);
            Assert.True(query.Notifications.Count == 0);

            Assert.Equal(0, query.Notifications.Count);
            Assert.Equal(query.Uuid, Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"));
            Assert.Equal(query.UserUuid, Guid.Parse("c95ae459-757f-4e6a-9d64-c943146f138c"));

        }

        [Fact]
        public void Validate_WithUserUuid_ShouldBeInvalid()
        {
            // Arrange
            var query = new RemoveTaskQuery
            {
                UserUuid = Guid.Empty,
                Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7")

            };
            var _notification = (List<Flunt.Notifications.Notification>)query.Notifications;

            // Act
            query.Validate();

            // Assert
            Assert.False(query.Valid);
            Assert.True(query.Invalid);
            Assert.True(query.Notifications.Count == 1);

            Assert.Equal(1, query.Notifications.Count);
            Assert.Equal(query.UserUuid, Guid.Empty);
            Assert.Equal(query.Uuid, Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"));


            Assert.True(_notification[0].Property == "UserUuid");
            Assert.True(_notification[0].Message.Contains("User"));

        }

        [Fact]
        public void Validate_WithUuid_ShouldBeInvalid()
        {
            // Arrange
            var query = new RemoveTaskQuery
            {
                UserUuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
                Uuid = Guid.Empty

            };
            var _notification = (List<Flunt.Notifications.Notification>)query.Notifications;

            // Act
            query.Validate();

            // Assert
            Assert.False(query.Valid);
            Assert.True(query.Invalid);
            Assert.True(query.Notifications.Count == 1);

            Assert.Equal(1, query.Notifications.Count);
            Assert.Equal(query.Uuid, Guid.Empty);
            Assert.Equal(query.UserUuid, Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"));


            Assert.True(_notification[0].Property == "Uuid");
            Assert.True(_notification[0].Message.Contains("UUID"));

        }


    }
}

