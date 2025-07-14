using Project.Api.Domain.Entities;

namespace Project.Api.Domain.Repositories
{
    public interface ITaskAuditRepository : ICudRepository
    {
        Task<TaskAuditEntity> GetByUuid(Guid uuid);

        Task<TaskAuditEntity> GetById(long id);

        Task<IEnumerable<TaskAuditEntity>> GetAll();
    }
}