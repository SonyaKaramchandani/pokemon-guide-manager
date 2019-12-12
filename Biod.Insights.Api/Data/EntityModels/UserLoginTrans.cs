using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class UserLoginTrans
    {
        public int UserLoginTransId { get; set; }
        public string UserId { get; set; }
        public DateTime LoginDateTime { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
