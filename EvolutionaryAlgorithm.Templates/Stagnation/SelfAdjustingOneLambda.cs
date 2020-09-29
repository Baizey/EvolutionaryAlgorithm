using System;
using System.Collections;
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
    public class SelfAdjustingOneLambda
    {
        public class SelfAdjustingOneLambdaGenerationGenerator
            : GenerationGeneratorBase<IBitIndividual, BitArray, bool>
        {
            public SelfAdjustingOneLambdaGenerationGenerator()
            {
                Mutator = new BitMutator()
                    .ThenApply(new CloneParent<IBitIndividual, BitArray, bool>(
                        new FirstParentSelector<IBitIndividual, BitArray, bool>()))
                    .ThenApply(new SelfAdjustingLambdaMutation());
                Filter = new ElitismGenerationFilter(true);
            }
        }

        public class SelfAdjustingLambdaMutation : IBitMutation
        {
            private readonly Random _random = new Random();
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

                // Create x_i by flipping each bit in a copy of x independently with probability [r / 2n] if [i ≤ λ/2] and with probability [2r / n] otherwise.
                bodies.ForEach(child.CloneGenesTo);
                for (var i = 0; i < bodies.Count / 2; i++)
                    DynamicMutation.Instance.Mutate(bodies[i],
                        2 * Algorithm.Parameters.GeneCount,
                        _parameters.MutationRate);

                for (var i = bodies.Count / 2; i < bodies.Count; i++)
                    DynamicMutation.Instance.Mutate(bodies[i],
                        Algorithm.Parameters.GeneCount,
                        2 * _parameters.MutationRate);

                // Calculate f(x_i)
                bodies.ForEach(b => b.Fitness = Algorithm.Fitness.Evaluate(b));
                // Find highest f(x_i)
                var best = 0;
                for (var i = 1; i < bodies.Count; i++)
                    if (bodies[i].Fitness > bodies[best].Fitness)
                        best = i;

                //Perform one of the following two actions with prob. [1 / 2]:
                //    – Replace r with the strength that y has been created with.
                //    – Replace r with either [r / 2] or [2 * r], each with probability [1 / 2].
                if (_random.NextDouble() >= 0.5)
                    _parameters.MutationRate = _random.NextDouble() >= 0.5
                        ? _parameters.MutationRate * 2
                        : _parameters.MutationRate / 2;
                else if (best < bodies.Count / 2)
                    _parameters.MutationRate /= 2;
                else
                    _parameters.MutationRate *= 2;

                bodies[best].CloneGenesTo(child);
                _storage.Dump(index, bodies);
            }
        }
    }
}