using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.GenerationFilter;

namespace EvolutionaryAlgorithm.Template.Endogenous
{
    public class EndogenousGenerationFilter : IBitGenerationFilter<IEndogenousBitIndividual>
    {
        public IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        public async Task<GenerationFilterResult<IEndogenousBitIndividual, BitArray, bool>> Filter(
            List<IEndogenousBitIndividual> bodies)
        {
            var oldRate = Algorithm.Population[0].MutationRate;

            var index = 0;
            for (var i = 1; i < bodies.Count; i++)
                if (bodies[i].Fitness > bodies[index].Fitness)
                    index = i;
                else if (bodies[i].Fitness == bodies[index].Fitness &&
                         bodies[i].MutationRate < oldRate)
                    index = i;

            var curr = Algorithm.Population[0];
            var best = bodies[index];
            bodies[index] = Algorithm.Population[0];
            Algorithm.Population.Individuals[0] = best;

            return new GenerationFilterResult<IEndogenousBitIndividual, BitArray, bool>
            {
                NextGeneration = Algorithm.Population.Individuals,
                Discarded = bodies
            };
        }
    }
}