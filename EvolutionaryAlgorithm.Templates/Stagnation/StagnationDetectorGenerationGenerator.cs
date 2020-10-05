using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Crossover;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.Stagnation
{
    public class StagnationDetectorGenerationGenerator : BitGenerationGenerator<IBitIndividual>
    {
        public StagnationDetectorGenerationGenerator()
        {
            Mutator = new BitMutator<IBitIndividual>()
                .ThenApply(new CloneParent<IBitIndividual, BitArray, bool>(
                    new FirstParentSelector<IBitIndividual, BitArray, bool>()))
                .ThenApply(new StagnationDetectionMutation());
            Filter = new BitElitismGenerationFilter<IBitIndividual>(false);
        }
    }

    public class StagnationDetectionMutation : IBitMutation<IBitIndividual>
    {
        private readonly MutationApplier _applier = new MutationApplier();
        private IParameters _parameters;
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize() => _parameters = Algorithm.Parameters;

        public void Update()
        {
        }

        public async Task Mutate(int index, IBitIndividual child) =>
            _applier.Mutate(child, _parameters.MutationRate, Algorithm.Parameters.GeneCount);
    }
}