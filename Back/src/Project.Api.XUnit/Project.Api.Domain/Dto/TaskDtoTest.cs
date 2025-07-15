using Xunit;
using Project.Api.Domain.Dto;
using Project.Api.Domain.Enum;
using System;

namespace Project.Api.Tests.Domain.Dto
{
    public class TaskDtoTests
    {
        [Fact]
        public void Should_Initialize_With_Defaults()
        {
            // Arrange
            var dto = new TaskDto
            {
                Uuid = Guid.NewGuid(),
                Title = "Tarefa Exemplo",
                Description = "Descrição de exemplo",
                ExpirationDate = DateTime.Today.AddDays(1),
                Author = new UserDto { Name = "Carlos", Role = RoleUserType.Manager, IsActive = true, CreateAt = DateTime.Now.Date, Uuid = Guid.Parse("0365a732-b465-4029-9422-119846560054") },
                Project = new ProjectDto
                {
                    Uuid = Guid.NewGuid(),
                    Status = "Ativo",
                    Author = new UserDto { Name = "Carlos", Role = RoleUserType.Manager, IsActive = true, CreateAt = DateTime.Now.Date, Uuid = Guid.Parse("0365a732-b465-4029-9422-119846560054") },
                    Tasks = Array.Empty<TaskDto>()
                }
            };

            // Assert
            Assert.NotEqual(Guid.Empty, dto.Uuid);
            Assert.Equal("Tarefa Exemplo", dto.Title);
            Assert.Equal("Descrição de exemplo", dto.Description);
            Assert.Equal(StatusTaskType.Pending, dto.Status);
            Assert.NotNull(dto.Author);
            Assert.Equal("Carlos", dto.Author.Name);
            Assert.NotNull(dto.Project);
            Assert.Equal("Ativo", dto.Project.Status);
        }

    }
}
