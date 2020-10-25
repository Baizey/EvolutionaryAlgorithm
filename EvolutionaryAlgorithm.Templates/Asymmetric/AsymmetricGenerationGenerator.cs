using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.Asymmetric
{
    public class AsymmetricGenerationGenerator : BitGenerationGenerator<IEndogenousBitIndividual>
    {
        public AsymmetricGenerationGenerator(double learningRate, int observationPhase)
        {
            Mutator = new BitMutator<IEndogenousBitIndividual>()
                .CloneGenesFrom(new FirstParentSelector<IEndogenousBitIndividual>())
                .ThenApply(new AsymmetricMutation(learningRate, observationPhase));
            Filter = new BitElitismGenerationFilter<IEndogenousBitIndividual>(true);
        }
    }
}