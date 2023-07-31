namespace OlxDataAccess.Favourits.FavouritRepositories
{
    public interface IFavouriteRepositort : IBaseRepository<Favorite>
    {
        Task<Favorite> GetByPostAndUser(int user_id, int post_id);
    }
}
