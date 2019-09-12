using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class Geonames
    {
        public Geonames()
        {
            XtblArticleLocation = new HashSet<XtblArticleLocation>();
            XtblArticleLocationDisease = new HashSet<XtblArticleLocationDisease>();
            XtblEventLocation = new HashSet<XtblEventLocation>();
        }

        public int GeonameId { get; set; }
        public string Name { get; set; }
        public int? LocationType { get; set; }
        public int? Admin1GeonameId { get; set; }
        public int? CountryGeonameId { get; set; }
        public string DisplayName { get; set; }
        public string Alternatenames { get; set; }
        public DateTime ModificationDate { get; set; }
        public string FeatureCode { get; set; }
        public string CountryName { get; set; }

        public ICollection<XtblArticleLocation> XtblArticleLocation { get; set; }
        public ICollection<XtblArticleLocationDisease> XtblArticleLocationDisease { get; set; }
        public ICollection<XtblEventLocation> XtblEventLocation { get; set; }
    }
}
