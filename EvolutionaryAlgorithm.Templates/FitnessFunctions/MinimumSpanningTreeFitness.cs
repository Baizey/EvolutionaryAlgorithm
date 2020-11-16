using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Template.FitnessFunctions.Graph;

namespace EvolutionaryAlgorithm.Template.FitnessFunctions
{
    public class MinimumSpanningTreeFitness<T> : IBitFitness<T> where T : IBitIndividual
    {
        private readonly SimpleGraph _simpleGraph;

        public IEvolutionaryAlgorithm<T, BitArray, bool> Algorithm { get; set; }

        public MinimumSpanningTreeFitness(SimpleGraph simpleGraph) => _simpleGraph = simpleGraph;

        public void Initialize()
        {
        }

        public long Calls { get; private set; }

        public double Evaluate(T individual)
        {
            Calls++;
            if (_simpleGraph.IsOneComponent(individual.Genes))
                return -_simpleGraph.Distance(individual.Genes);
            return int.MinValue;
        }
    }
}