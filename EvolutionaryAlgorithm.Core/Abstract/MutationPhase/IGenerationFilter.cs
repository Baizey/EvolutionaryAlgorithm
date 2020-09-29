using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;

namespace EvolutionaryAlgorithm.Core.Abstract.MutationPhase
{
    public interface IGenerationFilter<TIndividual, TGeneStructure, TGene>
        : IEvolutionary<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        Task<GenerationFilterResult<TIndividual, TGeneStructure, TGene>> Filter(List<TIndividual> bodies);
    }


    public abstract class GenerationFilterBase<TIndividual, TGeneStructure, TGene>
        : IGenerationFilter<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }
        public abstract void Initialize();
        public abstract void Update();

        public abstract Task<GenerationFilterResult<TIndividual, TGeneStructure, TGene>> Filter(
            List<TIndividual> bodies);
    }

    public class GenerationFilterResult<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public List<TIndividual> NextGeneration { get; set; }
        public List<TIndividual> Discarded { get; set; }
    }
}