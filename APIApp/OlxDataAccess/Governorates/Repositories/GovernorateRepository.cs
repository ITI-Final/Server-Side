namespace OlxDataAccess.Governorates.Repositories
{
    public class GovernorateRepository : BaseRepository<Governorate>, IGovernorateRepository
    {
        #region ctor
        private readonly OLXContext _dbContext;
        public GovernorateRepository(OLXContext context) : base(context)
        {
            _dbContext = context;
        }
        #endregion
        #region get
        #region GetAll
        public override async Task<IEnumerable<Governorate>> GetAll()
        {
            return await _dbContext.Governorates.Include(o => o.Cities).ToListAsync();
        }

        public override async Task<IQueryable<Governorate>> GetAllWithPagination(int page, int pageSize)
        {
            return _dbContext.Governorates
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(g => g.Cities);
        }


        public IQueryable<Governorate> GetAllWithSorting(int page, int pageSize, bool? isSortingAsc)
        {

            if (isSortingAsc.HasValue)
                if (isSortingAsc == true)
                    return _dbContext.Governorates.OrderBy(g => g.Governorate_Name_En).Skip((page - 1) * pageSize).Take(pageSize);
                else
                    return _dbContext.Governorates.OrderByDescending(g => g.Governorate_Name_En).Skip((page - 1) * pageSize).Take(pageSize);

            return _dbContext.Governorates
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(g => g.Cities);
        }

        public async Task<IEnumerable<Governorate>> GetAllWithOutCities()
        {
            return await _dbContext.Governorates.ToListAsync();
        }

        #endregion

        #region GetByid
        public override async Task<Governorate> GetById(int id)
        {
            return await _dbContext.Governorates.Include(o => o.Cities).FirstOrDefaultAsync(g => g.Id == id);
        }
        #endregion
        #endregion

        //public override async Task<IEnumerable<Governorate>> GetAllWithPagination(int page, int pageSize)
        //{

        //    List<Governorate>? governorate = await _dbContext.Governorates.ToListAsync();
        //    int skip = (page - 1) * pageSize;
        //    //IEnumerable<Governorate>? results = null;
        //    //for (int i = 0; i < governorate.Count / 10 + 1; i++)
        //    //{
        //    //    results = governorate.Skip(i * pageSize).Take(skip);
        //    //}

        //    //return results;

        //    return await _dbContext.Governorates.OrderBy(g => g.Id).Skip(pageSize).Take(pageSize).ToListAsync();
        //}

    }
}
