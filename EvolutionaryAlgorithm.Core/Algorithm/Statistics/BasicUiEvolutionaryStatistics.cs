using System;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;

namespace EvolutionaryAlgorithm.Core.Algorithm.Statistics
{
    public class BasicUiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : BasicEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>,
            IUiEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        private readonly int _maxDataPoints;
        private int _counter;

        public int StepSize { get; private set; } = 1;

        public List<TIndividual> History { get; private set; } = new List<TIndividual>();

        public BasicUiEvolutionaryStatistics(int maxDataPoints) => _maxDataPoints = maxDataPoints % 2 == 0
            ? maxDataPoints
            : maxDataPoints + 1;

        private void Resize()
        {
            StepSize *= 2;
            var keep = false;
            History = History.Where(e => keep = !keep).ToList();
        }

        public new void Initialize()
        {
            base.Initialize();
            History.Add(Best);
        }

        public new void Update()
        {
            base.Update();

            // Only add every x datapoint
            if (++_counter < StepSize) return;
            _counter = 0;

            // If we have more datapoints than we are allowed, trim them to keep every other
            if (History.Count >= _maxDataPoints) Resize();

            History.Add((TIndividual) Algorithm.Best.Clone());
        }
    }
}