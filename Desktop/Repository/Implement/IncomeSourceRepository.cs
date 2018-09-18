using System.Linq;
using Desktop;

namespace DAL.Repository.Implement
{
    public class IncomeSourceRepository : Repository<IncomeSource>, IIncomeSourceReposiotry
    {
        public IncomeSourceRepository(TruckDbContext context) : base(context)
        {

        }
        public IncomeSource GetById(int id)
        {
             return TruckDbContext.IncomeSources.SingleOrDefault(f => f.IncomeSourceId == id);
        }

        public IncomeSource GetByName(string name)
        {
            return TruckDbContext.IncomeSources.SingleOrDefault(f => f.Name.ToLower() == name.ToLower());
        }

        public TruckDbContext TruckDbContext
        {
            get
            {
                return Context as TruckDbContext;
            }
        }
    }
}
