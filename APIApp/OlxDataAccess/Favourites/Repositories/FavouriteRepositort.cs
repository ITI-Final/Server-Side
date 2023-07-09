namespace OlxDataAccess.Favourits.FavouritRepositories
{
    public class FavouriteRepositort : BaseRepository<Favorite>, IFavouriteRepositort
    {
        public FavouriteRepositort(OLXContext context) : base(context)
        {
        }
    }
}
