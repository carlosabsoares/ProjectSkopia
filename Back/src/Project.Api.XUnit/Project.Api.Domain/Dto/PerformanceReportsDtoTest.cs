using Xunit;
using Project.Api.Domain.Dto;

namespace Project.Api.Tests.Domain.Dto
{
    public class PerformanceReportsDtoTests
    {
        [Fact]
        public void Should_Create_Dto_With_Default_Values()
        {
            // Arrange
            var dto = new PerformanceReportsDto();

            // Assert
            Assert.Equal(0, dto.UserId);
            Assert.Equal(string.Empty, dto.UserName);
            Assert.Equal(0, dto.NumberTasksCompleted);
        }

        [Fact]
        public void Should_Assign_Properties_Correctly()
        {
            // Arrange
            var dto = new PerformanceReportsDto
            {
                UserId = 10,
                UserName = "Carlos",
                NumberTasksCompleted = 5
            };

            // Assert
            Assert.Equal(10, dto.UserId);
            Assert.Equal("Carlos", dto.UserName);
            Assert.Equal(5, dto.NumberTasksCompleted);
        }
    }
}
