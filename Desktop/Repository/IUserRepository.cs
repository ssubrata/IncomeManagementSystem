using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Desktop;

namespace DAL.Repository
{
    public interface IUserRepository:IRepository<User> 
    {
        User GetById(int id);
        User GetByUserName(string userName);
        User ExistUser(string userName,string Password);
    }
}
