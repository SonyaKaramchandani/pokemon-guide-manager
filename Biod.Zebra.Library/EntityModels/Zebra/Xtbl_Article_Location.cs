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
    
    public partial class Xtbl_Article_Location
    {
        public string ArticleId { get; set; }
        public int LocationGeoNameId { get; set; }
    
        public virtual ProcessedArticle ProcessedArticle { get; set; }
    }
}