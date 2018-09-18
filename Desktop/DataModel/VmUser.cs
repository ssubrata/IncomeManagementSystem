using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.DataModel
{
   public class VmUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string NationalId { get; set; }
        public string Photo { get; set; }
        public Nullable<int> RoleId { get; set; }

        public virtual VmRole VmRole { get; set; }
    }
}
