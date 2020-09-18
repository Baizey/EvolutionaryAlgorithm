using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm.Mutator
{
    public class DynamicMutator<TIndividual, TGeneStructure, TGene> : BaseMutator<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public DynamicMutator(int nextGenerationSize,
            IParentSelector<TIndividual, TGeneStructure, TGene> initialSelector = null)
            : base(nextGenerationSize, initialSelector)
        {
        }

        public override List<TIndividual> GenerateNextGeneration(
            IPopulation<TIndividual, TGeneStructure, TGene> population)
        {
            if (NextGenerationSize > Reserves.Count)
                CorrectReserveSize();

            for (var i = 0; i < NextGenerationSize; i++)
            {
                var reserve = Reserves[i];
                InitialSelector?.Select(population).CloneGenesTo(reserve);
                Mutations.ForEach(step => step.Mutate(reserve));
            }

            return NextGenerationSize < Reserves.Count
                ? Reserves.GetRange(0, NextGenerationSize)
                : Reserves;
        }
    }
}