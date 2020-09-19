using System.Collections;
using System.Collections.Generic;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.Selection
{
    public class ElitismGenerationFilter : IBitGenerationFilter
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        private static readonly List<IBitIndividual> Empty = new List<IBitIndividual>();

        public GenerationFilterResult<IBitIndividual, BitArray, bool> Filter(
            IPopulation<IBitIndividual, BitArray, bool> oldIndividuals,
            List<IBitIndividual> newIndividuals)
        {
            var keep = Algorithm.Parameters.Mu;

            var nextGeneration = new List<IBitIndividual>(oldIndividuals.Count + newIndividuals.Count);
            nextGeneration.AddRange(oldIndividuals);
            nextGeneration.AddRange(newIndividuals);

            if (keep >= nextGeneration.Count)
                return new GenerationFilterResult<IBitIndividual, BitArray, bool>
                {
                    NextGeneration = nextGeneration,
                    Discarded = Empty
                };

            nextGeneration.Sort((a, b) => b.CompareTo(a));
            return new GenerationFilterResult<IBitIndividual, BitArray, bool>
            {
                NextGeneration = nextGeneration.GetRange(0, keep),
                Discarded = nextGeneration.GetRange(keep, nextGeneration.Count - keep)
            };
        }
    }
}