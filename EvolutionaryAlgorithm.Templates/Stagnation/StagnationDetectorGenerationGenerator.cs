using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Crossover;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.Stagnation
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

    public class StagnationDetectionMutation : IBitMutation<IEndogenousBitIndividual>
    {
        private readonly MutationApplier _applier = new MutationApplier();
        private IParameters _parameters;
        public IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize() => _parameters = Algorithm.Parameters;

        public void Update()
        {
        }

        public async Task Mutate(int index, IEndogenousBitIndividual child) =>
            _applier.Mutate(child, _parameters.MutationRate, Algorithm.Parameters.GeneCount);
    }
}