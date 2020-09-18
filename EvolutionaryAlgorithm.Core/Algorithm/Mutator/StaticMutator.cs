using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm.Mutator
{
    public class StaticMutator<TIndividual, TGeneStructure, TGene> : BaseMutator<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public StaticMutator(int nextGenerationSize,
            IParentSelector<TIndividual, TGeneStructure, TGene> initialSelector = null)
            : base(nextGenerationSize, initialSelector)
        {
        }

        public override List<TIndividual> GenerateNextGeneration(
            IPopulation<TIndividual, TGeneStructure, TGene> population)
        {
            if (NextGenerationSize > Reserves.Count)
                CorrectReserveSize();

            Reserves.ForEach(reserve =>
            {
                InitialSelector?.Select(population).CloneGenesTo(reserve);
                Mutations.ForEach(step => step.Mutate(reserve));
            });

            return Reserves;
        }
    }
}