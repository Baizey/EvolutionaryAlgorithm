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
            List<IEndogenousBitIndividual> newIndividuals)
        {
            var oldRate = Algorithm.Population[0].MutationRate;

            var index = 0;
            for (var i = 1; i < newIndividuals.Count; i++)
                if (newIndividuals[i].Fitness > newIndividuals[index].Fitness)
                    index = i;
                else if (newIndividuals[i].Fitness == newIndividuals[index].Fitness &&
                         newIndividuals[i].MutationRate < oldRate)
                    index = i;

            var best = newIndividuals[index];
            newIndividuals[index] = Algorithm.Population[0];
            Algorithm.Population.Individuals[0] = best;

            return new GenerationFilterResult<IEndogenousBitIndividual, BitArray, bool>
            {
                NextGeneration = Algorithm.Population.Individuals,
                Discarded = newIndividuals
            };
        }
    }
}