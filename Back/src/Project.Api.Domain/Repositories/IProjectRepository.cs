using Project.Api.Domain.Entities;

namespace Project.Api.Domain.Repositories
{
    public interface IProjectRepository : ICudRepository
    {
        Task<ProjectEntity> GetByUuid(Guid uuid);

        Task<ProjectEntity> GetById(long id);

        Task<IEnumerable<ProjectEntity>> GetAll();
    }
}