using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Surveillance.ViewModels
{
    public class ArticleCaseCountMongo
    {
        public int suspected { get; set; }
        public int confirmed { get; set; }
        public int reported { get; set; }
        public int deaths { get; set; }
    }

    public class ArticleDiseaseMongo
    {
        public int diseaseId { get; set; }
        public ArticleCaseCountMongo newCaseCounts { get; set; }
        public ArticleCaseCountMongo totalCaseCounts { get; set; }
    }
    public class ArticleLocationAndCaseCountsMongo
    {
        public int geonameId { get; set; }
        public List<ArticleDiseaseMongo> disease { get; set; }

        //public int diseaseId { get; set; }
        //public ArticleCaseCountMongo newCaseCounts { get; set; }
        //public ArticleCaseCountMongo totalCaseCounts { get; set; }
    }

    public class ArticleMetadataSyncMongoDB
    {
        public int spamFilterLabel { get; set; }
        //public string userLastModifiedDate { get; set; }
        public List<ArticleLocationAndCaseCountsMongo> locations { get; set; }
        public List<int> diseaseIds { get; set; }
        public List<int> associatedEventIds { get; set; }
        public string notes { get; set; }
    }
}