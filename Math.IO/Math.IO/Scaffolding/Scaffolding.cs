using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathIO
{
    public class Scaffolding
    {
        public string ProblemId   { get; set; }
        public string ProblemText { get; set; }
        public List<ScaffoldingStep> Steps { get; set; }

        public Scaffolding()
        {
            Steps = new List<ScaffoldingStep>();
        }
    }
}
