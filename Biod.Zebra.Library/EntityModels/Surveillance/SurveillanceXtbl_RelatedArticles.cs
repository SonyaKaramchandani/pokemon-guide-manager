//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biod.Zebra.Library.EntityModels.Surveillance
{
    using System;
    using System.Collections.Generic;
    
    public partial class SurveillanceXtbl_RelatedArticles
    {
        public string MainArticleId { get; set; }
        public string RelatedArticleId { get; set; }
    
        public virtual SurveillanceProcessedArticle ProcessedArticle { get; set; }
    }
}