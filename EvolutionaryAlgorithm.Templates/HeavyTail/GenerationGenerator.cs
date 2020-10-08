using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.HeavyTail
{
    public class GenerationGenerator : BitGenerationGenerator<IEndogenousBitIndividual>
    {
        public GenerationGenerator(double beta)
        {
            Mutator = new BitMutator<IEndogenousBitIndividual>()
                .CloneGenesFrom(new FirstParentSelector<IEndogenousBitIndividual, BitArray, bool>())
                .ThenApply(new HeavyTailHalfAndHalfMutation(beta));
            Filter = new BitElitismGenerationFilter<IEndogenousBitIndividual>(true);
        }
    }
}