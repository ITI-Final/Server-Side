namespace OlxDataAccess.Fields.Repositories
{
    public class FieldRepository : BaseRepository<Field>, IFieldRepository
    {
        #region ctor
        private readonly OLXContext _dbContext;
        public FieldRepository(OLXContext context) : base(context)
        {
            _dbContext = context;
        }
        #endregion


        #region GetById
        public override Task<Field> GetById(int id)
        {
            return _dbContext.Fields.Include(o => o.Choices).FirstOrDefaultAsync(o => o.Id == id)!;
        }
        #endregion
    }


}
