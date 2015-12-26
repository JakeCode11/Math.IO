using MathIO.Concept;
using NUnit.Framework;
using System.Collections.Generic;

namespace MathIO.Test
{
    [TestFixture]
    public class MathIOTest
    {
        [Test]
        public void TopicTest()
        {
            Assert.True(MathInputs.Instance.Topics.Count == 8);
        }

        [Test]
        public void ConceptsTest()
        {
            Assert.True(MathInputs.Instance.ConceptGraph.VertexCount == 69 );
        }

        [Test]
        public void ErrorConceptsTest()
        {
            var nodesWithError = 
                MathInputs.Instance.ConceptGraph.SearchConceptNodeWithErrorConcepts();
            
            Assert.True(nodesWithError.Count == 1);
        }

        [Test]
        public void ProblemsTest()
        {
            Assert.True(MathInputs.Instance.Problems.Count ==48);
        }

        [Test]
        public void ProblemScaffoldingTest()
        {
            //TODO Josh add here.
        }
    }
}