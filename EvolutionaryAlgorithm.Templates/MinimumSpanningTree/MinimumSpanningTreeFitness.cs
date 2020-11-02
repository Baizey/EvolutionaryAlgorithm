using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Template.MinimumSpanningTree.Graph;

namespace EvolutionaryAlgorithm.Template.MinimumSpanningTree
{
    public class MinimumSpanningTreeFitness<T> : IBitFitness<T> where T : IBitIndividual
    {
        private readonly SimpleGraph _simpleGraph;

        public IEvolutionaryAlgorithm<T, BitArray, bool> Algorithm { get; set; }

        public MinimumSpanningTreeFitness(SimpleGraph simpleGraph) => _simpleGraph = simpleGraph;

        public void Initialize()
        {
        }

        public double Evaluate(T individual)
        {
            if (_simpleGraph.IsOneComponent(individual.Genes))
                return -_simpleGraph.Distance(individual.Genes);
            return int.MinValue;
        }
    }
}