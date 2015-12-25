using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.IO
{
    public class ConceptEdge
    {
        public ConceptNode SourceConcept { get; set; }

        public ConceptNode TargetConcept { get; set; }

        public ConceptEdge(ConceptNode _source, ConceptNode _target)
        {
            SourceConcept = _source;
            TargetConcept = _target;
        }


    }
}
