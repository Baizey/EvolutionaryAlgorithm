using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EvolutionaryAlgorithm.Template.FitnessFunctions.Graph
{
    public class SimpleGraph
    {
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }

        public SimpleGraph(
            int nodes,
            double edgeChance,
            int maxX = 1000,
            int maxY = 1000,
            int? seed = null,
            bool randomDistanceCost = false)
        {
            var random = seed == null
                ? new Random()
                : new Random((int) seed);

            AddNodes(nodes, maxX, maxY);
            AddEdges(edgeChance, random, randomDistanceCost);
        }

        public double Distance(bool[] activatedEdges) => Edges.Where(e => activatedEdges[e.Id])
            .Select(e => e.Distance)
            .Sum();

        public bool IsOneComponent(bool[] activatedEdges) => GetRandomComponent(activatedEdges).Count == Nodes.Count;

        private HashSet<Node> GetRandomComponent() => GetRandomComponent(EnableAllEdges());

        private HashSet<Node> GetRandomComponent(bool[] activatedEdges)
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

        private void AddEdges(double edgeChance, Random random, bool randomDistanceCost)
        {
            Edge.Reset();
            Edges = new List<Edge>();
            for (var i = 0; i < Nodes.Count; i++)
            {
                var from = Nodes[i];
                for (var j = i + 1; j < Nodes.Count; j++)
                {
                    if (!(random.NextDouble() <= edgeChance)) continue;
                    var to = Nodes[j];
                    var distance = randomDistanceCost
                        ? random.Next(1000) + 1
                        : (double?) null;

                    @from.Add(to, this, distance);
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

        private bool[] EnableAllEdges()
        {
            var result = new bool[Edges.Count];
            Array.Fill(result, true);
            return result;
        }
    }
}