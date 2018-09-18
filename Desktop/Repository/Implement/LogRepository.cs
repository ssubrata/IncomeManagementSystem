using DAL.Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Repository.Implement
{
    public class LogRepository: Repository<Log>, ILogRepository
    {
        public LogRepository(TruckDbContext context) : base(context)
        {

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
