using Microsoft.EntityFrameworkCore;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Repositories;
using Project.Api.Infra.Context;

namespace Project.Api.Infra.Repositories
{
    public class TaskAuditRepository : CudRepository, ITaskAuditRepository
    {
        private readonly DataContext _context;

        public TaskAuditRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TaskAuditEntity> GetByUuid(Guid uuid)
        {
            return await _context.TasksAudit.AsNoTracking()
                                 .FirstOrDefaultAsync(x => x.Uuid == uuid);
        }

        public async Task<TaskAuditEntity> GetById(long id)
        {
            return await _context.TasksAudit.AsNoTracking()
                                          .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TaskAuditEntity>> GetAll()
        {
            return await _context.TasksAudit.AsNoTracking()
                                          .ToListAsync();
        }
    }
}