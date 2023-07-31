namespace OlxDataAccess.Governorates.Repositories
{
    public interface IGovernorateRepository : IBaseRepository<Governorate>
    {
        Task<IEnumerable<Governorate>> GetAllWithOutCities();
        IQueryable<Governorate> GetAllWithSorting(int page, int pageSize, bool? isSortingAsc);
    }
}
