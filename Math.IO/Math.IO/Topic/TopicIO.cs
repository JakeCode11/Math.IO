using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathIO
{
    public class TopicNode
    {
        public string Topic { get; set; }

        public List<TopicNode> Dependencies { get; set; } //TODO 

        public TopicNode(string _topic)
        {
            Topic = _topic;
        }
    }
}
