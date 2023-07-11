namespace OlxDataAccess.Governorates.Repositories
{
    public interface IGovernorateRepository : IBaseRepository<Governorate>
    {
        Task<IEnumerable<Governorate>> GetAllWithOutCities();
    }
}
