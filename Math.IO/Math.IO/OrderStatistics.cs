using MathIO.Concept;
using NUnit.Framework;

namespace MathIO
{
    [TestFixture]
    public class MathOrderStatistic
    {
        [Test]
        public void OrderStatistic()
        {
            //number of topics:
            Assert.True(MathInputs.Instance.Topics.Count == 8);

            //number of concepts:
            Assert.True(MathInputs.Instance.ConceptGraph.VertexCount == 92);

            //number of problems:
            Assert.True(MathInputs.Instance.Problems.Count == 186);

            //scaffolding
            //TODO
        }
    }
}