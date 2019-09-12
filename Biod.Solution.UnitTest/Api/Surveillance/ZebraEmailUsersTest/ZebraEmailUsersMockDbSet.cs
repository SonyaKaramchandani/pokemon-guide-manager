using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Biod.Solution.UnitTest.MockDbContext;

namespace Biod.Solution.UnitTest.Api
{
    class ZebraEmailUsersMockDbSet
    {
        private static readonly Random random = new Random();

        public Mock<BiodZebraEntities> MockContext { get; set; }

        public ZebraEmailUsersMockDbSet()
        {

        }
    }
}
