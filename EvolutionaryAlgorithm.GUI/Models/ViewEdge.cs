using EvolutionaryAlgorithm.Template.MinimumSpanningTree.Graph;

namespace EvolutionaryAlgorithm.GUI.Models
{
    public class ViewEdge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Id { get; set; }

        public ViewEdge(Edge edge)
        {
            From = edge.From.Id;
            To = edge.To.Id;
            Id = edge.Id;
        }
    }
}