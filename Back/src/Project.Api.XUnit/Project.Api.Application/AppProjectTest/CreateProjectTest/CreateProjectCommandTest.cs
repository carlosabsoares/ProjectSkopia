using Project.Api.Application.Commands.AppProject;
using Xunit;

namespace Project.Api.XUnit.Project.Api.Application.AppProject
{
    public class CreateProjectCommandTest
    {
        [Fact]
        public void Validate_WithValidData_ShouldBeValid()
        {
            // Arrange
            var command = new CreateProjectCommand
            {
                AuthorUuid = Guid.NewGuid(),
                Description = "A valid project description"
            };

            // Act
            command.Validate();

            // Assert
            Assert.True(command.Valid);
            Assert.False(command.Invalid);
            Assert.True(command.Notifications.Count == 0);

            Assert.Equal(0, command.Notifications.Count);
            Assert.Equal("A valid project description", command.Description);
            Assert.NotEqual(Guid.Empty, command.AuthorUuid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("a")] 
        [InlineData("ab")] 
        public void Validate_WithEmptyDescription_ShouldBeInvalid(string? descriptions)
        {

            var command = new CreateProjectCommand
            {
                AuthorUuid = Guid.NewGuid(),
                Description = descriptions
            };
            var _notification = (List<Flunt.Notifications.Notification>)command.Notifications;


            command.Validate();


            Assert.False(command.Valid);
            Assert.True(command.Invalid);
            Assert.True(command.Notifications.Count > 0);

            Assert.True(_notification[0].Property == "Description");
            Assert.True(_notification[0].Message.Contains("Description"));
        }

        [Fact]
        public void Validate_WithTooLongDescription_ShouldBeInvalid()
        {
            var description = new string('x', 501);

            var command = new CreateProjectCommand
            {
                AuthorUuid = Guid.NewGuid(),
                Description = description
            };
            var _notification = (List<Flunt.Notifications.Notification>)command.Notifications;

            command.Validate();

            Assert.False(command.Valid);
            Assert.True(command.Invalid);
            Assert.True(command.Notifications.Count > 0);

            Assert.True(_notification[0].Property == "Description");
            Assert.True(_notification[0].Message.Contains("Description"));
        }

        [Fact]
        public void Validate_WithAuthorUuid_ShouldBeValid()
        {
            // Arrange
            var command = new CreateProjectCommand
            {
                AuthorUuid = Guid.NewGuid(),
                Description = "Some description"
            };

            // Act
            command.Validate();

            // Assert
            Assert.True(command.Valid);
            Assert.False(command.Invalid);
            Assert.True(command.Notifications.Count == 0);

            Assert.Equal(0, command.Notifications.Count);
            Assert.Equal("Some description", command.Description);
            Assert.NotEqual(Guid.Empty, command.AuthorUuid);


        }

        [Fact]
        public void Validate_WithEmptyAuthorUuid_ShouldBeInvalid()
        {
            // Arrange
            var command = new CreateProjectCommand
            {
                AuthorUuid = Guid.Empty,
                Description = "Some description"
            };
            var _notification = (List<Flunt.Notifications.Notification>)command.Notifications;

            // Act
            command.Validate();

            // Assert
            Assert.False(command.Valid);
            Assert.True(command.Invalid);
            Assert.True(command.Notifications.Count > 0);

            Assert.True(_notification[0].Property == "AuthorUuid");
            Assert.True(_notification[0].Message.Contains("Author UUID must be a valid GUID"));

        }

        [Fact]
        public void Validate_WithAuthorUuidInvalid_ShouldBeInvalid()
        {
            // Arrange
            var command = new CreateProjectCommand
            {
                AuthorUuid = Guid.Empty,
                Description = "Some description"
            };
            var _notification = (List<Flunt.Notifications.Notification>)command.Notifications;

            // Act
            command.Validate();

            // Assert
            Assert.False(command.Valid);
            Assert.True(command.Invalid);
            Assert.True(command.Notifications.Count > 0);

            Assert.True(_notification[0].Property == "AuthorUuid");
            Assert.True(_notification[0].Message.Contains("Author UUID must be a valid GUID"));

        }
    }
}

