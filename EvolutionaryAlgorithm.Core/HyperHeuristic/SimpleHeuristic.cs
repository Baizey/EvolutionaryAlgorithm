using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic
{
    public class SimpleHeuristic<TIndividual, TGeneStructure, TGene>
        : HyperHeuristicBase<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public SimpleHeuristic(IGenerationGenerator<TIndividual, TGeneStructure, TGene> generationGenerator) =>
            States.Add(generationGenerator);

        public override void Update() => States[0].Update();

        public override Task<List<TIndividual>> Generate() => States[0].Generate();
    }
}