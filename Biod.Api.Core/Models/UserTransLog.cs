﻿using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class UserTransLog
    {
        public string UserId { get; set; }
        public DateTime ModifiedUtcdatetime { get; set; }
        public string ModificationDescription { get; set; }
    }
}