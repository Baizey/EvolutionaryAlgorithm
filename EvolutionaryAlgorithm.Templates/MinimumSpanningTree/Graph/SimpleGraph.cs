using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EvolutionaryAlgorithm.Template.MinimumSpanningTree.Graph
{
    public class SimpleGraph
    {
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }

        public SimpleGraph(int nodes, double edgeChance, int maxX = 10000, int maxY = 10000)
        {
            AddNodes(nodes, maxX, maxY);
            AddEdges(edgeChance);
        }

        public double Distance(BitArray activatedEdges) => Edges.Where(e => activatedEdges[e.Id])
            .Select(e => e.Distance)
            .Sum();

        public bool IsOneComponent(BitArray activatedEdges) => GetRandomComponent(activatedEdges).Count == Nodes.Count;

        private HashSet<Node> GetRandomComponent() => GetRandomComponent(EnableAllEdges());

        private HashSet<Node> GetRandomComponent(BitArray activatedEdges)
        {
            var seen = new HashSet<Node>();
            var queue = new Queue<Node>();
            var node = Nodes[new Random().Next(Nodes.Count)];
            queue.Enqueue(node);
            seen.Add(node);
            while (queue.Count > 0)
            {
                var at = queue.Dequeue();
                foreach (var edge in at.Edges.Where(edge => activatedEdges[edge.Id]))
                {
                    var n = at == edge.To ? edge.From : edge.To;
                    if (seen.Contains(n)) continue;
                    queue.Enqueue(n);
                    seen.Add(n);
                }
            }

            return seen;
        }

        private void AddNodes(int nodes, int maxX = 10000, int maxY = 10000)
        {
            Node.Reset();
            Nodes = new List<Node>();
            var random = new Random();
            for (var i = 0; i < nodes; i++)
                Nodes.Add(new Node(random.Next(maxX) + 1, random.Next(maxY) + 1));
        }

        private void AddEdges(double edgeChance)
        {
            Edge.Reset();
            Edges = new List<Edge>();
            var random = new Random();
            for (var i = 0; i < Nodes.Count; i++)
            {
                var from = Nodes[i];
                for (var j = i + 1; j < Nodes.Count; j++)
                {
                    var to = Nodes[j];
                    if (random.NextDouble() <= edgeChance)
                        from.Add(to, this);
                }
            }

            EnsureEntirelyConnected();
        }

        private void EnsureEntirelyConnected()
        {
            var random = new Random();
            HashSet<Node> component;
            while ((component = GetRandomComponent()).Count < Nodes.Count)
            {
                var a = component.ToList()[random.Next(component.Count)];
                var other = Nodes.Where(n => !component.Contains(n)).ToList();
                var b = other[random.Next(other.Count)];
                a.Add(b, this);
            }
        }

        private BitArray EnableAllEdges()
        {
            var bitArray = new BitArray(Edges.Count);
            bitArray.SetAll(true);
            return bitArray;
        }
    }
}