using AutoMapper;
using Moq;
using Project.Api.Application.Commands.AppProject;
using Project.Api.Application.Commands.AppTask;
using Project.Api.Application.Configuration.Events;
using Project.Api.Domain.Dto;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Repositories;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Project.Api.XUnit.Project.Api.Application.AppProject.CreateProject
{
    public class GetAllProjectHandlerTest
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetAllTaskHandler _handler;

        public GetAllProjectHandlerTest()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetAllTaskHandler(_taskRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_WhenProjectsExist_ShouldReturnSuccessWithMappedResult()
        {
            var taskEntity = new List<TaskEntity>
            {
                new TaskEntity()
                {
                    Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = Domain.Enum.StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.Parse("7192884f-152f-4195-bb29-92e81a70da07"),
                    Title = "Valid Task Title 2",
                    Description = "A valid task description 2",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = Domain.Enum.StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 2
                }
            };

            var taskDtos = new List<TaskDto>
            {
                new TaskDto()
                {
                    Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = Domain.Enum.StatusTaskType.Pending
                },
                new TaskDto()
                {
                    Uuid = Guid.Parse("7192884f-152f-4195-bb29-92e81a70da07"),
                    Title = "Valid Task Title 2",
                    Description = "A valid task description 2",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = Domain.Enum.StatusTaskType.Pending
                }
            };

            _taskRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(taskEntity);
            _mapperMock.Setup(m => m.Map<IEnumerable<TaskDto>>(taskEntity)).Returns(taskDtos);

            var query = new GetAllTaskQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            var data = (List<TaskDto>)result.Data;

            Assert.True(result.Success);
            Assert.True(result.Data is IEnumerable<TaskDto> dtos && dtos.Any());

            Assert.Equal(taskDtos[0].CreateAt, data[0].CreateAt);
            Assert.Equal(taskDtos[0].ExpirationDate, data[0].ExpirationDate);
            Assert.Equal(taskDtos[0].Description, data[0].Description);
            Assert.Equal(taskDtos[0].Title, data[0].Title);
            Assert.Equal(taskDtos[0].Project, data[0].Project);
            Assert.Equal(taskDtos[0].Uuid, data[0].Uuid);
            Assert.Equal(taskDtos[0].Author, data[0].Author);

            _taskRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<TaskDto>>(taskEntity), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenProjectsExist_ShouldReturnSuccessWithMappedResultNull()
        {
            var taskEntity = new List<TaskEntity>();

            var taskDtos = new List<TaskDto>();

            _taskRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(taskEntity);
            _mapperMock.Setup(m => m.Map<IEnumerable<TaskDto>>(taskEntity)).Returns(taskDtos);

            var query = new GetAllTaskQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            var data = (List<TaskDto>)result.Data;

            Assert.True(result.Success);
            Assert.True(result.Data is IEnumerable<TaskDto> dtos && !dtos.Any());

            _taskRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<TaskDto>>(taskEntity), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenProjectsExist_ShouldReturnException()
        {
            var taskEntity = new List<TaskEntity>();

            var taskDtos = new List<TaskDto>();

            _taskRepositoryMock.Setup(repo => repo.GetAll()).ThrowsAsync(new Exception("Error"));
            _mapperMock.Setup(m => m.Map<IEnumerable<TaskDto>>(taskEntity)).Returns(taskDtos);

            var query = new GetAllTaskQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal("Error", result.Data);

            _taskRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<TaskDto>>(taskEntity), Times.Never);
        }
    }
}
