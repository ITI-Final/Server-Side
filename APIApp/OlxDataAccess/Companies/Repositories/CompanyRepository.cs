namespace OlxDataAccess.Companies.Repositories
{

    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(OLXContext context) : base(context)
        {
        }
    }
}
