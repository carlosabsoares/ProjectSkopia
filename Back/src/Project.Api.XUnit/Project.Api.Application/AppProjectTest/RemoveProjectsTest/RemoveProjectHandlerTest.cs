using AutoMapper;
using Moq;
using Project.Api.Application.Commands.AppProject;
using Project.Api.Application.Configuration.Events;
using Project.Api.Domain.Dto;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Enum;
using Project.Api.Domain.Repositories;
using Xunit;

namespace Project.Api.XUnit.Project.Api.Application.AppProject.CreateProject
{
    public class RemoveProjectHandlerTest
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly RemoveProjectHandler _handler;

        private RemoveProjectQuery query = new RemoveProjectQuery()
        {
            Uuid = Guid.Parse("d2f8b1c4-3c5e-4f9a-8b6e-7f8c9d0e1f2a")
        };

        public RemoveProjectHandlerTest()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new RemoveProjectHandler(_projectRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_WhenProjectsExist_ShouldReturnSuccessWithMappedResult()
        {
            var projectEntity = new ProjectEntity
            {
                Author = new UserEntity
                {
                    Id = 1,
                    Name = "Author 1",
                    Uuid = Guid.Parse("a0709f72-b320-460c-b693-a3a3ecaa9aa9")
                },
                Id = 1,
                Description = "Project 1",
                AuthorId = 1,
                CreateAt = DateTime.Now.Date,
                Status = StatusProjectType.Active,
                Tasks = new List<TaskEntity>(),
                Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7")
            };

            var projectDtos = new ProjectDto()
            {
                Author = new UserDto
                {
                    Uuid = Guid.Parse("a0709f72-b320-460c-b693-a3a3ecaa9aa9"),
                    Name = "Author 1",
                    CreateAt = DateTime.Now.Date,
                    IsActive = true,
                    Role = RoleUserType.Manager
                },
                Status = "ACTIVE",
                CreateAt = DateTime.Now.Date,
                Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7")
            };

            _projectRepositoryMock.Setup(repo => repo.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(projectEntity);
            _projectRepositoryMock.Setup(repo => repo.Update(It.IsAny<ProjectEntity>())).ReturnsAsync(true);
            _mapperMock.Setup(m => m.Map<ProjectDto>(projectEntity)).Returns(projectDtos);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.True(result.Success);
            Assert.True((bool)result.Data);

            _projectRepositoryMock.Verify(x => x.GetByUuid(query.Uuid), Times.Once);
            _projectRepositoryMock.Verify(x => x.Update(It.IsAny<ProjectEntity>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenProjectsExist_ShouldReturnNOk_ProjetNotFound()
        {
            ProjectEntity projectEntity = null;

            var projectDtos = new ProjectDto();

            _projectRepositoryMock.Setup(repo => repo.GetByUuid(It.IsAny<Guid>())).ReturnsAsync((ProjectEntity)null);
            _mapperMock.Setup(m => m.Map<ProjectDto>(projectEntity)).Returns(projectDtos);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.False(result.Success);

            Assert.Equal("Project not found or not active", result.Data);

            _projectRepositoryMock.Verify(x => x.GetByUuid(query.Uuid), Times.Once);
            _projectRepositoryMock.Verify(x => x.Update(It.IsAny<ProjectEntity>()), Times.Never);
        }

        [Fact]
        public async Task Handle_WhenProjectsExist_ShouldReturnNOk_AllTaskNotCompleted()
        {
            var projectEntity = new ProjectEntity
            {
                Author = new UserEntity
                {
                    Id = 1,
                    Name = "Author 1",
                    Uuid = Guid.Parse("a0709f72-b320-460c-b693-a3a3ecaa9aa9")
                },
                Id = 1,
                Description = "Project 1",
                AuthorId = 1,
                CreateAt = DateTime.Now.Date,
                Status = StatusProjectType.Active,
                Tasks = new List<TaskEntity>()
                {
                    new TaskEntity
                    {
                        Id = 1,
                        Description = "Task 1",
                        Status = StatusTaskType.InProgress,
                        CreateAt = DateTime.Now.Date,
                        Uuid = Guid.Parse("d2f8b1c4-3c5e-4f9a-8b6e-7f8c9d0e1f2a")
                    }
                },
                Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7")
            };

            var projectDtos = new ProjectDto()
            {
                Author = new UserDto
                {
                    Uuid = Guid.Parse("a0709f72-b320-460c-b693-a3a3ecaa9aa9"),
                    Name = "Author 1",
                    CreateAt = DateTime.Now.Date,
                    IsActive = true,
                    Role = RoleUserType.Manager
                },
                Status = "ACTIVE",
                CreateAt = DateTime.Now.Date,
                Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7")
            };

            _projectRepositoryMock.Setup(repo => repo.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(projectEntity);
            _projectRepositoryMock.Setup(repo => repo.Update(It.IsAny<ProjectEntity>())).ReturnsAsync(true);
            _mapperMock.Setup(m => m.Map<ProjectDto>(projectEntity)).Returns(projectDtos);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.False(result.Success);

            Assert.Equal("Project has active tasks, cannot remove.", result.Data);

            _projectRepositoryMock.Verify(x => x.GetByUuid(query.Uuid), Times.Once);
            _projectRepositoryMock.Verify(x => x.Update(It.IsAny<ProjectEntity>()), Times.Never);
        }

        [Fact]
        public async Task Handle_WhenProjectsException_ShouldReturnSuccessWithMappedResult()
        {
            var projectEntity = new ProjectEntity
            {
                Author = new UserEntity
                {
                    Id = 1,
                    Name = "Author 1",
                    Uuid = Guid.Parse("a0709f72-b320-460c-b693-a3a3ecaa9aa9")
                },
                Id = 1,
                Description = "Project 1",
                AuthorId = 1,
                CreateAt = DateTime.Now.Date,
                Status = StatusProjectType.Active,
                Tasks = new List<TaskEntity>(),
                Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7")
            };

            var projectDtos = new ProjectDto()
            {
                Author = new UserDto
                {
                    Uuid = Guid.Parse("a0709f72-b320-460c-b693-a3a3ecaa9aa9"),
                    Name = "Author 1",
                    CreateAt = DateTime.Now.Date,
                    IsActive = true,
                    Role = RoleUserType.Manager
                },
                Status = "ACTIVE",
                CreateAt = DateTime.Now.Date,
                Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7")
            };

            _projectRepositoryMock.Setup(repo => repo.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(projectEntity);
            _projectRepositoryMock.Setup(repo => repo.Update(It.IsAny<ProjectEntity>())).ThrowsAsync(new Exception("Error"));
            _mapperMock.Setup(m => m.Map<ProjectDto>(projectEntity)).Returns(projectDtos);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal("Error",result.Data.ToString());

            _projectRepositoryMock.Verify(x => x.GetByUuid(query.Uuid), Times.Once);
            _projectRepositoryMock.Verify(x => x.Update(It.IsAny<ProjectEntity>()), Times.Once);
        }
    }
}
