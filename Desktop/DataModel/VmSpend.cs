using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.DataModel
{
   public class VmSpend
    {
        public int Id { get; set; }
        public string BookNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ReciptNo { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int SourceId { get; set; }

        public virtual VmSpendSource VmSpendSource { get; set; }
    }
}
