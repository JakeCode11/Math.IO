using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;

namespace Math.IO
{
    public class MathInputs
    {
        #region Singleton

        private static MathInputs _instance;

        private MathInputs()
        {
            
        }

        public static MathInputs Instance
        {
            get
            {
                if (Instance == null)
                {
                    _instance = new MathInputs();
                }
                return _instance;
            }
        }

        public List<TopicNode> Topics { get; set; }

        public BidirectionalGraph<ConceptNode, Edge<ConceptNode>> ConceptGraph { get; set; }


        public List<Problem> 

        #endregion

    }
}
