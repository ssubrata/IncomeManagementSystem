using System;
using System.Collections.Generic;
using System.Linq;
using Desktop;

namespace DAL.Repository.Implement
{
    public class SpendRepository : Repository<Spend>, ISpendRepository
    {
        
        public SpendRepository(TruckDbContext context):base(context)
        {

        }
        public IEnumerable<Spend> GetAllWithSpendSource()
        {
            throw new NotImplementedException();
        }

        public Spend GetById(int id)
        {
            return TruckDbContext.Spends.SingleOrDefault(f => f.SpendId == id);
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
