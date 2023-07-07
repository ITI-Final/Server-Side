namespace APIApp.Repositories.BaseRepository
{
    
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
            if (await IsExist(id) != null)
            {
                _dbSet.Remove(await IsExist(id));
            }
            await _context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task Update(int id, T entity)
        {
            if (await IsExist(id) != null)
            {

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<T> IsExist(int id)
        {
            return await _dbSet.FindAsync(id);

        }
    }
}
