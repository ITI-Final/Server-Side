namespace OlxDataAccess.Users.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(OLXContext context) : base(context)
        {
        }


        public override async Task<User> GetById(int id)
        {
            return await _dbSet.Include(c => c.Company).Include(f => f.Favorites).FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbSet.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> Login(string email, string password)
        {
            return await _context
                .Users
                .FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
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
