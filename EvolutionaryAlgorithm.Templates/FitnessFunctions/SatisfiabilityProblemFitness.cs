using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Template.FitnessFunctions.SatisfiabilityProblem;

namespace EvolutionaryAlgorithm.Template.FitnessFunctions
{
    public class SatisfiabilityProblemFitness<T> : IBitFitness<T> where T : IBitIndividual
    {
        private readonly SatisfiabilityProblem.SatisfiabilityProblem _satisfiabilityProblem;

        public IEvolutionaryAlgorithm<T, BitArray, bool> Algorithm { get; set; }

        public SatisfiabilityProblemFitness(SatisfiabilityProblem.SatisfiabilityProblem satisfiabilityProblem) =>
            _satisfiabilityProblem = satisfiabilityProblem;

        public long Calls { get; private set; }

        public void Initialize()
        {
        }

        public double Evaluate(T individual)
        {
            Calls++;
            return _satisfiabilityProblem.Satisfied(individual.Genes);
        }
    }
}