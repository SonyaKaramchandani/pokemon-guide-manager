﻿using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class UserRolesTransLog
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public DateTime ModifiedUtcdatetime { get; set; }
        public string Description { get; set; }
    }
}
