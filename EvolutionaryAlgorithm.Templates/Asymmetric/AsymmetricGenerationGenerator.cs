using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.GenerationFilter;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.Asymmetric
{
    public class AsymmetricGenerationGenerator : GenerationGenerator<IBitIndividual, BitArray, bool>
    {
        public AsymmetricGenerationGenerator(int learningRate, int observationPhase)
        {
            Mutator = new BitMutator<IBitIndividual>()
                .CloneGenesFrom(new FirstParentSelector<IBitIndividual, BitArray, bool>())
                .ThenApply(new AsymmetricMutation(learningRate, observationPhase));
            Filter = new ElitismGenerationFilter<IBitIndividual, BitArray, bool>(true);
        }
    }
}