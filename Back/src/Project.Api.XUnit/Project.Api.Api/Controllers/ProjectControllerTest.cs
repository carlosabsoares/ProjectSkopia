using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Project.Api.Api.Controllers;
using Project.Api.Application.Commands.AppProject;
using Project.Api.Application.Configuration.Events;
using Project.Api.Domain.Dto;
using Project.Api.Domain.Enum;
using Xunit;

namespace Project.Api.XUnit.Project.Api.Api.Controllers
{
    public class ProjectControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProjectController _controller;

        public ProjectControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProjectController(_mediatorMock.Object);
        }

        #region Post
        [Fact]
        public async Task PostProject_Success_ShouldBeOk()
        {

            var command = new CreateProjectCommand();
            var expectedResult = new ResultEvent(true, true);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.PostProject(command);

            Assert.Equal(200, ((ObjectResult)result).StatusCode);
            Assert.True(Convert.ToBoolean(((ObjectResult)result).Value));

        }

        [Fact]
        public async Task PostProject_Success_ShouldBeNOk()
        {
            // Arrange
            var command = new CreateProjectCommand();
            var expectedResult = new ResultEvent(false, null);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.PostProject(command);

            // Assert
            Assert.Equal(400, ((ObjectResult)result).StatusCode);
            Assert.False(Convert.ToBoolean(((ObjectResult)result).Value));

        }

        #endregion

        #region GetAllProjects
        [Fact]
        public async Task GetAllProjects_Success_ShouldBeOk()
        {

            var command = new GetAllProjectQuery();
            var expectedResult = new ResultEvent(true, new List<ProjectDto>());

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetAllProjects(command);

            Assert.Equal(200, ((ObjectResult)result).StatusCode);
            Assert.Equal(new List<ProjectDto>(), ((ObjectResult)result).Value);

        }

        [Fact]
        public async Task GetAllProjects_Success_ShouldBeNOk()
        {

            var command = new GetAllProjectQuery();
            var expectedResult = new ResultEvent(false, null);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetAllProjects(command);

            Assert.Equal(400, ((ObjectResult)result).StatusCode);
            Assert.Equal(null, ((ObjectResult)result).Value);

        }

        #endregion

        #region GetByUuidProjects
        [Fact]
        public async Task GetByUuidProjects_Success_ShouldBeOk()
        {
            var project = new ProjectDto
            {
                Author = new UserDto
                {
                    CreateAt = DateTime.Now.Date,
                    IsActive = true,
                    Role = RoleUserType.Manager,
                    Name = "Test User",
                    Uuid = Guid.Parse("76ddd3c0-89ae-41ad-be8c-2828d2736ed0")
                },
                Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
                CreateAt = DateTime.Now.Date,
                Status = "ACTIVE",
                Tasks = new List<TaskDto>
                {
                    new TaskDto
                    {
                        Uuid = Guid.NewGuid(),
                        Title = "Test Task",
                        Description = "This is a test task",
                        ExpirationDate = DateTime.Now.AddDays(7),
                        Status = StatusTaskType.Pending,
                    }
                }
            };


            var command = new GetByUuidProjectQuery();
            var expectedResult = new ResultEvent(true, project);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetByUuidProjects(command);

            Assert.Equal(200, ((ObjectResult)result).StatusCode);
            Assert.Equal(project, ((ObjectResult)result).Value);
        }

        [Fact]
        public async Task GetByUuidProjects_Success_ShouldBeNOk()
        {

            var command = new GetByUuidProjectQuery();
            var expectedResult = new ResultEvent(false, null);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetByUuidProjects(command);

            Assert.Equal(400, ((ObjectResult)result).StatusCode);
            Assert.Equal(null, ((ObjectResult)result).Value);

        }

        #endregion

        #region RemoveProject
        [Fact]
        public async Task RemoveProject_Success_ShouldBeOk()
        {

            var command = new RemoveProjectQuery();
            var expectedResult = new ResultEvent(true, true);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetByUuidProjects(command);

            Assert.Equal(200, ((ObjectResult)result).StatusCode);
            Assert.Equal(true, ((ObjectResult)result).Value);
        }

        [Fact]
        public async Task RemoveProjects_Success_ShouldBeNOk()
        {

            var command = new RemoveProjectQuery();
            var expectedResult = new ResultEvent(false, null);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetByUuidProjects(command);

            Assert.Equal(400, ((ObjectResult)result).StatusCode);
            Assert.Equal(null, ((ObjectResult)result).Value);

        }

        #endregion
    }
}
