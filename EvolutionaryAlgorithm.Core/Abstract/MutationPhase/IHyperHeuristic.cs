using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;

namespace EvolutionaryAlgorithm.Core.Abstract.MutationPhase
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

        public Task<List<TIndividual>> Generate(int amount);
    }

    public abstract class HyperHeuristicBase<TIndividual, TGeneStructure, TGene>
        : IHyperHeuristic<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public List<IGenerationGenerator<TIndividual, TGeneStructure, TGene>> States { get; set; } =
            new List<IGenerationGenerator<TIndividual, TGeneStructure, TGene>>();

        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public void Initialize() => States.ForEach(s =>
        {
            s.Algorithm = Algorithm;
            s.Initialize();
        });

        public void Update() => States.ForEach(s => s.Update());

        public abstract Task<List<TIndividual>> Generate(int amount);
    }
}