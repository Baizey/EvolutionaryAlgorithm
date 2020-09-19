using System.Collections;
using System.Collections.Generic;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.Selection
{
    public class ElitismGenerationFilter : IBitGenerationFilter
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public GenerationFilterResult<IBitIndividual, BitArray, bool> Filter(
            int keepPopulationSize,
            IPopulation<IBitIndividual, BitArray, bool> oldIndividuals,
            List<IBitIndividual> newIndividuals)
        {
            var nextGeneration = new List<IBitIndividual>(oldIndividuals.Count + newIndividuals.Count);
            nextGeneration.AddRange(oldIndividuals);
            nextGeneration.AddRange(newIndividuals);

            nextGeneration.Sort((a, b) => b.CompareTo(a));
            return new GenerationFilterResult<IBitIndividual, BitArray, bool>
            {
                NextGeneration = nextGeneration.GetRange(0, keepPopulationSize),
                Discarded = nextGeneration.GetRange(keepPopulationSize, nextGeneration.Count - keepPopulationSize)
            };
        }
    }
}