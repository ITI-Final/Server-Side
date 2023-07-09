namespace OlxDataAccess.Fields.Repositories
{
    public class FieldRepository : BaseRepository<Field>, IFieldRepository
    {
        private readonly OLXContext _dbContext;
        public FieldRepository(OLXContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
