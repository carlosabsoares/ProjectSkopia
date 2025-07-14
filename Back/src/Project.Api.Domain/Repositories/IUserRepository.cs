using Project.Api.Domain.Entities;

namespace Project.Api.Domain.Repositories
{
    public interface IUserRepository : ICudRepository
    {
        Task<UserEntity> GetByUuid(Guid uuid);

        Task<UserEntity> GetById(long id);

        Task<IEnumerable<UserEntity>> GetAll();

        Task<List<UserEntity>> GetByIds(IEnumerable<long> userIds);
    }
}