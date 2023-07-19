namespace OlxDataAccess.Choices.Repositories
{
    public class ChoiceRepository : BaseRepository<Choice>, IChoiceRepository
    {
        private readonly OLXContext _dbContext;
        public ChoiceRepository(OLXContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
