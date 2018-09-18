using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.DataModel
{
   public class VmSpendSource
    {
        public int SpendSourceId { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    }
}
