using Project.Api.Application.Commands.AppProject;
using Project.Api.Application.Commands.AppTask;
using Xunit;

namespace Project.Api.XUnit.Project.Api.Application.AppProject
{
    public class GetAllTaskQueryTest
    {
        [Fact]
        public void Validate_WhenNotImplemented_ShouldThrowException()
        {

            var query = new GetAllTaskQuery();

            Assert.True(query.Valid);
            Assert.False(query.Invalid);
            Assert.True(query.Notifications.Count < 1);
        }
    }
}

