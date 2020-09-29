using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;

namespace EvolutionaryAlgorithm.Core.Algorithm.Mutator
{
    public class SingleHeuristic<TIndividual, TGeneStructure, TGene>
        : HyperHeuristicBase<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public SingleHeuristic(IGenerationGenerator<TIndividual, TGeneStructure, TGene> generationGenerator) =>
            States.Add(generationGenerator);

        public override async Task<List<TIndividual>> Generate(int amount) => await States[0].Generate(amount);
    }
}