using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Algorithm.Crossover;
using EvolutionaryAlgorithm.Core.Algorithm.Mutator;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.Endogenous
{
    public class EndogenousGenerationGenerator : GenerationGeneratorBase<IEndogenousBitIndividual, BitArray, bool>
    {
        public EndogenousGenerationGenerator(int learningRate)
        {
            Mutator = new Mutator<IEndogenousBitIndividual, BitArray, bool>()
                .ThenApply(new CloneParent<IEndogenousBitIndividual, BitArray, bool>(
                    new FirstParentSelector<IEndogenousBitIndividual, BitArray, bool>()))
                .ThenApply(new EndogenousMutation(learningRate));
            Filter = new EndogenousGenerationFilter();
        }
    }
}