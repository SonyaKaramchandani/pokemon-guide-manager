﻿using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class EventGroupByFields
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string ColumnName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDefault { get; set; }
        public bool IsHidden { get; set; }
    }
}