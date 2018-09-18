using Desktop;
using System;
using System.Collections.Generic;

namespace DAL.Repository
{
    public interface IIncomeRepository:IRepository<Income>
    {
        IEnumerable<Income> GetByDate(DateTime date);
        IEnumerable<Income> GetAllWithIncomeSource( );
        Income GetById(int id);
       
    }
}
