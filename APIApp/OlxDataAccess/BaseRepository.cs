

namespace OlxDataAccess
{
    using OlxDataAccess.DBContext;

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly OLXContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(OLXContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public virtual async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteById(int id)
        {
            var entity = await GetEntity(id);
            if (entity == null)
                return;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

        public virtual async Task<T> GetById(int id) => await _dbSet.FindAsync(id);

        public virtual async Task Update(int id, T entity)
        {
            if (!await IsExist(id))
                return;


            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private async Task<bool> IsExist(int id) => await GetEntity(id) != null;

        private async Task<T> GetEntity(int id) => await _dbSet.FindAsync(id);

    }
}
