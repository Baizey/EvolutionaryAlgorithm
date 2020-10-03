using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator;
using EvolutionaryAlgorithm.Core.Infrastructure;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic
{
    public interface IHyperHeuristic<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public List<IGenerationGenerator<TIndividual, TGeneStructure, TGene>> States { get; set; }

        public IGenerationGenerator<TIndividual, TGeneStructure, TGene> this[int i]
        {
            get => States[i];
            set => States[i] = value;
        }

        public Task<List<TIndividual>> Generate();
    }
}