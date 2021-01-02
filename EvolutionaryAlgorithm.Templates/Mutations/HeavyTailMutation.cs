using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.Mutations
{
    public class SelfAdaptingHeavyTailMutation : IBitMutation<IBitIndividual>
    {
        private readonly MutationApplier _applier = new MutationApplier();
        private HeavyTailApplier _heavyTailApplier;
        private readonly double _beta;
        private int _n;
        public IEvolutionaryAlgorithm<IBitIndividual, bool[], bool> Algorithm { get; set; }

        public SelfAdaptingHeavyTailMutation(double beta) => _beta = beta;

        public void Initialize()
        {
            _n = Algorithm.Parameters.GeneCount;
            _heavyTailApplier = new HeavyTailApplier(_n);
        }

        public void Mutate(int index, IBitIndividual child) =>
            _applier.Mutate(
                child,
                _heavyTailApplier.GenerateP((int) child.MutationRate, _beta),
                _n);

        public void Update()
        {
        }
    }
}