using System.Collections;
using EvolutionaryAlgorithm.Bit.Abstract;
using EvolutionaryAlgorithm.Bit.Algorithm;
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
            Filter = new ElitismGenerationFilter<IBitIndividual>(true);
        }
    }
}