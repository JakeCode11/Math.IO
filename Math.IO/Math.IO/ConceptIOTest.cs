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
    public class ConceptIOTest
    {
        [Test]
        public void GenerateGraph()
        {
            var conceptIo = new ConceptIO(PathConfig.RetrieveConceptPath());
            Assert.True(conceptIo.ConceptCount > 10);
        }
    }
}