using System;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
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

        public BasicUiEvolutionaryStatistics(int maxDataPoints) => _maxDataPoints = maxDataPoints;

        public new void Start(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo)
        {
            base.Start(algo);
            History.Add(Best);
        }

        public new void Update(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo)
        {
            base.Update(algo);

            // Only add every x datapoint
            if (++_counter < StepSize) return;
            _counter = 0;
            History.Add((TIndividual) algo.Best.Clone());

            // If we have more datapoints than we are allowed, trim them to keep every other
            if (History.Count < _maxDataPoints) return;
            StepSize *= 2;
            var keep = true;
            History = History.Where(e => keep = !keep).ToList();
        }
    }
}