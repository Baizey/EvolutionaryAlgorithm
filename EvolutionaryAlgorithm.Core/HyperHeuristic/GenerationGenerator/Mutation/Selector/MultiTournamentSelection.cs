using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation.Selector
{
    public class MultiTournamentSelection<TIndividual, TGeneStructure, TGene>
        : IMultiParentSelector<TIndividual, TGeneStructure, TGene>
        where TIndividual : IIndividual<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        private readonly int _amount;

        private readonly TournamentSelection<TIndividual, TGeneStructure, TGene> _singleSelector =
            new TournamentSelection<TIndividual, TGeneStructure, TGene>();

        public MultiTournamentSelection(int amount) => _amount = amount;

        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public List<TIndividual> Select(int index)
        {
            var list = new List<TIndividual>(_amount);
            for (var i = 0; i < _amount; i++)
                list.Add(_singleSelector.Select(index));
            return list;
        }

        public void Initialize()
        {
            _singleSelector.Algorithm = Algorithm;
            _singleSelector.Initialize();
        }

        public void Update()
        {
            _singleSelector.Update();
        }
    }
}