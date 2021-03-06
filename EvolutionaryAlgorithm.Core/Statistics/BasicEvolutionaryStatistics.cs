﻿using System;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Core.Statistics
{
    public class BasicEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public BasicEvolutionaryStatistics()
        {
        }

        public BasicEvolutionaryStatistics(IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene> other)
        {
            StartTime = other.StartTime;
            EndTime = other.EndTime;
            Best = (TIndividual) other.Best?.Clone();
            Current = (TIndividual) other.Current?.Clone();
            Previous = (TIndividual) other.Previous?.Clone();
            Parameters = (IParameters) other.Parameters.Clone();
            StagnantGeneration = other.StagnantGeneration;
            Generations = other.Generations;
        }

        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public IParameters Parameters { get; private set; }
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
            Parameters = (IParameters) Algorithm.Parameters.Clone();
        }

        public virtual void Update()
        {
            var temp = Previous;
            Previous = Current;
            Current = temp;
            Algorithm.Best.CopyTo(Current);
            Algorithm.Parameters.CopyTo(Parameters);

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

        public virtual object Clone() => new BasicEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>(this);
    }
}