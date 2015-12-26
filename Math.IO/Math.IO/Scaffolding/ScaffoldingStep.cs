using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathIO
{
    public enum ScaffoldingType
    {
        Summary, Meta,Conceptscaffold, ProceduralScaffold, Solution
    }

    public class ScaffoldingStep
    {
        public string Id { get; set; }
        public ScaffoldingType Type { get; set; }
        public string Text { get; set; }
        //TODO BO do not think it is requied to add dependies property?
        public List<ScaffoldingStep> Dependencies { get; set; }
        public List<string> ConceptsRelated { get; set; }
        public List<ScaffoldingStep> Procedurals { get; set; } 
    }
}
