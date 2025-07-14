using Microsoft.EntityFrameworkCore;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Repositories;
using Project.Api.Infra.Context;

namespace Project.Api.Infra.Repositories
{
    public class TaskRepository : CudRepository, ITaskRepository
    {
        private readonly DataContext _context;

        public TaskRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TaskEntity> GetByUuid(Guid uuid)
        {
            return await _context.Tasks.AsNoTracking()
                                       .Include(x => x.Project)
                                       .Include(x => x.Author)
                                       .FirstOrDefaultAsync(x => x.Uuid == uuid);
        }

        public async Task<TaskEntity> GetById(long id)
        {
            return await _context.Tasks.AsNoTracking()
                                       .Include(x => x.Project)
                                       .Include(x => x.Author)
                                       .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TaskEntity>> GetAll()
        {
            return await _context.Tasks.AsNoTracking()
                                       .Include(x => x.Project)
                                       .Include(x => x.Author)
                                       .ToListAsync();
        }

        public async Task<IEnumerable<TaskEntity>> GetPerformanceReport(int days)
        {
            return await _context.Tasks.AsNoTracking()
                                       .Include(x => x.Project)
                                       .Include(x => x.Author)
                                       .Where(x => x.ExpirationDate >= DateTime.UtcNow.AddDays(-days) && x.Status == Domain.Enum.StatusTaskType.Completed)
                                       .ToListAsync();
        }
    }
}