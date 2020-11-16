using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.GenerationFilter
{
    public class ElitismGenerationFilterAlwaysNew<TIndividual, TGeneStructure, TGene>
        : IGenerationFilter<TIndividual, TGeneStructure, TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        private GenerationFilterResult<TIndividual, TGeneStructure, TGene> FilterSingle(List<TIndividual> bodies)
        {
            if (Algorithm.Population.Count > 1)
                Algorithm.Population.Individuals = new List<TIndividual> {Algorithm.Population.Best};
            var index = 0;
            var oldRate = bodies[index].MutationRate;
            for (var i = 1; i < bodies.Count; i++)
                if (bodies[i].Fitness > bodies[index].Fitness ||
                    bodies[i].Fitness == bodies[index].Fitness && bodies[i].MutationRate < oldRate)
                    oldRate = bodies[index = i].MutationRate;

            var best = bodies[index];
            bodies[index] = Algorithm.Population[0];
            Algorithm.Population.Individuals[0] = best;
            return new GenerationFilterResult<TIndividual, TGeneStructure, TGene>
            {
                NextGeneration = Algorithm.Population.Individuals,
                Discarded = bodies
            };
        }

        public GenerationFilterResult<TIndividual, TGeneStructure, TGene> Filter(List<TIndividual> bodies)
        {
            var keep = Algorithm.Parameters.Mu;
            if (keep == 1) return FilterSingle(bodies);

            var oldIndividuals = Algorithm.Population.Individuals;

            if (bodies.Count <= keep)
                return new GenerationFilterResult<TIndividual, TGeneStructure, TGene>
                {
                    NextGeneration = bodies,
                    Discarded = oldIndividuals
                };

            bodies.Sort((a, b) => b.CompareTo(a));
            var nextGeneration = bodies.GetRange(0, keep);

            oldIndividuals.AddRange(bodies.GetRange(keep, bodies.Count - keep));

            return new GenerationFilterResult<TIndividual, TGeneStructure, TGene>
            {
                NextGeneration = nextGeneration,
                Discarded = oldIndividuals
            };
        }

        public void Initialize()
        {
        }

        public void Update()
        {
        }
    }
}