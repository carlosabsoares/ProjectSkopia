using Microsoft.EntityFrameworkCore;
using Project.Api.Domain.Entities;
using Project.Api.Domain.Repositories;
using Project.Api.Infra.Context;

namespace Project.Api.Infra.Repositories
{
    public class ProjectRepository : CudRepository, IProjectRepository
    {
        private readonly DataContext _context;

        public ProjectRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProjectEntity> GetByUuid(Guid uuid)
        {
            return await _context.Projects.AsNoTracking()
                                          .Include(x => x.Author)
                                          .Include(x => x.Tasks)
                                          .FirstOrDefaultAsync(x => x.Uuid.ToString().ToUpper() == uuid.ToString().ToUpper());
        }

        public async Task<ProjectEntity> GetById(long id)
        {
            return await _context.Projects.AsNoTracking()
                                          .Include(x => x.Author)
                                          .Include(x => x.Tasks)
                                          .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ProjectEntity>> GetAll()
        {
            return await _context.Projects.AsNoTracking()
                                          .Include(x => x.Author)
                                          .Include(x => x.Tasks)
                                          .ToListAsync();
        }
    }
}