using AutoMapper;
using Moq;
using Project.Api.Application.Commands.AppProject;
using Project.Api.Application.Configuration.Events;
using Project.Api.Domain.Dto;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Repositories;
using Xunit;

namespace Project.Api.XUnit.Project.Api.Application.AppProject.CreateProject
{
    public class GetByUuidProjectQueryTest
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetByUuidProjectHandler _handler;

        public GetByUuidProjectQueryTest()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetByUuidProjectHandler(_projectRepositoryMock.Object, _mapperMock.Object);
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
                Status = Domain.Enum.StatusProjectType.Active,
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
                    Role = Domain.Enum.RoleUserType.Manager
                },
                Status = "ACTIVE",
                CreateAt = DateTime.Now.Date,
                Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7")
            };

            _projectRepositoryMock.Setup(repo => repo.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(projectEntity);
            _mapperMock.Setup(m => m.Map<ProjectDto>(projectEntity)).Returns(projectDtos);

            var query = new GetByUuidProjectQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.True(result.Success);

            _projectRepositoryMock.Verify(x => x.Add(It.IsAny<ProjectEntity>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenProjectsExist_ShouldReturnSuccessWithMappedResultNull()
        {
            var projectEntities = new ProjectEntity();

            var projectDtos = new ProjectDto();

            _projectRepositoryMock.Setup(repo => repo.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(projectEntities);
            _mapperMock.Setup(m => m.Map<ProjectDto>(projectEntities)).Returns(projectDtos);

            var query = new GetByUuidProjectQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.True(result.Success);
        }
    }
}
