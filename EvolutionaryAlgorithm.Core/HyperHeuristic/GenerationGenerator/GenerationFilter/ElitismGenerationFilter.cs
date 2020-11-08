using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.GenerationFilter
{
    public class ElitismGenerationFilter<TIndividual, TGeneStructure, TGene>
        : IGenerationFilter<TIndividual, TGeneStructure, TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        private readonly bool _preferNew;

        public ElitismGenerationFilter(bool preferNew) => _preferNew = preferNew;

        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        private GenerationFilterResult<TIndividual, TGeneStructure, TGene> FilterSingle(List<TIndividual> bodies)
        {
            if (Algorithm.Population.Count > 1)
                Algorithm.Population.Individuals = new List<TIndividual> {Algorithm.Population.Best};
            var oldRate = Algorithm.Best.MutationRate;
            var index = 0;
            for (var i = 1; i < bodies.Count; i++)
                if (bodies[i].Fitness > bodies[index].Fitness ||
                    bodies[i].Fitness == bodies[index].Fitness && bodies[i].MutationRate < oldRate)
                    index = i;
            var best = bodies[index];

            // If no-one beats current best
            if (best.Fitness < Algorithm.Population[0].Fitness)
                return new GenerationFilterResult<TIndividual, TGeneStructure, TGene>
                {
                    NextGeneration = Algorithm.Population.Individuals,
                    Discarded = bodies
                };

            // If tie, but we dont prefer new
            if (!_preferNew && best.Fitness == Algorithm.Population[0].Fitness)
                return new GenerationFilterResult<TIndividual, TGeneStructure, TGene>
                {
                    NextGeneration = Algorithm.Population.Individuals,
                    Discarded = bodies
                };

            // Replace old best with new best
            bodies[index] = Algorithm.Population[0];
            Algorithm.Population.Individuals[0] = best;
            return new GenerationFilterResult<TIndividual, TGeneStructure, TGene>
            {
                NextGeneration = Algorithm.Population.Individuals,
                Discarded = bodies
            };
        }

        public GenerationFilterResult<TIndividual, TGeneStructure, TGene> Filter(
            List<TIndividual> bodies)
        {
            var keep = Algorithm.Parameters.Mu;
            // Handle optimized if [Mu == 1]
            if (keep == 1) return FilterSingle(bodies);

            var oldIndividuals = Algorithm.Population.Individuals;

            oldIndividuals.Sort((a, b) => b.CompareTo(a));
            bodies.Sort((a, b) => b.CompareTo(a));

            int i = 0, j = 0;
            for (; i < oldIndividuals.Count && j < bodies.Count && i + j < keep;)
            {
                if (oldIndividuals[i].Fitness > bodies[j].Fitness)
                    i++;
                else if (oldIndividuals[i].Fitness < bodies[j].Fitness)
                    j++;
                else if (_preferNew)
                    j++;
                else
                    i++;
            }

            var nextGeneration = new List<TIndividual>(keep);
            if (i > 0) nextGeneration.AddRange(oldIndividuals.GetRange(0, i));
            if (j > 0) nextGeneration.AddRange(bodies.GetRange(0, j));

            if (keep >= nextGeneration.Count)
                return new GenerationFilterResult<TIndividual, TGeneStructure, TGene>
                {
                    NextGeneration = nextGeneration,
                    Discarded = new List<TIndividual>()
                };

            var discarded = new List<TIndividual>(oldIndividuals.Count + bodies.Count - keep);
            if (i < oldIndividuals.Count) discarded.AddRange(oldIndividuals.GetRange(i, oldIndividuals.Count - i));
            if (j < bodies.Count) discarded.AddRange(bodies.GetRange(j, bodies.Count - j));

            return new GenerationFilterResult<TIndividual, TGeneStructure, TGene>
            {
                NextGeneration = nextGeneration,
                Discarded = discarded
            };
        }
    }
}