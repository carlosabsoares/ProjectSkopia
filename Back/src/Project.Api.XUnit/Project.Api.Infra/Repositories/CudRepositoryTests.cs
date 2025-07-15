using Moq;
using Project.Api.Domain.Repositories;
using Xunit;
using System.Threading.Tasks;

public class CudRepositoryMockTests
{
    public class TestEntity { }

    [Fact]
    public async Task Add_ShouldReturnTrue()
    {
        // Arrange
        var mock = new Mock<ICudRepository>();

        mock.Setup(repo => repo.Add(It.IsAny<TestEntity>()))
            .ReturnsAsync(true);

        var service = mock.Object;

        // Act
        var result = await service.Add(new TestEntity());

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Update_ShouldThrowException()
    {
        // Arrange
        var mock = new Mock<ICudRepository>();

        mock.Setup(repo => repo.Update(It.IsAny<TestEntity>()))
            .ThrowsAsync(new Exception("Erro ao atualizar"));

        var repo = mock.Object;

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => repo.Update(new TestEntity()));
        Assert.Equal("Erro ao atualizar", ex.Message);
    }

    [Fact]
    public async Task TransactionMethods_ShouldReturnExpectedResults()
    {
        var mock = new Mock<ICudRepository>();

        mock.Setup(x => x.BeginTransactionAsync()).ReturnsAsync(true);
        mock.Setup(x => x.CommitTransactionAsync()).ReturnsAsync(false);
        mock.Setup(x => x.RollbackTransactionAsync()).ReturnsAsync(true);

        var repo = mock.Object;

        Assert.True(await repo.BeginTransactionAsync());
        Assert.False(await repo.CommitTransactionAsync());
        Assert.True(await repo.RollbackTransactionAsync());
    }
}
