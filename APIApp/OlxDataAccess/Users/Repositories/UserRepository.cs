namespace OlxDataAccess.Users.Repositories
{
   

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(OLXContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _dbSet.Include(c=>c.Companies).ToListAsync();
        }

        public override async Task<User> GetById(int id)
        {
            return await _dbSet.Include(c => c.Company).Include(f => f.Favorites).FirstOrDefaultAsync(u=>u.Id == id);
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbSet.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        
    }
}
