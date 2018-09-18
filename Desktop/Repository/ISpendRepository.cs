using System;
using System.Collections.Generic;

using Desktop;

namespace DAL.Repository
{
   public interface ISpendRepository:IRepository<Spend>
    {
        IEnumerable<Spend> GetAllWithSpendSource();
        Spend GetById(int id);
    }
}
