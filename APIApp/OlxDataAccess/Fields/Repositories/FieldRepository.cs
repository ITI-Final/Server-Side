using OlxDataAccess.Categories.Repositories;
using OlxDataAccess.DBContext;
using OlxDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlxDataAccess.Fields.Repositories
{
    public class FieldRepository : BaseRepository<Field>, IFieldRepository
    {
        #region ctor
        private readonly OLXContext _dbContext;
        public FieldRepository(OLXContext context) : base(context)
        {
            _dbContext = context;
        }
        #endregion
    }
}
