using System.Linq;
using Desktop;

namespace DAL.Repository.Implement
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TruckDbContext context):base(context)
        {

        }
        public User GetById(int id) => TrcukDbContext.Users.SingleOrDefault(f => f.Id == id);

        public User GetByUserName(string userName) => TrcukDbContext.Users.SingleOrDefault(f => f.UserName.ToLower() == userName.ToLower());

        public User ExistUser(string userName, string Password)
        {
            return TrcukDbContext.Users.SingleOrDefault(f => f.UserName.ToUpper() == userName.ToUpper() && f.Password.ToUpper() == Password.ToUpper());
        }

        public TruckDbContext TrcukDbContext => Context as TruckDbContext;
    }
}
