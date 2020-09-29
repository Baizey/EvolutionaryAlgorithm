using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Algorithm.Crossover;
using EvolutionaryAlgorithm.Template.Basics.Mutation;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;
using EvolutionaryAlgorithm.Template.Basics.Selection;

namespace EvolutionaryAlgorithm.Template.Stagnation
{
    public class StagnationDetectorGenerationGenerator : GenerationGeneratorBase<IBitIndividual, BitArray, bool>
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
        private IIndividualStorage<IBitIndividual, BitArray, bool> _storage;
        private IParameters<IBitIndividual, BitArray, bool> _parameters;
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
            _parameters = Algorithm.Parameters;
            _storage = new IndividualStorage<IBitIndividual, BitArray, bool>(Algorithm);
        }

        public void Update()
        {
        }

        public async Task Mutate(int index, IBitIndividual child)
        {
            var bodies = _storage.Get(index, Algorithm.Parameters.Lambda);
            bodies.ForEach(child.CloneGenesTo);

            for (var i = 0; i < bodies.Count; i++)
                DynamicMutation.Instance.Mutate(bodies[i],
                    Algorithm.Parameters.GeneCount,
                    2 * _parameters.MutationRate);

            bodies.ForEach(b => b.Fitness = Algorithm.Fitness.Evaluate(b));
            bodies
                .Aggregate((a, b) => a.Fitness > b.Fitness ? a : b)
                .CloneGenesTo(child);
            _storage.Dump(index, bodies);
        }
    }
}