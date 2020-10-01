using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;
using EvolutionaryAlgorithm.Template.Basics.Selection;

namespace EvolutionaryAlgorithm.Template.Asymmetric
{
    public class AsymmetricGenerationGenerator : GenerationGenerator<IBitIndividual, BitArray, bool>
    {
        public AsymmetricGenerationGenerator(int learningRate, int observationPhase)
        {
            Mutator = new BitMutator()
                .CloneGenesFrom(new FirstParentSelector<IBitIndividual, BitArray, bool>())
                .ThenApply(new AsymmetricMutation(learningRate, observationPhase));
            Filter = new ElitismGenerationFilter(true);
        }
    }
}