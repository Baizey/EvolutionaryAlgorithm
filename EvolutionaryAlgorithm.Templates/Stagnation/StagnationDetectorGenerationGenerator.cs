using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Bit.Abstract;
using EvolutionaryAlgorithm.Bit.Algorithm;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Algorithm.Crossover;
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
            Filter = new ElitismGenerationFilter<IBitIndividual>(false);
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