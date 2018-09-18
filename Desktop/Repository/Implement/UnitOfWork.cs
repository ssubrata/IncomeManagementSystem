


using Desktop;
using Desktop.Repository;
using Desktop.Repository.Implement;

namespace DAL.Repository.Implement
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TruckDbContext _context;
        public UnitOfWork(TruckDbContext context)
        {
            _context = context;
            Incomes = new IncomeRepository(_context);
            IncomeSource = new IncomeSourceRepository(_context);
            Spends = new SpendRepository(_context);
            SpendSource = new SpendSourceRepository(_context);
            User = new UserRepository(_context);
            Role = new RoleRepository(_context);
            Log = new LogRepository(_context);
        }
        public IIncomeRepository Incomes { get;  set; }
        public IIncomeSourceReposiotry IncomeSource { get;  set; }
        public ISpendRepository Spends { get;  set; }
        public ISpendSourceRepository SpendSource { get;  set; }
        public IUserRepository User { get;  set; }
        public IRoleRepository Role { get;  set ; }
        public ILogRepository Log { get;  set ; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
