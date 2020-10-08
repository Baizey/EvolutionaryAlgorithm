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
        public class SelfAdjustingOneLambdaGenerationGenerator : BitGenerationGenerator<IEndogenousBitIndividual>
        {
            public SelfAdjustingOneLambdaGenerationGenerator()
            {
                Mutator = new BitMutator<IEndogenousBitIndividual>()
                    .CloneGenesFrom(new FirstParentSelector<IEndogenousBitIndividual, BitArray, bool>())
                    .ThenApply(new SelfAdjustingLambdaMutation());
                Filter = new SasdOneLambdaGenerationFilter();
            }
        }

        public class SelfAdjustingLambdaMutation : IBitMutation<IEndogenousBitIndividual>
        {
            private readonly MutationApplier _applier = new MutationApplier();
            private IParameters _parameters;
            private int _n;
            public IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> Algorithm { get; set; }

            public void Initialize()
            {
                _parameters = Algorithm.Parameters;
                _n = _parameters.GeneCount;
            }

            public void Update()
            {
            }

            public async Task Mutate(int index, IEndogenousBitIndividual child)
            {
                // Create x_i by flipping each bit in a copy of x independently with probability [r / 2n] if [i ≤ λ/2] and with probability [2r / n] otherwise.
                if (index < _parameters.Lambda / 2)
                {
                    child.MutationRate = Math.Max(1, _parameters.MutationRate / 2);
                    _applier.Mutate(child, _n);
                }
                else
                {
                    child.MutationRate = Math.Min(_n / 2, _parameters.MutationRate * 2);
                    _applier.Mutate(child, _n);
                }
            }
        }

        public class SasdOneLambdaGenerationFilter : IBitGenerationFilter<IEndogenousBitIndividual>
        {
            private readonly Random _random = new Random();
            private IParameters _parameters;
            private int _n;
            private IEndogenousBitIndividual _y;
            public IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> Algorithm { get; set; }

            public void Initialize()
            {
                _parameters = Algorithm.Parameters;
                _n = _parameters.GeneCount;
            }

            public void Update()
            {
                //Perform one of the following two actions with prob. [1 / 2]:
                //    – Replace r with the strength that y has been created with.
                //    – Replace r with either [r / 2] or [2 * r], each with probability [1 / 2].
                if (_random.NextDouble() >= 0.5)
                    _parameters.MutationRate = _random.NextDouble() >= 0.5
                        ? Math.Min(_n / 2, _parameters.MutationRate * 2)
                        : Math.Max(1, _parameters.MutationRate / 2);
                else
                    _parameters.MutationRate = _y.MutationRate;
            }

            public async Task<GenerationFilterResult<IEndogenousBitIndividual, BitArray, bool>> Filter(
                List<IEndogenousBitIndividual> bodies)
            {
                // Find highest f(x_i)
                var index = 0;
                for (var i = 1; i < bodies.Count; i++)
                    if (bodies[i].Fitness > bodies[index].Fitness)
                        index = i;
                _y = bodies[index];

                if (_y.Fitness >= Algorithm.Population[0].Fitness)
                {
                    bodies[index] = Algorithm.Population[0];
                    Algorithm.Population.Individuals[0] = _y;
                }

                return new GenerationFilterResult<IEndogenousBitIndividual, BitArray, bool>
                {
                    NextGeneration = Algorithm.Population.Individuals,
                    Discarded = bodies
                };
            }
        }
    }
}