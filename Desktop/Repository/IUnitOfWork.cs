using Desktop.Repository;
using System;

namespace DAL.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        IIncomeRepository Incomes { get;  }
        IIncomeSourceReposiotry IncomeSource { get; }
        ISpendRepository Spends { get;  set; }
        ISpendSourceRepository SpendSource { get;  set; }
        IUserRepository User { get;  set; }
        IRoleRepository Role { get;  set; }
        ILogRepository Log { get;  set; }
        int Complete();
    }
}
