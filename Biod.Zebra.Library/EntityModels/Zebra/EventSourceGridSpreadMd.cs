//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biod.Zebra.Library.EntityModels.Zebra
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventSourceGridSpreadMd
    {
        public int EventId { get; set; }
        public string GridId { get; set; }
        public int Cases { get; set; }
        public int MinCases { get; set; }
        public int MaxCases { get; set; }
    
        public virtual Event Event { get; set; }
    }
}
