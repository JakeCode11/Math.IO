using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.IO
{
    public class ConceptNode
    {
        public string Concept { get; set; }

        public double learntProb { get; set; }

        public TopicNode Topic { get; set; } 

        public ConceptNode(string concept)
        {
            Concept = concept;
        }
    }
}
