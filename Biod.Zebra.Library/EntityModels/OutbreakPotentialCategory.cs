//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biod.Zebra.Library.EntityModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class OutbreakPotentialCategory
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public string Rule { get; set; }
        public bool NeedsMap { get; set; }
        public string MapThreshold { get; set; }
        public string EffectiveMessageDescription { get; set; }
        public string EffectiveMessage { get; set; }
        public bool IsLocalTransmissionPossible { get; set; }
    }
}
