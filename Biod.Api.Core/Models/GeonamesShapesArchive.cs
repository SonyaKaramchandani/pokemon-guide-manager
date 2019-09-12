using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class GeonamesShapesArchive
    {
        public int GeonameId { get; set; }
        public DateTime? ShapeLastModified { get; set; }
    }
}
