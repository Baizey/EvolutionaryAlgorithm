using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Bit.Abstract;
using EvolutionaryAlgorithm.Bit.Algorithm;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;
using EvolutionaryAlgorithm.Template.Basics.Selection;

namespace EvolutionaryAlgorithm.Template.OneLambdaLambda
{
    public class OneLambdaLambdaGenerationGenerator : GenerationGenerator<IBitIndividual, BitArray, bool>
    {
        public OneLambdaLambdaGenerationGenerator()
        {
            Mutator = new BitMutator()
                .CloneGenesFrom(new FirstParentSelector<IBitIndividual, BitArray, bool>())
                .ThenApply(new CrossoverPhase());
            Filter = new ElitismGenerationFilter<IBitIndividual>(true);
        }
    }

    public class CrossoverPhase : IBitMutation<IBitIndividual>
    {
        private MutationPhase _mutationPhase;
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
            _mutationPhase = new MutationPhase();
            _mutationPhase.Initialize();
        }

        public void Update()
        {
            _mutationPhase.Update();
        }

        public async Task Mutate(int index, IBitIndividual child)
        {
            var x = _mutationPhase.Select(index);
        }
    }
}