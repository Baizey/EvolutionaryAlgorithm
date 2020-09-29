using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Algorithm.Crossover;
using EvolutionaryAlgorithm.Template.Basics.Mutation;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;
using EvolutionaryAlgorithm.Template.Basics.Selection;

namespace EvolutionaryAlgorithm.Template.Stagnation
{
    public class StagnationDetectorGenerationGenerator : GenerationGenerator<IBitIndividual, BitArray, bool>
    {
        public StagnationDetectorGenerationGenerator()
        {
            Mutator = new BitMutator()
                .ThenApply(new CloneParent<IBitIndividual, BitArray, bool>(
                    new FirstParentSelector<IBitIndividual, BitArray, bool>()))
                .ThenApply(new StagnationDetectionMutation());
            Filter = new ElitismGenerationFilter(false);
        }
    }

    public class StagnationDetectionMutation : IBitMutation
    {
        private IParameters<IBitIndividual, BitArray, bool> _parameters;
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize() => _parameters = Algorithm.Parameters;

        public void Update()
        {
        }

        public async Task Mutate(int index, IBitIndividual child) =>
            DynamicMutation.Instance.Mutate(child, Algorithm.Parameters.GeneCount, 2 * _parameters.MutationRate);
    }
}