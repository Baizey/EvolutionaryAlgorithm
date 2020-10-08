using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Crossover;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.Stagnation.sd
{
    public class StagnationDetectorGenerationGenerator : BitGenerationGenerator<IEndogenousBitIndividual>
    {
        public StagnationDetectorGenerationGenerator()
        {
            Mutator = new BitMutator<IEndogenousBitIndividual>()
                .ThenApply(new CloneParent<IEndogenousBitIndividual, BitArray, bool>(
                    new FirstParentSelector<IEndogenousBitIndividual, BitArray, bool>()))
                .ThenApply(new StagnationDetectionMutation());
            Filter = new BitElitismGenerationFilter<IEndogenousBitIndividual>(false);
        }
    }
}