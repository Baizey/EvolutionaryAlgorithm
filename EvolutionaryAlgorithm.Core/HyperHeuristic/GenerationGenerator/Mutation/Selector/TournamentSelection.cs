using System;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector
{
    public class TournamentSelection<TIndividual, TGeneStructure, TGene>
        : ISingleParentSelector<TIndividual, TGeneStructure, TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        private readonly Random _random = new Random();
        private int _size;
        private int _lambda = int.MinValue;
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public TIndividual Select(int index)
        {
            var population = Algorithm.Population;
            var popCount = population.Count;
            TIndividual best = default;
            for (var i = 0; i < _size; i++)
            {
                var selected = population[_random.Next(popCount)];
                if (best == null || selected.Fitness > best.Fitness)
                    best = selected;
            }

            return best;
        }

        public void Initialize() => Update();

        public void Update()
        {
            if (_lambda == Algorithm.Parameters.Lambda) return;
            _lambda = Algorithm.Parameters.Lambda;
            _size = Math.Max(2, (int) Math.Log2(_lambda));
        }
    }
}