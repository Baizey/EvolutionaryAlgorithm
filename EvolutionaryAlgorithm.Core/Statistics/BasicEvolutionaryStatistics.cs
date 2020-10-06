using System;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.Statistics
{
    public class BasicEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public TIndividual Best { get; private set; }
        public TIndividual Previous { get; private set; }
        public TIndividual Current { get; private set; }
        public long StagnantGeneration { get; private set; }
        public long Generations { get; private set; }

        public virtual void Initialize()
        {
            StartTime = DateTime.Now;
            Current = (TIndividual) Algorithm.Best.Clone();
            Best = (TIndividual) Current.Clone();
            Previous = (TIndividual) Current.Clone();
        }

        public virtual void Update()
        {
            var temp = Previous;
            Previous = Current;
            Current = temp;
            Algorithm.Best.CopyTo(Current);

            Generations++;

            if (Current.Fitness <= Previous.Fitness)
                StagnantGeneration++;
            else
                StagnantGeneration = 0;

            if (Current.Fitness > Best.Fitness)
                Current.CopyTo(Best);
        }

        public virtual void Finish() => EndTime = DateTime.Now;

        public override string ToString() =>
            $"Runtime: {(DateTime.Now - StartTime).TotalSeconds} seconds, Generations: {Generations}, Fitness: {Best.Fitness}";
    }
}