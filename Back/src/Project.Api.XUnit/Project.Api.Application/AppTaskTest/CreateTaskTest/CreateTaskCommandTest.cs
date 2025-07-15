using Project.Api.Application.Commands.AppProject;
using Project.Api.Domain.Enum;
using Xunit;

namespace Project.Api.XUnit.Project.Api.Application.AppProject
{
    public class CreateTaskCommandTest
    {
        [Fact]
        public void Validate_WithValidData_ShouldBeValid()
        {
            // Arrange
            var command = new CreateTaskCommand
            {
                ProjectUuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
                AuthorUuid = Guid.Parse("c95ae459-757f-4e6a-9d64-c943146f138c"),
                Title = "Valid Task Title",
                Description = "A valid task description",
                ExpirationDate = DateTime.Now.Date.AddDays(7),
                Status = StatusTaskType.Pending
            };

            // Act
            command.Validate();

            // Assert
            Assert.True(command.Valid);
            Assert.False(command.Invalid);
            Assert.True(command.Notifications.Count == 0);

            Assert.Equal(0, command.Notifications.Count);
            Assert.Equal("Valid Task Title", command.Title);
            Assert.Equal("A valid task description", command.Description);  
            Assert.NotEqual(Guid.Empty, command.AuthorUuid);
            Assert.NotEqual(Guid.Empty, command.ProjectUuid);
            Assert.Equal(command.ProjectUuid, Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"));
            Assert.Equal(command.AuthorUuid, Guid.Parse("c95ae459-757f-4e6a-9d64-c943146f138c"));
            Assert.True(command.ExpirationDate == DateTime.Now.Date.AddDays(7));
            Assert.Equal(StatusTaskType.Pending, command.Status);

        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("a")] 
        [InlineData("ab")] 
        public void Validate_WithEmptyDescription_ShouldBeInvalid(string? description)
        {

            // Arrange
            var command = new CreateTaskCommand
            {
                ProjectUuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
                AuthorUuid = Guid.Parse("c95ae459-757f-4e6a-9d64-c943146f138c"),
                Title = "Valid Task Title",
                Description = description,
                ExpirationDate = DateTime.Now.Date.AddDays(7),
                Status = StatusTaskType.Pending
            };

            // Act
            command.Validate();
            var _notification = (List<Flunt.Notifications.Notification>)command.Notifications;

            // Assert
            Assert.False(command.Valid);
            Assert.True(command.Invalid);
            Assert.True(command.Notifications.Count > 0);

            Assert.True(_notification[0].Property == "Description");
            Assert.True(_notification[0].Message.Contains("Description"));
        }

        [Fact]
        public void Validate_WithEmptyDescription_More500Digits_ShouldBeInvalid()
        {

            // Arrange
            var command = new CreateTaskCommand
            {
                ProjectUuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
                AuthorUuid = Guid.Parse("c95ae459-757f-4e6a-9d64-c943146f138c"),
                Title = "Valid Task Title",
                Description = new string('x', 501),
                ExpirationDate = DateTime.Now.Date.AddDays(7),
                Status = StatusTaskType.Pending
            };

            // Act
            command.Validate();
            var _notification = (List<Flunt.Notifications.Notification>)command.Notifications;

            // Assert
            Assert.False(command.Valid);
            Assert.True(command.Invalid);
            Assert.True(command.Notifications.Count > 0);

            Assert.True(_notification[0].Property == "Description");
            Assert.True(_notification[0].Message.Contains("Description"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("a")]
        [InlineData("ab")]
        public void Validate_WithEmptyTitle_ShouldBeInvalid(string? title)
        {

            // Arrange
            var command = new CreateTaskCommand
            {
                ProjectUuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
                AuthorUuid = Guid.Parse("c95ae459-757f-4e6a-9d64-c943146f138c"),
                Title = title,
                Description = "A valid task description",
                ExpirationDate = DateTime.Now.Date.AddDays(7),
                Status = StatusTaskType.Pending
            };

            // Act
            command.Validate();
            var _notification = (List<Flunt.Notifications.Notification>)command.Notifications;

            // Assert
            Assert.False(command.Valid);
            Assert.True(command.Invalid);
            Assert.True(command.Notifications.Count > 0);

            Assert.True(_notification[0].Property == "Title");
            Assert.True(_notification[0].Message.Contains("Title"));
        }

        [Fact]
        public void Validate_WithEmptyTitle_More100Digits_ShouldBeInvalid()
        {

            // Arrange
            var command = new CreateTaskCommand
            {
                ProjectUuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
                AuthorUuid = Guid.Parse("c95ae459-757f-4e6a-9d64-c943146f138c"),
                Title = new string('x', 101),
                Description = "A valid task description",
                ExpirationDate = DateTime.Now.Date.AddDays(7),
                Status = StatusTaskType.Pending
            };

            // Act
            command.Validate();
            var _notification = (List<Flunt.Notifications.Notification>)command.Notifications;

            // Assert
            Assert.False(command.Valid);
            Assert.True(command.Invalid);
            Assert.True(command.Notifications.Count > 0);

            Assert.True(_notification[0].Property == "Title");
            Assert.True(_notification[0].Message.Contains("Title"));
        }

        [Fact]
        public void Validate_WithDate_ShouldBeInvalid()
        {

            // Arrange
            var command = new CreateTaskCommand
            {
                ProjectUuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
                AuthorUuid = Guid.Parse("c95ae459-757f-4e6a-9d64-c943146f138c"),
                Title = new string('x', 10),
                Description = "A valid task description",
                ExpirationDate = DateTime.Now.Date,
                Status = StatusTaskType.Pending
            };

            // Act
            command.Validate();
            var _notification = (List<Flunt.Notifications.Notification>)command.Notifications;

            // Assert
            Assert.False(command.Valid);
            Assert.True(command.Invalid);
            Assert.True(command.Notifications.Count > 0);

            Assert.True(_notification[0].Property == "DueDate");
            Assert.True(_notification[0].Message.Contains("date"));
        }
    }
}

