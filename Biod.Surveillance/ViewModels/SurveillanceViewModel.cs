using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biod.Surveillance.ViewModels
{
    public class SurveillanceViewModel
    {

        public IList<DiseaseRoot> DiseaseClass { get; set; }
        public IList<EventItemModel> EventList { get; set; }
        public IList<ArticleGrid> ArticleClass { get; set; }
        public ArticleCount ArticleCounts { get; set; }
        public IList<SuggestedEventItemModel> SuggestEventList { get; set; }

        public MultiSelectList CountryList { get; set; }



        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Date { get; set; }
        public ArticleCount ArticleCount { get; internal set; }
    }
}