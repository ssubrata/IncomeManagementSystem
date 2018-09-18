using System.Data.Entity;

namespace Desktop
{
    public partial  class TruckDbContext:DbContext
    {
        public TruckDbContext(string CON):base(CON)
        {

        }
    }
}
