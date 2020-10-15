using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.GenerationFilter;
using EvolutionaryAlgorithm.Core.Parameters;

namespace EvolutionaryAlgorithm.Template.Stagnation
{
    public class SaHalfAndHalfGenerationFilter : IBitGenerationFilter<IEndogenousBitIndividual>
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
                    ? Math.Min(_n / 4, _parameters.MutationRate * 2)
                    : Math.Max(1, _parameters.MutationRate / 2);
            else
                _parameters.MutationRate = _y.MutationRate;
        }

        public GenerationFilterResult<IEndogenousBitIndividual, BitArray, bool> Filter(
            List<IEndogenousBitIndividual> bodies)
        {
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