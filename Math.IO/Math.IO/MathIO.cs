using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MathIO.Concept;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;
using QuickGraph;

namespace MathIO
{
    public class MathInputs
    {
        #region Singleton

        private static MathInputs _instance;

        private MathInputs()
        {
            Topics = new List<TopicNode>();
            ConceptGraph = new BidirectionalGraph<ConceptNode, Edge<ConceptNode>>();
            Problems = new List<MathProblem>();
            Load();
        }

        public static MathInputs Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MathInputs();
                }
                return _instance;
            }
        }

        public List<TopicNode> Topics { get; set; }

        public BidirectionalGraph<ConceptNode, Edge<ConceptNode>> ConceptGraph { get; set; }

        public List<MathProblem> Problems { get; set; }

        #endregion

        #region Load

        public void Load()
        {
            LoadTopics();
            LoadConcepts();
            //LoadErrorConcepts();
            LoadProblems();
            LoadScaffolding();
        }

        private void LoadTopics()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var resourceName = "MathIO.Source.Topics.json";

            using (Stream stream = currentAssembly.GetManifestResourceStream(resourceName))
            using (var file = new StreamReader(stream))
            using (var reader = new JsonTextReader(file))
            {
                var jArray = (JArray)JToken.ReadFrom(reader);
                foreach (JToken jt in jArray)
                {
                    var jo = jt as JObject;
                    Debug.Assert(jo != null);

                    var newTopic = new TopicNode((string)jo.GetValue("topic"));
                    Topics.Add(newTopic);
                }
            }
        }

        private void LoadConcepts()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var resourceName = "MathIO.Source.Concepts.json";

            using (Stream stream = currentAssembly.GetManifestResourceStream(resourceName))
            using (var file = new StreamReader(stream))
            using (var reader = new JsonTextReader(file))
            {
                var jArray = (JArray)JToken.ReadFrom(reader);
                foreach (JToken jt in jArray)
                {
                    var jo = jt as JObject;
                    Debug.Assert(jo != null);

                    var concept = (string)jo.GetValue("concept");
                    var topic = (string)jo.GetValue("topic");

                    var conceptNode = new ConceptNode(concept, topic);

                    if (!ConceptGraph.ContainsVertex(conceptNode))
                    {
                        ConceptGraph.AddVertex(conceptNode);
                    }

                    var dependencies = jo.GetValue("dependencies") as JArray;
                    if (dependencies == null) continue;
                    foreach (JToken dependToken in dependencies)
                    {
                        var dependentConcept = (string)dependToken;
                        if (dependentConcept == null) continue;

                        ConceptNode cn = ConceptGraph.SearchConceptNode(dependentConcept);
                        if (cn == null)
                        {
                            cn = new ConceptNode(dependentConcept);
                            ConceptGraph.AddVertex(cn);
                        }
                        ConceptGraph.AddEdge(new Edge<ConceptNode>(cn, conceptNode));
                    }
                }
            }
        }

        private void LoadProblems()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var resourceName = "MathIO.Source.Problems.json";

            using (Stream stream = currentAssembly.GetManifestResourceStream(resourceName))
            using (var file = new StreamReader(stream))
            using (var reader = new JsonTextReader(file))
            {
                var jArray = (JArray)JToken.ReadFrom(reader);
                foreach (JToken jt in jArray)
                {
                    var jo = jt as JObject;
                    Debug.Assert(jo != null);

                    var mp = new MathProblem();

                    #region internal

                    var id = (string)jo.GetValue("id");
                    mp.ProblemId = id;
                    var problem = (string)jo.GetValue("problem");
                    mp.Problem = problem;
                    var semantic = jo.GetValue("semantic") as JArray;
                    if (semantic != null)
                    {
                        foreach (JToken jtt in semantic)
                        {
                            var jtto = (string)jtt;
                            mp.Semantic.Add(jtto);
                        }
                    }
                    var concept = jo.GetValue("concept") as JArray;
                    if (concept != null)
                    {
                        foreach (JToken jtt1 in concept)
                        {
                            var jtt1o = (string)jtt1;
                            mp.Concept.Add(jtt1o);
                        }
                    }
                    var topic = jo.GetValue("topic") as JArray;
                    if (topic != null)
                    {
                        foreach (JToken jtt1 in topic)
                        {
                            var jtt1o = (string)jtt1;
                            mp.Topic = jtt1o;
                        }
                    }
                    var solution = jo.GetValue("solution") as JArray;
                    if (solution != null)
                    {
                        foreach (JToken jtt1 in solution)
                        {
                            var jtt1o = (string)jtt1;
                            mp.Solutions.Add(jtt1o);
                        }
                    }
                    #endregion

                    Problems.Add(mp);
                }
            }
        }

        private void LoadScaffolding()
        {
        }

        #endregion

    }
}
