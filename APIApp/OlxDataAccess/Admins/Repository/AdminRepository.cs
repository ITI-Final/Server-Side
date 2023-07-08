using OlxDataAccess.DBContext;
using OlxDataAccess.Models;

namespace OlxDataAccess.Admins.Repository
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        #region Fields
        private readonly OLXContext _context;
        #endregion

        #region Constructors
        public AdminRepository(OLXContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public override Task<Admin> GetById(int id)
        {
            return _context
                .Admins
                .Include(a => a.Permissions)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async override Task DeleteById(int id)
        {
            if (await IsExisted(id) != null)
            {
                _context.Admins.Remove(await IsExisted(id));
                await _context.SaveChangesAsync();
            }
        }

        private async Task<Admin> IsExisted(int id)
        {
            return await _context.Admins.FirstOrDefaultAsync(q => q.Id == id);
        }
        #endregion
    }
}
