using OlxDataAccess.Categories.Repositories;
using OlxDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlxDataAccess.Choices.Repositories
{
    public class ChoiceRepository : BaseRepository<Choice>, IChoiceRepository
    {
        private readonly OLXContext _dbContext;
        public ChoiceRepository(OLXContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
