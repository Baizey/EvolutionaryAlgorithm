using EvolutionaryAlgorithm.Template.FitnessFunctions.Graph;

namespace EvolutionaryAlgorithm.GUI.Models
{
    public class ViewNode
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public ViewNode(Node edge)
        {
            X = edge.X;
            Y = edge.Y;
            Id = edge.Id;
        }
    }
}