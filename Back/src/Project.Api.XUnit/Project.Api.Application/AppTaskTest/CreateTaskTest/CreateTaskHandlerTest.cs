using Moq;
using Project.Api.Application.Commands.AppProject;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Repositories;
using Xunit;

namespace Project.Api.XUnit.Project.Api.Application.AppProject.CreateProject
{
    public class CreateTaskHandlerTest
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly CreateProjectHandler _handler;

        public CreateTaskHandlerTest()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new CreateProjectHandler(_projectRepositoryMock.Object, _userRepositoryMock.Object);
        }

        private CreateProjectCommand command = new CreateProjectCommand()
        {
            AuthorUuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7"),
            Description = "New Project"
        };

        private UserEntity user = new UserEntity()
        {
            Id = 1,
            CreateAt = DateTime.Now.Date,
            IsActive = true,
            Name = "Name",
            Role = Domain.Enum.RoleUserType.Manager,
            Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7")
        };


        [Fact]
        public async Task Handle_ValidCreation_ShouldBeValid()
        {

            _userRepositoryMock.Setup(r => r.GetByUuid(command.AuthorUuid)).ReturnsAsync(user);
            _projectRepositoryMock.Setup(r => r.Add(It.IsAny<ProjectEntity>())).ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);

            _userRepositoryMock.Verify(x => x.GetByUuid(command.AuthorUuid), Times.Once);
            _projectRepositoryMock.Verify(x => x.Add(It.IsAny<ProjectEntity>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ValidCreation_ShouldBeInvalid_UserNotFound()
        {


            _userRepositoryMock.Setup(r => r.GetByUuid(command.AuthorUuid)).ReturnsAsync((UserEntity)null);
            _projectRepositoryMock.Setup(r => r.Add(It.IsAny<ProjectEntity>())).ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);

            _userRepositoryMock.Verify(x => x.GetByUuid(command.AuthorUuid), Times.Once);
            _projectRepositoryMock.Verify(x => x.Add(It.IsAny<ProjectEntity>()), Times.Never);
        }
    

        [Fact]
        public async Task Handle_ValidCreation_ShouldBeInvalid_AddError()
        {

            _userRepositoryMock.Setup(r => r.GetByUuid(command.AuthorUuid)).ReturnsAsync(user);
            _projectRepositoryMock.Setup(r => r.Add(It.IsAny<ProjectEntity>())).ReturnsAsync(false);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);

            _userRepositoryMock.Verify(x => x.GetByUuid(command.AuthorUuid), Times.Once);
            _projectRepositoryMock.Verify(x => x.Add(It.IsAny<ProjectEntity>()), Times.Once);
        }
    }
}
