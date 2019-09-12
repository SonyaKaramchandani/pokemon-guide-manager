using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biod.Zebra.Library.Infrastructures
{
    public class CustomIdentityResult
    {
        public bool Succeeded { get; set; }
        public object[] Errors { get; set; }
    }

}
