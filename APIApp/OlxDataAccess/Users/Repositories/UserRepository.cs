namespace OlxDataAccess.Users.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(OLXContext context) : base(context)
        {
        }

        #region Auth
        public async Task<User> Login(string email)
        {
            return await _context
                .Users
                .FirstOrDefaultAsync(a => a.Email == email);
        }


        public async Task Register(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _context.Users.AnyAsync(a => a.Email == email);
        }
        #endregion

        #region Get
        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public override async Task<User> GetById(int id)
        {
            return await _dbSet
                .Include(c => c.Companies)
                .Include(f => f.Favorites)
                .ThenInclude(d => d.Post)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public object GetUserChats(int id)
        {
            var result = (
                from cm in _context.Chat_Messages
                join sender in _context.Users on cm.Sender_ID equals sender.Id
                join receiver in _context.Users on cm.Receiver_ID equals receiver.Id
                where receiver.Id == id
                group sender by new { sender.Name, sender.Id } into g
                select new { Id = g.Key.Id, Name = g.Key.Name })
                .Union(
                from cm in _context.Chat_Messages
                join sender in _context.Users on cm.Sender_ID equals sender.Id
                join receiver in _context.Users on cm.Receiver_ID equals receiver.Id
                where sender.Id == id
                group receiver by new { receiver.Name, receiver.Id } into g
                select new { Id = g.Key.Id, Name = g.Key.Name }
                );

            return result;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbSet.Where(u => u.Email == email).FirstOrDefaultAsync();
        }
        #endregion

    }
}
