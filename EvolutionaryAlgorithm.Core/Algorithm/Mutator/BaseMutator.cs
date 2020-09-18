using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm.Mutator
{
    public abstract class BaseMutator<TIndividual, TGeneStructure, TGene> : IMutator<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        private IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> _algorithm;
        public List<TIndividual> Reserves { get; set; }
        public int NextGenerationSize { get; set; }

        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm
        {
            get => _algorithm;
            set
            {
                _algorithm = value;
                if (InitialSelector != null)
                    InitialSelector.Algorithm = _algorithm;
                Mutations.ForEach(step => step.Algorithm = _algorithm);
            }
        }

        public IParentSelector<TIndividual, TGeneStructure, TGene> InitialSelector { get; set; }

        public List<IMutation<TIndividual, TGeneStructure, TGene>> Mutations { get; set; } =
            new List<IMutation<TIndividual, TGeneStructure, TGene>>();

        public BaseMutator(int nextGenerationSize,
            IParentSelector<TIndividual, TGeneStructure, TGene> initialSelector = null)
        {
            Reserves = new List<TIndividual>();
            NextGenerationSize = nextGenerationSize;
            InitialSelector = initialSelector;
            if (InitialSelector != null)
                InitialSelector.Algorithm = _algorithm;
        }

        public IMutator<TIndividual, TGeneStructure, TGene> Then(IMutation<TIndividual, TGeneStructure, TGene> mutation)
        {
            Mutations.Add(mutation);
            mutation.Algorithm = _algorithm;
            return this;
        }

        protected void CorrectReserveSize()
        {
            var missing = NextGenerationSize - Reserves.Count;
            var example = Algorithm.Population.Individuals[0];
            for (var i = 0; i < missing; i++)
                Reserves.Add((TIndividual) example.Clone());
        }

        public abstract List<TIndividual> GenerateNextGeneration(
            IPopulation<TIndividual, TGeneStructure, TGene> population);
    }
}