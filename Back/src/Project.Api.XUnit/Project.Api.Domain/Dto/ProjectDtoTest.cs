using Xunit;
using Project.Api.Domain.Dto;
using System;
using System.Collections.Generic;
using Project.Api.Domain.Enum;

namespace Project.Api.Tests.Domain.Dto
{
    public class ProjectDtoTests
    {
        [Fact]
        public void Should_Initialize_ProjectDto_With_Defaults()
        {
            // Arrange
            var dto = new ProjectDto
            {
                Uuid = Guid.Parse("6837f41a-3512-4774-88fa-c56b3f664dac"),
                Status = "Active",
                Tasks = new List<TaskDto>(),
                Author = new UserDto
                {
                    Uuid = Guid.Parse("2f6c149b-6229-473d-95a4-cad1037f4d93"),
                    CreateAt = DateTime.Now.Date,
                    IsActive = true,
                    Name = "Nome",
                    Role = RoleUserType.Manager
                }
            };

            // Assert
            Assert.NotEqual(Guid.Empty, dto.Uuid);
            Assert.True((DateTime.Now - dto.CreateAt).TotalSeconds < 5); // Criado recentemente
            Assert.Equal("Active", dto.Status);
            Assert.NotNull(dto.Tasks);
            Assert.Empty(dto.Tasks);
            Assert.NotNull(dto.Author);
            Assert.Equal(Guid.Parse("6837f41a-3512-4774-88fa-c56b3f664dac"), dto.Uuid);
            Assert.Equal(Guid.Parse("2f6c149b-6229-473d-95a4-cad1037f4d93"), dto.Author.Uuid);
            Assert.Equal(DateTime.Now.Date, dto.Author.CreateAt);
            Assert.True(dto.Author.IsActive);
            Assert.Equal("Nome", dto.Author.Name);
            Assert.Equal(RoleUserType.Manager, dto.Author.Role);

        }
    }
}
