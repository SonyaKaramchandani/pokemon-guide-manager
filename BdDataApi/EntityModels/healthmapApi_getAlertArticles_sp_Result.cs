//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BdDataApi.EntityModels
{
    using System;
    
    public partial class healthmapApi_getAlertArticles_sp_Result
    {
        public string ArticleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> PublicationDate { get; set; }
        public string SourceUrl { get; set; }
        public string Author { get; set; }
        public string LocationName { get; set; }
        public Nullable<int> LocationTypeId { get; set; }
        public Nullable<int> SourceTypeId { get; set; }
        public System.DateTime LastUpdateTime { get; set; }
        public Nullable<double> Lat { get; set; }
        public Nullable<double> Long { get; set; }
        public string Point { get; set; }
        public Nullable<int> BdLocationTypeId { get; set; }
        public int BdCtryTeryId { get; set; }
        public int BdProvinceId { get; set; }
    }
}
