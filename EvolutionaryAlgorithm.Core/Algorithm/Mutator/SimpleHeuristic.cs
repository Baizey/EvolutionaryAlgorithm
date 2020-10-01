using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;

namespace EvolutionaryAlgorithm.Core.Algorithm.Mutator
{
    public class SimpleHeuristic<TIndividual, TGeneStructure, TGene>
        : HyperHeuristicBase<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public SimpleHeuristic(IGenerationGenerator<TIndividual, TGeneStructure, TGene> generationGenerator) =>
            States.Add(generationGenerator);

        public override async Task<List<TIndividual>> Generate() => await States[0].Generate();
    }
}