using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Project.Api.Api.Controllers;
using Project.Api.Application.Commands.AppProject;
using Project.Api.Application.Commands.AppTask;
using Project.Api.Application.Configuration.Events;
using Project.Api.Domain.Dto;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Enum;
using Xunit;

namespace Project.Api.XUnit.Project.Api.Api.Controllers
{
    public class TaskControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TaskController _controller;

        public TaskControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new TaskController(_mediatorMock.Object);
        }

        #region Post
        [Fact]
        public async Task PostTask_Success_ShouldBeOk()
        {

            var command = new CreateTaskCommand();
            var expectedResult = new ResultEvent(true, true);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.PostTask(command);

            Assert.Equal(200, ((ObjectResult)result).StatusCode);
            Assert.True(Convert.ToBoolean(((ObjectResult)result).Value));

        }

        [Fact]
        public async Task PostProject_Success_ShouldBeNOk()
        {
            // Arrange
            var command = new CreateTaskCommand();
            var expectedResult = new ResultEvent(false, null);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.PostTask(command);

            // Assert
            Assert.Equal(400, ((ObjectResult)result).StatusCode);
            Assert.False(Convert.ToBoolean(((ObjectResult)result).Value));

        }

        #endregion

        #region GetAllTasks
        [Fact]
        public async Task GetAllProjects_Success_ShouldBeOk()
        {

            var command = new GetAllTaskQuery();
            var expectedResult = new ResultEvent(true, new List<TaskEntity>());

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetAllTasks(command);

            Assert.Equal(200, ((ObjectResult)result).StatusCode);
            Assert.Equal(new List<TaskEntity>(), ((ObjectResult)result).Value);

        }

        [Fact]
        public async Task GetAllProjects_Success_ShouldBeNOk()
        {

            var command = new GetAllTaskQuery();
            var expectedResult = new ResultEvent(false, null);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.GetAllTasks(command);

            Assert.Equal(400, ((ObjectResult)result).StatusCode);
            Assert.Equal(null, ((ObjectResult)result).Value);

        }

        #endregion

        #region RemoveTask
        [Fact]
        public async Task RemoveProject_Success_ShouldBeOk()
        {

            var command = new RemoveTaskQuery();
            var expectedResult = new ResultEvent(true, true);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.RemoveTasks(command);

            Assert.Equal(200, ((ObjectResult)result).StatusCode);
            Assert.Equal(true, ((ObjectResult)result).Value);
        }

        [Fact]
        public async Task RemoveProjects_Success_ShouldBeNOk()
        {

            var command = new RemoveTaskQuery();
            var expectedResult = new ResultEvent(false, null);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.RemoveTasks(command);

            Assert.Equal(400, ((ObjectResult)result).StatusCode);
            Assert.Equal(null, ((ObjectResult)result).Value);

        }

        #endregion

        #region UpdateTask
        [Fact]
        public async Task UpdateProject_Success_ShouldBeOk()
        {

            var command = new UpdateTaskCommand();
            var expectedResult = new ResultEvent(true, true);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.UpdateCategory(command);

            Assert.Equal(200, ((ObjectResult)result).StatusCode);
            Assert.Equal(true, ((ObjectResult)result).Value);
        }

        [Fact]
        public async Task UpdateProjects_Success_ShouldBeNOk()
        {

            var command = new UpdateTaskCommand();
            var expectedResult = new ResultEvent(false, null);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var result = await _controller.UpdateCategory(command);

            Assert.Equal(400, ((ObjectResult)result).StatusCode);
            Assert.Equal(null, ((ObjectResult)result).Value);

        }

        #endregion
    }
}
