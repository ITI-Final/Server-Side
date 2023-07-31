using OlxDataAccess.Models;

namespace OlxDataAccess.Favourits.FavouritRepositories
{
    public class FavouriteRepositort : BaseRepository<Favorite>, IFavouriteRepositort
    {
        private OLXContext _context;
        public FavouriteRepositort(OLXContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Favorite> GetByPostAndUser(int user_id, int post_id)
        {
            return await _context.Favorites.Include(a=> a.Post).FirstOrDefaultAsync(a => a.User_Id == user_id && a.Post_Id == post_id);
        }

    }
}
