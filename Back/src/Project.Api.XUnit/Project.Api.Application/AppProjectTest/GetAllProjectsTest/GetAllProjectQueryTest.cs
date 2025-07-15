using Project.Api.Application.Commands.AppProject;
using Xunit;

namespace Project.Api.XUnit.Project.Api.Application.AppProject
{
    public class GetAllProjectQueryTest
    {
        [Fact]
        public void Validate_WhenNotImplemented_ShouldThrowException()
        {

            var query = new GetAllProjectQuery();

            Assert.True(query.Valid);
            Assert.False(query.Invalid);
            Assert.True(query.Notifications.Count < 1);
        }
    }
}

