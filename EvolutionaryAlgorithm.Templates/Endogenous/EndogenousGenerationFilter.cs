using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;

namespace EvolutionaryAlgorithm.Template.Endogenous
{
    public class EndogenousGenerationFilter : IGenerationFilter<IEndogenousBitIndividual, BitArray, bool>
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