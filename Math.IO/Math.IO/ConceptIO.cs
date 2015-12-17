using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuickGraph;
using QuickGraph.Serialization;

namespace Math.IO
{
    public class ConceptIO
    {
        public int ConceptCount
        {
            get { return _conceptGraph.VertexCount; }
        }

        private BidirectionalGraph<string, Edge<string>> _conceptGraph;

        public ConceptIO(string input) 
        {
            _conceptGraph = new BidirectionalGraph<string, Edge<string>>();
            InitiateGraph(input);
        }

        private void InitiateGraph(string input)
        {
            using (StreamReader file = File.OpenText(input))
            {
                using (var reader = new JsonTextReader(file))
                {
                    var jobject = (JObject)JToken.ReadFrom(reader);
                    foreach (var prop in jobject.Properties())
                    {
                        var concept = prop.Name;

                        if (!_conceptGraph.ContainsVertex(concept))
                        {
                            _conceptGraph.AddVertex(concept);
                        }

                        var dependencies = prop.Value as JArray;
                        Debug.Assert(dependencies != null);
                        foreach (JToken dependToken in dependencies)
                        {
                            var dependentConcept = (string)dependToken;
                            if (dependentConcept == null) continue;

                            if (!_conceptGraph.ContainsVertex(dependentConcept))
                            {
                                _conceptGraph.AddVertex(dependentConcept);
                                _conceptGraph.AddEdge(new Edge<string>(dependentConcept, concept));
                            }
                            else
                            {
                                _conceptGraph.AddEdge(new Edge<string>(dependentConcept, concept));
                            }
                        }
                    }
                }
            }

            //Serialize as the graphML data format.

            string xml;
            using (var writer = new StringWriter())
            {
                var settins = new XmlWriterSettings();
                settins.Indent = true;
                using (var xwriter = XmlWriter.Create(writer, settins))
                    _conceptGraph.SerializeToGraphML<string, Edge<string>, BidirectionalGraph<String, Edge<string>>>(xwriter);

                xml = writer.ToString();
                Console.WriteLine("serialized: " + xml);
            }


        }
    }
}