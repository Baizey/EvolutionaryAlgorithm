using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic
{
    public abstract class HyperHeuristicBase<TIndividual, TGeneStructure, TGene>
        : IHyperHeuristic<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public List<IGenerationGenerator<TIndividual, TGeneStructure, TGene>> States { get; set; } =
            new List<IGenerationGenerator<TIndividual, TGeneStructure, TGene>>();

        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public virtual void Initialize() => States.ForEach(s =>
        {
            s.Algorithm = Algorithm;
            s.Initialize();
        });

        public virtual void Update() => States.ForEach(s => s.Update());

        public abstract Task<List<TIndividual>> Generate();
    }
}