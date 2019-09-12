﻿using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class XtblArticleLocation
    {
        public string ArticleId { get; set; }
        public int LocationGeoNameId { get; set; }

        public ProcessedArticle Article { get; set; }
        public Geonames LocationGeoName { get; set; }
    }
}