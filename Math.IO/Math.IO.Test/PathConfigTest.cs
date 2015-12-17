using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Math.IO.Test
{
    [TestFixture]
    public class PathConfigTest
    {
        [Test]
        public void TestConceptPath()
        {
            Assert.NotNull(PathConfig.RetrieveConceptPath());
        }
    }
}
