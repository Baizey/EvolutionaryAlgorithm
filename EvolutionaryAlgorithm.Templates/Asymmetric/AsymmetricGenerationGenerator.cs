using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.Asymmetric
{
    public class AsymmetricGenerationGenerator : BitGenerationGenerator<IBitIndividual>
    {
        public AsymmetricGenerationGenerator(double learningRate, int observationPhase)
        {
            Mutator = new BitMutator<IBitIndividual>()
                .CloneGenesFrom(new FirstParentSelector<IBitIndividual>())
                .ThenApply(new AsymmetricMutation(learningRate, observationPhase));
            Filter = new BitElitismGenerationFilter<IBitIndividual>(true);
        }
    }
}