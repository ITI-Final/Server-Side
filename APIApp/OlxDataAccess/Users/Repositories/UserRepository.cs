namespace OlxDataAccess.Users.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(OLXContext context) : base(context)
        {
        }


        public override async Task<User> GetById(int id)
        {
            return await _dbSet
                .Include(c => c.Companies)
                .Include(f => f.Favorites)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public object GetUserChats(int id)
        {
            var result = from cm in _context.Chat_Messages
                         join sender in _context.Users on cm.Sender_ID equals sender.Id
                         join receiver in _context.Users on cm.Receiver_ID equals receiver.Id
                         where sender.Id == id
                         group receiver by new { receiver.Id, receiver.Name } into g
                         select new
                         {
                             ReceiverId = g.Key.Id,
                             ReceiverName = g.Key.Name
                         };

            return result;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbSet.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> Login(string email)
        {
            return await _context
                .Users
                .FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _context.Users.AnyAsync(a => a.Email == email);
        }

        public async Task Register(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }


    }
}
