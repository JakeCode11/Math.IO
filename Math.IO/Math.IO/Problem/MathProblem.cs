using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathIO
{
    public class MathProblem
    {
        public string ProblemId { get; set; }
        public string Problem   { get; set; }
        public List<object> Semantic { get; set; }
        public List<string> Concept { get; set; }
        public string Topic { get; set; }
        public List<string> Solutions { get; set; }

        public Scaffolding Scaffolding { get; set; }

        public MathProblem()
        {
            Semantic = new List<object>();
            Concept  = new List<string>();
            Solutions = new List<string>();
        }
    }
}
