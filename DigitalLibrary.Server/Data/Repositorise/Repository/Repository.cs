using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbDigitalLibraryContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbDigitalLibraryContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void DeleteAsync(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }    
        }

        public void EditAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
