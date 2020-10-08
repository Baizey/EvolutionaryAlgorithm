using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.HeavyTail
{
    public class HeavyTailGenerationGenerator : BitGenerationGenerator<IEndogenousBitIndividual>
    {
        public HeavyTailGenerationGenerator(double beta)
        {
            Mutator = new BitMutator<IEndogenousBitIndividual>()
                .CloneGenesFrom(new FirstParentSelector<IEndogenousBitIndividual>())
                .ThenApply(new HeavyTailHalfAndHalfMutation(beta));
            Filter = new BitElitismGenerationFilter<IEndogenousBitIndividual>(true);
        }
    }
}