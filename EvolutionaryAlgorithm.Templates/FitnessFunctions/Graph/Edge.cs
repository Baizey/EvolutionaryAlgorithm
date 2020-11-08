namespace EvolutionaryAlgorithm.Template.FitnessFunctions.Graph
{
    public class Edge
    {
        private static int _nextId;
        public int Id { get; } = _nextId++;
        public static void Reset() => _nextId = 0;
        public Node From { get; }
        public Node To { get; }
        public double Distance { get; }

        public Edge(Node from, Node to)
        {
            From = from;
            To = to;
            Distance = From.Distance(To);
        }
    }
}