
using Desktop;

namespace DAL.Repository.Implement
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(TruckDbContext context) : base(context)
        {

        }
        public TruckDbContext TrcukDbContext
        {
            get
            {
                return Context as TruckDbContext;
            }
        }
    }
}
