using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Template.MinimumSpanningTree.Graph
{
    public class Node
    {
        public double X { get; set; }
        public double Y { get; set; }
        public List<Edge> Edges { get; } = new List<Edge>();

        public void Add(Node other, SimpleGraph simpleGraph = null)
        {
            var edge = new Edge(this, other);
            Edges.Add(edge);
            other.Edges.Add(edge);
            simpleGraph?.Edges.Add(edge);
        }

        public Node()
        {
        }

        public Node(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Distance(Node other) => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
    }
}