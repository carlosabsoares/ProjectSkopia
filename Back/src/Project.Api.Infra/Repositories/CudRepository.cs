using Project.Api.Domain.Repositories;
using Project.Api.Infra.Context;

namespace Project.Api.Infra.Repositories
{
    public class CudRepository : ICudRepository
    {
        private readonly DataContext _context;

        public CudRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Add<T>(T entity) where T : class
        {
            _context.Add(entity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> Update<T>(T entity) where T : class
        {
            try
            {
                _context.Update(entity);
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public virtual async Task<T> FindById<T>(Guid id) where T : class
        {
            var _return = (await _context.Set<T>().FindAsync(id));

            return _return;
        }

        public async Task<List<T>> FindAll<T>() where T : class
        {
            var _return = _context.Set<T>().ToList();

            return _return;
        }

        public async Task<bool> BeginTransactionAsync()
        {
            bool _return;

            try
            {
                await _context.Database.BeginTransactionAsync();
                _return = true;
            }
            catch (Exception)
            {
                _return = false;
            }

            return _return;
        }

        public async Task<bool> CommitTransactionAsync()
        {
            bool _return;

            try
            {
                await _context.Database.CommitTransactionAsync();
                _return = true;
            }
            catch (Exception)
            {
                _return = false;
            }

            return _return;
        }

        public async Task<bool> RollbackTransactionAsync()
        {
            bool _return;

            try
            {
                await _context.Database.RollbackTransactionAsync();
                _return = true;
            }
            catch (Exception)
            {
                _return = false;
            }

            return _return;
        }
    }
}