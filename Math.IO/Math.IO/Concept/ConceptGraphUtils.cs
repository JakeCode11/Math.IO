using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Algorithms;

namespace MathIO.Concept
{
    public static class ConceptGraphUtils
    {
        public static ConceptNode SearchConceptNode(this BidirectionalGraph<ConceptNode, Edge<ConceptNode>> graph, string concept)
        {
            return graph.Vertices.FirstOrDefault(cn => cn.Concept.Equals(concept));
        }

        public static List<ConceptNode> SearchConceptNodeWithErrorConcepts(
            this BidirectionalGraph<ConceptNode, Edge<ConceptNode>> graph)
        {
            return graph.Vertices.Where(cn => cn.ErrorConcept != null).ToList();
        }
    }
}
