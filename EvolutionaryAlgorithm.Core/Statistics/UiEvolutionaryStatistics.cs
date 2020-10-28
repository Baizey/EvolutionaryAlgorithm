using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Infrastructure;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.Statistics
{
    public interface IUiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>, ICopyTo<TIndividual>
    {
        public List<TIndividual> GeneHistory { get; set; }
        public List<IParameters> ParameterHistory { get; set; }
    }

    public class UiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : BasicEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>,
            IUiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>, ICopyTo<TIndividual>
    {
        public List<TIndividual> GeneHistory { get; set; }
        public List<IParameters> ParameterHistory { get; set; }

        public UiEvolutionaryStatistics()
        {
        }

        private UiEvolutionaryStatistics(IUiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> other)
            : base(other)
        {
            GeneHistory = other.GeneHistory;
            ParameterHistory = other.ParameterHistory;
            other.GeneHistory = new List<TIndividual>();
            other.ParameterHistory = new List<IParameters>();
        }

        public override void Initialize()
        {
            base.Initialize();
            GeneHistory = new List<TIndividual> {(TIndividual) Algorithm.Best.Clone()};
            ParameterHistory = new List<IParameters> {(IParameters) Algorithm.Parameters.Clone()};
        }

        public override void Update()
        {
            base.Update();
            GeneHistory.Add((TIndividual) Algorithm.Best.Clone());
            ParameterHistory.Add((IParameters) Algorithm.Parameters.Clone());
        }

        public override object Clone() => new UiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>(this);
    }
}