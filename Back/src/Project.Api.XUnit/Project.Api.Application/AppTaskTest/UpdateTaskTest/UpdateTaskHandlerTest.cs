using Moq;
using Project.Api.Application.Commands.AppProject;
using Project.Api.Application.Commands.AppTask;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Enum;
using Project.Api.Domain.Repositories;
using Xunit;

namespace Project.Api.XUnit.Project.Api.Application.AppProject.CreateProject
{
    public class UpdateTaskHandlerTest
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<ITaskAuditRepository> _taskAuditRepositoryMock;

        private readonly UpdateTaskHandler _handler;

        private UpdateTaskCommand command = new UpdateTaskCommand
        {
            Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
            EditorUuid = Guid.Parse("c95ae459-757f-4e6a-9d64-c943146f138c"),
            Title = "Valid Task Title",
            Description = "A valid task description",
            ExpirationDate = DateTime.Now.Date.AddDays(7),
            Status = StatusTaskType.Pending
        };


        private UserEntity user = new UserEntity()
        {
            Id = 1,
            CreateAt = DateTime.Now.Date,
            IsActive = true,
            Name = "Name",
            Role = RoleUserType.Manager,
            Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7")
        };

        private TaskEntity tasks = new TaskEntity()
        {
            Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
            Title = "Valid Task Title",
            Description = "A valid task description",
            ExpirationDate = DateTime.Now.Date.AddDays(7),
            Status = StatusTaskType.Pending,
            AuthorId = 1,
            ProjectId = 1
        };

        private ProjectEntity project = new ProjectEntity()
        {
            Id = 1,
            CreateAt = DateTime.Now.Date,
            Description = "Project Description",
            Status = StatusProjectType.Active,
            Tasks = new List<TaskEntity>()
            {
                new TaskEntity()
                {
                    Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                }
            },
            AuthorId = 1
        };

        private ProjectEntity projects = new ProjectEntity()
        {
            Id = 1,
            CreateAt = DateTime.Now.Date,
            Description = "Project Description",
            Status = StatusProjectType.Active,
            Tasks = new List<TaskEntity>()
            {
                new TaskEntity()
                {
                    Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.Parse("1a457733-e68c-425f-8cd7-ddfa1e2f5b6d"),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.Parse("b446adab-96cc-46c7-9321-0235888cae98"),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.Parse("84c00885-c55d-4198-bc80-89ce32b1e819"),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.Parse("5e7e71a3-6875-4d58-946b-3637a455d8ab"),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                },
                new TaskEntity()
                {
                    Uuid = Guid.NewGuid(),
                    Title = "Valid Task Title",
                    Description = "A valid task description",
                    ExpirationDate = DateTime.Now.Date.AddDays(7),
                    Status = StatusTaskType.Pending,
                    AuthorId = 1,
                    ProjectId = 1
                }
            },
            AuthorId = 1
        };


        public UpdateTaskHandlerTest()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _taskAuditRepositoryMock = new Mock<ITaskAuditRepository>();

            _handler = new UpdateTaskHandler(_taskRepositoryMock.Object,
                                             _taskAuditRepositoryMock.Object,
                                             _projectRepositoryMock.Object,
                                             _userRepositoryMock.Object);
        }





        [Fact]
        public async Task Handle_ValidUpdate_ShouldBeValid()
        {
            _userRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(user);
            _taskRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(tasks);
            _projectRepositoryMock.Setup(r => r.GetById(It.IsAny<long>())).ReturnsAsync(project);
            _taskAuditRepositoryMock.Setup(r => r.Add(It.IsAny<TaskAuditEntity>())).ReturnsAsync(true);
            _taskRepositoryMock.Setup(r => r.Add(It.IsAny<Task>())).ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);

            _userRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _taskRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _projectRepositoryMock.Verify(r => r.GetById(It.IsAny<long>()), Times.Once);
            _taskAuditRepositoryMock.Verify(r => r.Add(It.IsAny<TaskAuditEntity>()), Times.Once);
            _taskRepositoryMock.Verify(r => r.Update(It.IsAny<TaskEntity>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ValidUpdate_ShouldBeInvalid_UserNotFound()
        {
            _userRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync((UserEntity)null);
            _taskRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(tasks);
            _projectRepositoryMock.Setup(r => r.GetById(It.IsAny<long>())).ReturnsAsync(project);
            _taskAuditRepositoryMock.Setup(r => r.Add(It.IsAny<TaskAuditEntity>())).ReturnsAsync(true);
            _taskRepositoryMock.Setup(r => r.Add(It.IsAny<Task>())).ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);

            _userRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _taskRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Never);
            _projectRepositoryMock.Verify(r => r.GetById(It.IsAny<long>()), Times.Never);
            _taskAuditRepositoryMock.Verify(r => r.Add(It.IsAny<TaskAuditEntity>()), Times.Never);
            _taskRepositoryMock.Verify(r => r.Update(It.IsAny<TaskEntity>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ValidUpdate_ShouldBeInvalid_TaskNotFound()
        {
            _userRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(user);
            _taskRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync((TaskEntity)null);
            _projectRepositoryMock.Setup(r => r.GetById(It.IsAny<long>())).ReturnsAsync(project);
            _taskAuditRepositoryMock.Setup(r => r.Add(It.IsAny<TaskAuditEntity>())).ReturnsAsync(true);
            _taskRepositoryMock.Setup(r => r.Add(It.IsAny<Task>())).ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);

            _userRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _taskRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _projectRepositoryMock.Verify(r => r.GetById(It.IsAny<long>()), Times.Never);
            _taskAuditRepositoryMock.Verify(r => r.Add(It.IsAny<TaskAuditEntity>()), Times.Never);
            _taskRepositoryMock.Verify(r => r.Update(It.IsAny<TaskEntity>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ValidUpdate_ShouldBeInvalid_ProjectNotFound()
        {
            _userRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(user);
            _taskRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(tasks);
            _projectRepositoryMock.Setup(r => r.GetById(It.IsAny<long>())).ReturnsAsync((ProjectEntity)null);
            _taskAuditRepositoryMock.Setup(r => r.Add(It.IsAny<TaskAuditEntity>())).ReturnsAsync(true);
            _taskRepositoryMock.Setup(r => r.Add(It.IsAny<Task>())).ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);

            _userRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _taskRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _projectRepositoryMock.Verify(r => r.GetById(It.IsAny<long>()), Times.Once);
            _taskAuditRepositoryMock.Verify(r => r.Add(It.IsAny<TaskAuditEntity>()), Times.Never);
            _taskRepositoryMock.Verify(r => r.Update(It.IsAny<TaskEntity>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ValidUpdate_ShouldBeInvalid_TaskAuditNotFound()
        {
            _userRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(user);
            _taskRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(tasks);
            _projectRepositoryMock.Setup(r => r.GetById(It.IsAny<long>())).ReturnsAsync(project);
            _taskAuditRepositoryMock.Setup(r => r.Add(It.IsAny<TaskAuditEntity>())).ReturnsAsync(false);
            _taskRepositoryMock.Setup(r => r.Add(It.IsAny<Task>())).ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);

            _userRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _taskRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _projectRepositoryMock.Verify(r => r.GetById(It.IsAny<long>()), Times.Once);
            _taskAuditRepositoryMock.Verify(r => r.Add(It.IsAny<TaskAuditEntity>()), Times.Once);
            _taskRepositoryMock.Verify(r => r.Update(It.IsAny<TaskEntity>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ValidUpdate_ShouldBeInvalid_ProjetsGetMore20Tasks()
        {
            _userRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(user);
            _taskRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(tasks);
            _projectRepositoryMock.Setup(r => r.GetById(It.IsAny<long>())).ReturnsAsync(projects);
            _taskAuditRepositoryMock.Setup(r => r.Add(It.IsAny<TaskAuditEntity>())).ReturnsAsync(true);
            _taskRepositoryMock.Setup(r => r.Add(It.IsAny<Task>())).ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);

            _userRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _taskRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _projectRepositoryMock.Verify(r => r.GetById(It.IsAny<long>()), Times.Once);
            _taskAuditRepositoryMock.Verify(r => r.Add(It.IsAny<TaskAuditEntity>()), Times.Never);
            _taskRepositoryMock.Verify(r => r.Update(It.IsAny<TaskEntity>()), Times.Never);
        }
    }
}
