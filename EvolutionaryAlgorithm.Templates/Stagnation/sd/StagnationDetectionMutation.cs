using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;

namespace EvolutionaryAlgorithm.Template.Stagnation.sd
{
    public class StagnationDetectionMutation : IBitMutation<IEndogenousBitIndividual>
    {
        private readonly MutationApplier _applier = new MutationApplier();
        private IParameters _parameters;
        public IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize() => _parameters = Algorithm.Parameters;

        public void Update()
        {
        }

        public void Mutate(int index, IEndogenousBitIndividual child) =>
            _applier.Mutate(child, _parameters.MutationRate, Algorithm.Parameters.GeneCount);
    }
}