using Microsoft.EntityFrameworkCore;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Repositories;
using Project.Api.Infra.Context;

namespace Project.Api.Infra.Repositories
{
    public class UserRepository : CudRepository, IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetByUuid(Guid uuid)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Uuid.ToString().ToUpper() == uuid.ToString().ToUpper() && x.IsActive);
        }

        public async Task<UserEntity> GetById(long id)
        {
            return await _context.Users.AsNoTracking()
                                          .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _context.Users.AsNoTracking()
                                          .Where(x => x.IsActive)
                                          .ToListAsync();
        }

        public async Task<List<UserEntity>> GetByIds(IEnumerable<long> userIds)
        {
            return await _context.Users
                .Where(u => userIds.Contains(u.Id))
                .ToListAsync();
        }
    }
}