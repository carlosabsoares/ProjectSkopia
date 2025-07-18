﻿using BlazorBootstrap.Extensions;
using Moq;
using Project.Api.Application.Commands.AppProject;
using Project.Api.Application.Commands.AppReports;
using Project.Api.Application.Commands.AppTask;
using Project.Api.Domain.Dto;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Enum;
using Project.Api.Domain.Repositories;
using Xunit;

namespace Project.Api.XUnit.Project.Api.Application.AppProject.CreateProject
{
    public class GetPerformanceReportHandlerTest
    {

        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITaskRepository> _taskRepositoryMock;

        private readonly GetPerformanceReportHandler _handler;

        private GetPerformanceReportQuery command = new GetPerformanceReportQuery();


        private IList<UserEntity> user = new List<UserEntity>()
        {
            new UserEntity()
            {
                Id = 1,
                CreateAt = DateTime.Now.Date,
                IsActive = true,
                Name = "Name",
                Role = Domain.Enum.RoleUserType.Manager,
                Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7")
            }
        };

        private UserEntity userNotManager = 
            new UserEntity()
            {
                Id = 1,
                CreateAt = DateTime.Now.Date,
                IsActive = true,
                Name = "Name",
                Role = Domain.Enum.RoleUserType.User,
                Uuid = Guid.Parse("cc393ae2-8227-4df7-9da5-fb996a2b9af7")
        };

        private IList<TaskEntity> tasks = new List<TaskEntity>()
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
        };



        public GetPerformanceReportHandlerTest()
        {

            _userRepositoryMock = new Mock<IUserRepository>();
            _taskRepositoryMock = new Mock<ITaskRepository>();


            _handler = new GetPerformanceReportHandler(_taskRepositoryMock.Object,
                                             _userRepositoryMock.Object);
        }


        [Fact]
        public async Task Handle_ValidPerformanceReports_ShouldBeValid()
        {
            _userRepositoryMock.Setup(r => r.GetByIds(It.IsAny<List<long>>()))
                .ReturnsAsync(user.ToList());
            _userRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(user.First);
            _taskRepositoryMock.Setup(r => r.GetPerformanceReport(It.IsAny<int>())).ReturnsAsync(tasks);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
            var data = (List<PerformanceReportsDto>)result.Data;

            Assert.Equal(1, data[0].UserId);
            Assert.Equal(1, data[0].NumberTasksCompleted);
            Assert.Equal("Name", data[0].UserName);

            _userRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _userRepositoryMock.Verify(r => r.GetByIds(It.IsAny<List<long>>()), Times.Once);
            _taskRepositoryMock.Verify(r => r.GetPerformanceReport(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ValidPerformanceReports_ShouldBeInvalid_UserNotManager()
        {
            _userRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(userNotManager);
            _userRepositoryMock.Setup(r => r.GetByIds(It.IsAny<List<long>>())).ReturnsAsync((List<UserEntity>)null);
            _taskRepositoryMock.Setup(r => r.GetPerformanceReport(It.IsAny<int>())).ReturnsAsync(tasks);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal("User does not have permission to access this report.", result.Data);

            _userRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _userRepositoryMock.Verify(r => r.GetByIds(It.IsAny<List<long>>()), Times.Never);
            _taskRepositoryMock.Verify(r => r.GetPerformanceReport(It.IsAny<int>()), Times.Never);
        }


        [Fact]
        public async Task Handle_ValidPerformanceReports_ShouldBeInvalid_UserNotFound()
        {
            _userRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync((UserEntity)null);
            _userRepositoryMock.Setup(r => r.GetByIds(It.IsAny<List<long>>())).ReturnsAsync((List<UserEntity>)null);
            _taskRepositoryMock.Setup(r => r.GetPerformanceReport(It.IsAny<int>())).ReturnsAsync(tasks);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal("User not found.", result.Data);

            _userRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _userRepositoryMock.Verify(r => r.GetByIds(It.IsAny<List<long>>()), Times.Never);
            _taskRepositoryMock.Verify(r => r.GetPerformanceReport(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ValidPerformanceReports_ShouldBeInvalid_TaskNotFound()
        {
            _userRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(user.First);
            _userRepositoryMock.Setup(r => r.GetByIds(It.IsAny<List<long>>())).ReturnsAsync((List<UserEntity>)null);
            _taskRepositoryMock.Setup(r => r.GetPerformanceReport(It.IsAny<int>())).ReturnsAsync((List<TaskEntity>)null);

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.True(result.Success);

            _userRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _userRepositoryMock.Verify(r => r.GetByIds(It.IsAny<List<long>>()), Times.Never);
            _taskRepositoryMock.Verify(r => r.GetPerformanceReport(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ErrorPerformanceReports_ShouldBeException()
        {
            _userRepositoryMock.Setup(r => r.GetByIds(It.IsAny<List<long>>())).ThrowsAsync(new Exception("Error"));
            _userRepositoryMock.Setup(r => r.GetByUuid(It.IsAny<Guid>())).ReturnsAsync(user.First);
            _taskRepositoryMock.Setup(r => r.GetPerformanceReport(It.IsAny<int>())).ReturnsAsync(tasks);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal("Error", result.Data);


            _userRepositoryMock.Verify(r => r.GetByUuid(It.IsAny<Guid>()), Times.Once);
            _userRepositoryMock.Verify(r => r.GetByIds(It.IsAny<List<long>>()), Times.Once);
            _taskRepositoryMock.Verify(r => r.GetPerformanceReport(It.IsAny<int>()), Times.Once);
        }
    }
}
