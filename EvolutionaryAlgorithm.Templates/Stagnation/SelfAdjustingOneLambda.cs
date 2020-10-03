using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.GenerationFilter;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Template.Basics.ParentSelector;

namespace EvolutionaryAlgorithm.Template.Stagnation
{
    public class SelfAdjustingOneLambda
    {
        public class SelfAdjustingOneLambdaGenerationGenerator : BitGenerationGenerator<IBitIndividual>
        {
            public SelfAdjustingOneLambdaGenerationGenerator()
            {
                Mutator = new BitMutator<IBitIndividual>()
                    .CloneGenesFrom(new FirstParentSelector<IBitIndividual, BitArray, bool>())
                    .ThenApply(new SelfAdjustingLambdaMutation());
                Filter = new SasdOneLambdaGenerationFilter();
            }
        }

        public class SelfAdjustingLambdaMutation : IBitMutation<IBitIndividual>
        {
            private readonly MutationApplier _applier = new MutationApplier();
            private IParameters _parameters;
            public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

            public void Initialize() => _parameters = Algorithm.Parameters;

            public void Update()
            {
            }

            public Task Mutate(int index, IBitIndividual child)
            {
                // Create x_i by flipping each bit in a copy of x independently with probability [r / 2n] if [i ≤ λ/2] and with probability [2r / n] otherwise.
                _applier.Mutate(child,
                    index < _parameters.Lambda / 2
                        ? Math.Max(1, _parameters.MutationRate / 2)
                        : Math.Min(Algorithm.Parameters.GeneCount / 2, _parameters.MutationRate * 2),
                    Algorithm.Parameters.GeneCount);

                return Task.CompletedTask;
            }
        }

        public class SasdOneLambdaGenerationFilter : IBitGenerationFilter<IBitIndividual>
        {
            private readonly Random _random = new Random();
            private IParameters _parameters;
            public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

            public void Initialize()
            {
                _parameters = Algorithm.Parameters;
            }

            public void Update()
            {
            }

            public async Task<GenerationFilterResult<IBitIndividual, BitArray, bool>> Filter(
                List<IBitIndividual> bodies)
            {
                // Find highest f(x_i)
                var index = 0;
                for (var i = 1; i < bodies.Count; i++)
                    if (bodies[i].Fitness > bodies[index].Fitness)
                        index = i;

                //Perform one of the following two actions with prob. [1 / 2]:
                //    – Replace r with the strength that y has been created with.
                //    – Replace r with either [r / 2] or [2 * r], each with probability [1 / 2].
                if (_random.NextDouble() >= 0.5)
                    _parameters.MutationRate = _random.NextDouble() >= 0.5
                        ? _parameters.MutationRate * 2
                        : _parameters.MutationRate / 2;
                else if (index < bodies.Count / 2)
                    _parameters.MutationRate /= 2;
                else
                    _parameters.MutationRate *= 2;

                var best = bodies[index];
                if (best.Fitness >= Algorithm.Population[0].Fitness)
                {
                    bodies[index] = Algorithm.Population[0];
                    Algorithm.Population.Individuals[0] = best;
                }

                return new GenerationFilterResult<IBitIndividual, BitArray, bool>
                {
                    NextGeneration = Algorithm.Population.Individuals,
                    Discarded = bodies
                };
            }
        }
    }
}