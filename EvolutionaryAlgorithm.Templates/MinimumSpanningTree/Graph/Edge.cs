namespace EvolutionaryAlgorithm.Template.MinimumSpanningTree.Graph
{
    public class Edge
    {
        private static int _nextId;
        public static void Reset() => _nextId = 0;
        public Node From { get; }
        public Node To { get; }
        public int Id { get; }
        public double Distance { get; }

        public Edge(Node from, Node to)
        {
            Id = _nextId++;
            From = from;
            To = to;
            Distance = From.Distance(To);
        }
    }
}