
using Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace DAL.Repository.Implement
{
    public class IncomeRepository : Repository<Income>, IIncomeRepository
    {
        public IncomeRepository(TruckDbContext context):base(context)
        {
            
        }
        public IEnumerable<Income> GetByDate(DateTime date)
        {
            return TruckDbContext.Incomes.Where(f => f.Date.Equals(date));
        }

        public Income GetById(int id)
        {
            return TruckDbContext.Incomes.SingleOrDefault(f=>f.IncomeId==id);
        }

        public IEnumerable<Income> GetAllWithIncomeSource()
        {
            throw new NotImplementedException();
        }

      

        public TruckDbContext TruckDbContext
        {
            get
            {
                return Context  as TruckDbContext;
            }
        }

    }
}
