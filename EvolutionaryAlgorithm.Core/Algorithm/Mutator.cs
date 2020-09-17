using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class Mutator<TIndividual, TGeneStructure, TGene> : IMutator<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        private IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> _algorithm;

        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm
        {
            get => _algorithm;
            set
            {
                _algorithm = value;
                MutationSteps.ForEach(step => step.Algorithm = _algorithm);
            }
        }

        public List<TIndividual> Reserves { get; set; }
        public IParentSelector<TIndividual, TGeneStructure, TGene> InitialSelector { get; set; }

        public List<IMutation<TIndividual, TGeneStructure, TGene>> MutationSteps { get; set; } =
            new List<IMutation<TIndividual, TGeneStructure, TGene>>();

        public Mutator(int size, IParentSelector<TIndividual, TGeneStructure, TGene> initialSelector)
        {
            Reserves = Enumerable.Range(0, size)
                .Select(_ => Activator.CreateInstance<TIndividual>())
                .ToList();
            InitialSelector = initialSelector;
        }

        public IMutator<TIndividual, TGeneStructure, TGene> Then(
            IMutation<TIndividual, TGeneStructure, TGene> mutation)
        {
            MutationSteps.Add(mutation);
            mutation.Algorithm = _algorithm;
            return this;
        }

        public List<TIndividual> GenerateNextGeneration(
            IPopulation<TIndividual, TGeneStructure, TGene> population)
        {
            Reserves.ForEach(reserve =>
            {
                var origin = InitialSelector.Select(population);
                origin.CloneGenesTo(reserve);
                foreach (var step in MutationSteps) step.Mutate(reserve);
            });

            return Reserves;
        }
    }
}