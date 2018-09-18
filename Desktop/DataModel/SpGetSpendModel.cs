using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.DataModel
{
  public  class SpGetSpendModel
    {
        public int SpendId { get; set; }
        public string BookNo { get; set; }
        public string ReciptNo { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Smame { get; set; }
        public DateTime Date { get; set; }
    }
}
