using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Bit.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;

namespace EvolutionaryAlgorithm.Template.Basics.Selection
{
    public class ElitismGenerationFilter<T> : IBitGenerationFilter<T> where T : IBitIndividual
    {
        private readonly bool _preferNew;

        public ElitismGenerationFilter(bool preferNew) => _preferNew = preferNew;

        public IEvolutionaryAlgorithm<T, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        public async Task<GenerationFilterResult<T, BitArray, bool>> Filter(
            List<T> bodies)
        {
            var keep = Algorithm.Parameters.Mu;
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

            var nextGeneration = new List<T>(keep);
            if (i > 0) nextGeneration.AddRange(oldIndividuals.GetRange(0, i));
            if (j > 0) nextGeneration.AddRange(bodies.GetRange(0, j));

            if (keep >= nextGeneration.Count)
                return new GenerationFilterResult<T, BitArray, bool>
                {
                    NextGeneration = nextGeneration,
                    Discarded = new List<T>()
                };

            var discarded = new List<T>(oldIndividuals.Count + bodies.Count - keep);
            if (i < oldIndividuals.Count) discarded.AddRange(oldIndividuals.GetRange(i, oldIndividuals.Count - i));
            if (j < bodies.Count) discarded.AddRange(bodies.GetRange(j, bodies.Count - j));

            return new GenerationFilterResult<T, BitArray, bool>
            {
                NextGeneration = nextGeneration,
                Discarded = discarded
            };
        }
    }
}