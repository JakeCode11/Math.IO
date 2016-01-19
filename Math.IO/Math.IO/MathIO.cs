using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        public static string path;

        private MathInputs()
        {
            Topics = new List<TopicNode>();
            ConceptGraph = new BidirectionalGraph<ConceptNode, Edge<ConceptNode>>();
            Problems = new List<MathProblem>();
            Load(path);
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

        #region Path Config

        private static string _conceptFilePath      = "Concepts.json";
        private static string _topicFilePath = "Topics.json";
        private static string _errorConceptFilePath = "ErrorConcepts.json";
        private static string _problemFilePath = "Problems.json";
        private static string _scaffoldingFilePath = "Scaffoldings.json";

        private static string _dataFolderName = "MathProblem-Dataset";

        public static string RetrievePath(string path, string filePath)
        {
            string startupPath;
            try
            {
                startupPath =
                    Path.Combine(path,
                        _dataFolderName,
                        filePath);
                return startupPath;
            }
            catch (Exception)
            {
               
            }
            return null;
        }

        #endregion

        #region Load

        public void Load(string inputUrl)
        {
            LoadTopics();
            LoadConcepts();
            //LoadErrorConcepts();
            LoadProblems();
            LoadScaffolding();
        }

        private void LoadTopics()
        {
            string inputUrl = RetrievePath(path,_topicFilePath);

            using (StreamReader file = File.OpenText(inputUrl))
            {
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
        }

        private void LoadConcepts()
        {
            string inputUrl = RetrievePath(path,_conceptFilePath);
            using (StreamReader file = File.OpenText(inputUrl))
            {
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
        }

        [Obsolete]
        private void LoadErrorConcepts()
        {
            string inputUrl = RetrievePath(path,_errorConceptFilePath);
            using (StreamReader file = File.OpenText(inputUrl))
            {
                using (var reader = new JsonTextReader(file))
                {
                    var jArray = (JArray) JToken.ReadFrom(reader);
                    foreach (JToken jt in jArray)
                    {
                        var jo = jt as JObject;
                        Debug.Assert(jo != null);

                        var error = (string) jo.GetValue("error-concept");
                        var concept = (string) jo.GetValue("concept");
                        var conceptNode = ConceptGraph.SearchConceptNode(concept);
                        if (conceptNode != null)
                        {
                            conceptNode.ErrorConcept = error;
                        }
                    }
                }
            }
        }

        private void LoadProblems()
        {
            string inputUrl = RetrievePath(path,_problemFilePath);
            using (StreamReader file = File.OpenText(inputUrl))
            {
                using (var reader = new JsonTextReader(file))
                {
                    var jArray = (JArray) JToken.ReadFrom(reader);
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
                                var jtto = (string) jtt;
                                mp.Semantic.Add(jtto);
                            }
                        }
                        var concept = jo.GetValue("concept") as JArray;
                        if (concept != null)
                        {
                            foreach (JToken jtt1 in concept)
                            {
                                var jtt1o = (string) jtt1;
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
        }

        private void LoadScaffolding()
        {
        }

        #endregion

        #region Transformation

        public void ToGraphML()
        {
/*            //Serialize as the graphML data format.

            string xml;
            using (var writer = new StringWriter())
            {
                var settins = new XmlWriterSettings();
                settins.Indent = true;
                using (var xwriter = XmlWriter.Create(writer, settins))
                    ConceptGraph.SerializeToGraphML<string, Edge<string>, BidirectionalGraph<String, Edge<string>>>(xwriter);

                xml = writer.ToString();
                Console.WriteLine("serialized: " + xml);
            }*/            
        }

        #endregion

    }
}
