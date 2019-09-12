using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Zebra.Library.Infrastructures
{
    public class AzureMLServiceResult
    {
        public Results Results { get; set; }
    }
    public class Value
    {
        public List<List<string>> Values { get; set; }
    }

    public class Output1
    {
        public string type { get; set; }
        public Value value { get; set; }
    }

    public class Value2
    {
        public List<List<string>> Values { get; set; }
    }

    public class Output2
    {
        public string type { get; set; }
        public Value2 value { get; set; }
    }

    public class Results
    {
        public Output1 output1 { get; set; }
        public Output2 output2 { get; set; }
    }

    //public class RootObject
    //{
    //    public Results Results { get; set; }
    //}
}