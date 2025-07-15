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
    public class GetAllProjectHandlerTest
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetAllProjectHandler _handler;

        public GetAllProjectHandlerTest()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetAllProjectHandler(_projectRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_WhenProjectsExist_ShouldReturnSuccessWithMappedResult()
        {
            var projectEntities = new List<ProjectEntity>
            {
                new ProjectEntity {
                    Author = new UserEntity { Id = 1, 
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
                },

                new ProjectEntity {
                    Author = new UserEntity { Id = 1, Name = "Author 1", 
                                             Uuid = Guid.Parse("b1bb71b0-710a-4038-a25c-4610db52cb34") },
                    Id = 1,
                    Description = "Project 2",
                    AuthorId = 1,
                    CreateAt = DateTime.Now.Date,
                    Status = Domain.Enum.StatusProjectType.Active,
                    Tasks = new List<TaskEntity>(),
                    Uuid = Guid.Parse("7192884f-152f-4195-bb29-92e81a70da07")
                }
            };

            var projectDtos = new List<ProjectDto>
            {
                new ProjectDto {
                    Author = new UserDto { 
                        Uuid = Guid.Parse("a0709f72-b320-460c-b693-a3a3ecaa9aa9"),
                        Name = "Author 1",
                        CreateAt = DateTime.Now.Date,
                        IsActive = true,
                        Role = Domain.Enum.RoleUserType.Manager
                    },
                    Status = "ACTIVE",
                    CreateAt = DateTime.Now.Date,
                    Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7")
                },
                new ProjectDto { 
                    Author = new UserDto {
                        Uuid = Guid.Parse("b1bb71b0-710a-4038-a25c-4610db52cb34"),
                        Name = "Author 1",
                        CreateAt = DateTime.Now.Date,
                        IsActive = true,
                        Role = Domain.Enum.RoleUserType.Manager
                    },
                    Status = "ACTIVE",
                    CreateAt = DateTime.Now.Date,
                    Uuid = Guid.Parse("7192884f-152f-4195-bb29-92e81a70da07"),
                    Tasks = new List<TaskDto>()
                }
            };

            _projectRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(projectEntities);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProjectDto>>(projectEntities)).Returns(projectDtos);

            var query = new GetAllProjectQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.True(result.Success);

            _projectRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenProjectsExist_ShouldReturnSuccessWithMappedResultNull()
        {
            var projectEntities = new List<ProjectEntity>();

            var projectDtos = new List<ProjectDto>();

            _projectRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(projectEntities);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProjectDto>>(projectEntities)).Returns(projectDtos);

            var query = new GetAllProjectQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.True(result.Success);

            _projectRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
