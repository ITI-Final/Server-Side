namespace OlxDataAccess
{
    public class UserConnectionRepository : BaseRepository<User_Connection>, IUserConnectionRepository
    {
        public UserConnectionRepository(OLXContext context) : base(context)
        {
        }

        public override async Task<User_Connection> GetById(int id)
        {
            return await _context.User_Connections.FirstOrDefaultAsync(uc => uc.User_ID == id);
        }
    }
}
