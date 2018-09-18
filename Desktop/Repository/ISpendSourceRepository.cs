using Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
   public interface ISpendSourceRepository: IRepository<SpendSource>
    {
        SpendSource GetById(int id);
        SpendSource GetByName(string name);

    }
}
