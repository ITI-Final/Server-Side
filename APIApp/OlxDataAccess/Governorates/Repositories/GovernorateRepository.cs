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

        #endregion

        #region GetByid
        public override async Task<Governorate> GetById(int id)
        {
            return await _dbContext.Governorates.Include(o => o.Cities).FirstOrDefaultAsync(g => g.Id == id);
        }
        #endregion
        #endregion
    }
}
