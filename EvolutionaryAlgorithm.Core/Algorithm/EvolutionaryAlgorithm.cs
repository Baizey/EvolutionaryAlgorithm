using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class
        EvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        : IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public static EvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Construct =>
            new EvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>();

        private List<TIndividual> _reserves = new List<TIndividual>();
        private IFitness<TIndividual, TGeneStructure, TGene> _fitness;
        private IPopulation<TIndividual, TGeneStructure, TGene> _population;
        private IMutator<TIndividual, TGeneStructure, TGene> _mutator;
        private IGenerationFilter<TIndividual, TGeneStructure, TGene> _generationFilter;
        private IParameters<TIndividual, TGeneStructure, TGene> _parameters;
        public TIndividual Best => Population.Best;
        public IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> Statistics { get; set; }

        public IParameters<TIndividual, TGeneStructure, TGene> Parameters
        {
            get => _parameters;
            set
            {
                _parameters = value;
                _parameters.Algorithm = this;
            }
        }

        public IPopulation<TIndividual, TGeneStructure, TGene> Population
        {
            get => _population;
            set
            {
                _population = value;
                if (_fitness != null)
                    _population.Individuals.ForEach(i => i.Fitness = Fitness.Evaluate(i));
            }
        }

        public IFitness<TIndividual, TGeneStructure, TGene> Fitness
        {
            get => _fitness;
            set
            {
                _fitness = value;
                _fitness.Algorithm = this;
                _population?.Individuals.ForEach(i => i.Fitness = Fitness.Evaluate(i));
            }
        }

        public IMutator<TIndividual, TGeneStructure, TGene> Mutator
        {
            get => _mutator;
            set
            {
                _mutator = value;
                _mutator.Algorithm = this;
            }
        }

        public IGenerationFilter<TIndividual, TGeneStructure, TGene> GenerationFilter
        {
            get => _generationFilter;
            set
            {
                _generationFilter = value;
                _generationFilter.Algorithm = this;
            }
        }

        private void AddUnusedBodiesFrom(IEnumerable<TIndividual> individuals) => _reserves.AddRange(individuals);

        private List<TIndividual> GetFreshBodies(int generationSize)
        {
            if (_reserves.Count < generationSize)
            {
                var example = Population[0];
                var missing = generationSize - _reserves.Count;
                for (var i = 0; i < missing; i++)
                    _reserves.Add((TIndividual) example.Clone());
            }

            if (_reserves.Count == generationSize)
            {
                var used = _reserves;
                _reserves = new List<TIndividual>();
                return used;
            }
            else
            {
                var used = _reserves.GetRange(0, generationSize);
                _reserves = _reserves.GetRange(generationSize, _reserves.Count - generationSize);
                return used;
            }
        }

        public Task EvolveOneGeneration()
        {
            var newIndividuals = GetFreshBodies(Parameters.Lambda);

            Mutator.Mutate(Population, newIndividuals);
            newIndividuals.ForEach(i => i.Fitness = Fitness.Evaluate(i));

            var result = GenerationFilter.Filter(Parameters.Mu, Population, newIndividuals);
            Population.Individuals = result.NextGeneration;
            AddUnusedBodiesFrom(result.Discarded);

            Statistics.Update(this);
            return Task.CompletedTask;
        }
    }
}