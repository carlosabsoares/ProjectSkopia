namespace Project.Api.Domain.Repositories
{
    public interface ICudRepository
    {
        Task<bool> Add<T>(T entity) where T : class;

        Task<bool> Update<T>(T entity) where T : class;

        Task<bool> Delete<T>(T entity) where T : class;

        Task<bool> BeginTransactionAsync();

        Task<bool> CommitTransactionAsync();

        Task<bool> RollbackTransactionAsync();
    }
}