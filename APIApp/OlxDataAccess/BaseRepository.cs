namespace OlxDataAccess
{


    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Fileds
        protected readonly OLXContext _context;
        protected readonly DbSet<T> _dbSet;
        #endregion

        #region Constructors
        public BaseRepository(OLXContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        #endregion

        #region Methods

        #region Get
        public virtual async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

        public virtual async Task<T> GetById(int id) => await _dbSet.FindAsync(id);
        #endregion

        #region Add
        public virtual async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Update
        public virtual async Task Update(int id, T entity)
        {
            #region V1
            //if (!await IsExist(id))
            //    return;

            //_context.Entry(entity).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            #endregion

            #region V2
            var existingEntity = await _context.Set<T>().FindAsync(id);
            if (existingEntity == null)
                return;

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            #endregion

        }

        #endregion

        #region Delete
        public virtual async Task DeleteById(int id)
        {
            var entity = await GetEntity(id);
            if (entity == null)
                return;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region ISExisted
        private async Task<bool> IsExist(int id) => await GetEntity(id) != null;

        private async Task<T> GetEntity(int id) => await _dbSet.FindAsync(id);
        #endregion

        #endregion
    }
}
