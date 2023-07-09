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
    }
}
