using OlxDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OlxDataAccess.Models;
using OlxDataAccess.DBContext;

namespace OlxDataAccess.Governorates.Repositories
{
    public class GovernorateRepository : BaseRepository<Governorate>, IGovernorateRepository
    {
        #region ctor
        private readonly OLXContext _dbContext;
        public GovernorateRepository(OLXContext context) : base(context)
        {
            _dbContext = context;
        }
        #endregion
        #region get
        #region GetAll
        public override  async Task<IEnumerable<Governorate>> GetAll()
        {
            return await _dbContext.Governorates.Include(o => o.Cities).ToListAsync();
        }
        #endregion
        #region GetBYid
        public override async Task<Governorate> GetById(int id)
        {
            Governorate? governorate = await _dbContext.Governorates.Include(o => o.Cities).FirstOrDefaultAsync(g => g.Id == id);
            return governorate;
        }
        #endregion
        #endregion
    }
}
