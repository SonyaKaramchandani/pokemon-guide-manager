using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class UserLoginTrans
    {
        public int UserLoginTransId { get; set; }
        public string UserId { get; set; }
        public DateTime LoginDateTime { get; set; }

        public AspNetUsers User { get; set; }
    }
}
