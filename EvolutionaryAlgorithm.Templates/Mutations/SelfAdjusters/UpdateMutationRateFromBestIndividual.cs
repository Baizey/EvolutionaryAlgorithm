using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;

namespace EvolutionaryAlgorithm.Template.Mutations.SelfAdjusters
{
    public class UpdateMutationRateFromBestIndividual : IBitParameterAdjuster<IBitIndividual>
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }
        private IParameters _parameters;

        public void Initialize() => _parameters = Algorithm.Parameters;

        public void Update() => _parameters.MutationRate = Algorithm.Best.MutationRate;
    }
}