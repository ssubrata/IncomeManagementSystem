//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Desktop
{
    using System;
    using System.Collections.Generic;
    
    public partial class Spend
    {
        public int SpendId { get; set; }
        public string BookNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ReciptNo { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int SourceId { get; set; }
    
        public virtual SpendSource SpendSource { get; set; }
    }
}
