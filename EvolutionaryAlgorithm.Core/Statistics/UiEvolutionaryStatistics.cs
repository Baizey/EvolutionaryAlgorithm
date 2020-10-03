using System;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.Statistics
{
    public class History<T> where T : ICloneable
    {
        private readonly int _maxDataPoints;
        private int _counter;

        public History(int maxDataPoints)
        {
            _maxDataPoints = maxDataPoints;
            if (_maxDataPoints % 2 == 1) _maxDataPoints++;
            Items = new List<T>(_maxDataPoints);
            StepSize = 1;
        }

        public List<T> Items { get; private set; }
        public int StepSize { get; private set; }

        private bool Resize()
        {
            if (Items.Count < _maxDataPoints) return false;
            StepSize *= 2;
            var keep = false;
            Items = Items.Where(e => keep = !keep).ToList();
            return true;
        }

        public void Add(T item)
        {
            if (++_counter < StepSize) return;
            if (Resize()) return;
            _counter = 0;
            Items.Add((T) item.Clone());
        }
    }

    public interface IUiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public History<TIndividual> GeneHistory { get; }
        public History<IParameters> ParameterHistory { get; }
    }

    public class UiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : BasicEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>,
            IUiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public History<TIndividual> GeneHistory { get; }
        public History<IParameters> ParameterHistory { get; }

        public UiEvolutionaryStatistics(int maxDataPoints)
        {
            GeneHistory = new History<TIndividual>(maxDataPoints);
            ParameterHistory = new History<IParameters>(maxDataPoints);
        }

        private void Add()
        {
            GeneHistory.Add(Algorithm.Best);
            ParameterHistory.Add(Algorithm.Parameters);
        }

        public override void Initialize()
        {
            base.Initialize();
            Add();
        }

        public override void Update()
        {
            base.Update();
            Add();
        }
    }
}