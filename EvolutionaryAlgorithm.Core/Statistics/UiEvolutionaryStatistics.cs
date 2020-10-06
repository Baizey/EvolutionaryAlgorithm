using System;
using EvolutionaryAlgorithm.Core.Infrastructure;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.Statistics
{

    public class History<T> where T : ICloneable, ICopyTo<T>
    {
        private readonly int _maxDataPoints;
        private int _counter;
        private readonly T[] _items;

        public T[] Items
        {
            get
            {
                var arr = new T[Count];
                Array.Copy(_items, arr, arr.Length);
                return arr;
            }
        }

        public int StepSize { get; private set; }
        public int Count { get; private set; }

        public History(int maxDataPoints, T first)
        {
            _maxDataPoints = maxDataPoints;
            if (_maxDataPoints % 2 == 1) _maxDataPoints++;
            _items = new T[_maxDataPoints];
            for (var i = 0; i < _maxDataPoints; i++)
                _items[i] = (T) first.Clone();
            Count = 1;
            StepSize = 1;
        }

        private bool Resize()
        {
            if (Count < _maxDataPoints) return false;
            for (int i = 0, j = 0; i < _maxDataPoints; i += 2, j++)
                _items[j] = _items[i];
            StepSize *= 2;
            Count /= 2;
            return true;
        }

        public void Add(T item)
        {
            if (++_counter < StepSize) return;
            if (Resize()) return;
            _counter = 0;
            item.CopyTo(_items[Count++]);
        }
    }

    public interface IUiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>, ICopyTo<TIndividual>
    {
        public History<TIndividual> GeneHistory { get; }
        public History<IParameters> ParameterHistory { get; }
    }

    public class UiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : BasicEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>,
            IUiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>, ICopyTo<TIndividual>
    {
        private readonly int _maxDataPoints;
        public History<TIndividual> GeneHistory { get; private set; }
        public History<IParameters> ParameterHistory { get; private set; }

        public UiEvolutionaryStatistics(int maxDataPoints)
        {
            _maxDataPoints = maxDataPoints;
        }

        public override void Initialize()
        {
            base.Initialize();
            GeneHistory = new History<TIndividual>(_maxDataPoints, Algorithm.Best);
            ParameterHistory = new History<IParameters>(_maxDataPoints, Algorithm.Parameters);
        }

        public override void Update()
        {
            base.Update();
            GeneHistory.Add(Algorithm.Best);
            ParameterHistory.Add(Algorithm.Parameters);
        }
    }
}