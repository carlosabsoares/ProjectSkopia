using Project.Api.Domain.Entities;

namespace Project.Api.Domain.Repositories
{
    public interface ITaskRepository : ICudRepository
    {
        Task<TaskEntity> GetByUuid(Guid uuid);

        Task<TaskEntity> GetById(long id);

        Task<IEnumerable<TaskEntity>> GetAll();

        Task<IEnumerable<TaskEntity>> GetPerformanceReport(int days);
    }
}