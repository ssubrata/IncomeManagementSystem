using Desktop;
using System.Linq;

namespace DAL.Repository.Implement
{
    public class SpendSourceRepository : Repository<SpendSource>, ISpendSourceRepository
    {
        public SpendSourceRepository(TruckDbContext context) : base(context)
        {
        }

        public SpendSource GetById(int id)
        {
            return TruckDbContext.SpendSources.SingleOrDefault(f => f.SpendSourceId == id);
        }

        public SpendSource GetByName(string name)
        {
           return TruckDbContext.SpendSources.SingleOrDefault(f => f.Name.ToLower() == name.ToLower());
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
