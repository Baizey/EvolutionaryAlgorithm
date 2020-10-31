using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Template.MinimumSpanningTree.Graph;
using EvolutionaryAlgorithm.Template.SatisfiabilityProblem.Structures;

namespace EvolutionaryAlgorithm.Template.SatisfiabilityProblem
{
    public class SatisfiabilityProblemFitness<T> : IBitFitness<T> where T : IBitIndividual
    {
        private readonly Problem _problem;

        public IEvolutionaryAlgorithm<T, BitArray, bool> Algorithm { get; set; }

        public SatisfiabilityProblemFitness(Problem problem) => _problem = problem;

        public void Initialize()
        {
        }

        public double Evaluate(T individual) => _problem.Satisfied(individual.Genes);
    }
}