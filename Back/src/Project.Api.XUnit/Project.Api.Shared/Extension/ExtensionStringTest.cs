using Project.Api.Shared.Extension;
using Xunit;

namespace Project.Api.XUnit.Project.Api.Shared.Extension
{
    public class ExtensionStringTest
    {
        [Theory]
        [InlineData("d9b70927-93be-4d0e-b7c3-62e58b798e68", true)]
        [InlineData("00000000-0000-0000-0000-000000000000", false)]
        [InlineData("not-a-guid", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsGuid_ShouldReturnExpectedResult(string input, bool expected)
        {
            // Arrange & Act
            var result = input?.IsGuid() ?? false;

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
