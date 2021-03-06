﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathIO
{
    public class ConceptNode
    {
        public string Concept { get; set; }

        public double learntProb { get; set; }

        public string Topic { get; set; }

        public string ErrorConcept { get; set; }

        public bool IsError { get; set; }

        public ConceptNode(string concept)
        {
            Concept = concept;
        }

        public ConceptNode(string concept, string topic)
        {
            Concept = concept;
            Topic = topic;
        }

        public ConceptNode(string concept, string topic, bool isError)
        {
            Concept = concept;
            Topic = topic;
            IsError = isError;
        }

        public override string ToString()
        {
            return Concept;
        }
    }
}
