﻿using System;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm.Statistics
{
    public class BasicEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> Algorithm { get; set; }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TIndividual Best { get; private set; }
        public TIndividual Previous { get; private set; }
        public TIndividual Current { get; private set; }
        public int StagnantGeneration { get; private set; }
        public long Generations { get; private set; }

        public void Initialize()
        {
            StartTime = DateTime.Now;
            Current = (TIndividual) Algorithm.Best.Clone();
            Best = Current;
            Previous = Current;
        }

        public void Update()
        {
            Previous = Current;
            Current = (TIndividual) Algorithm.Best.Clone();

            Generations++;

            if (Current.Fitness <= Best.Fitness)
            {
                StagnantGeneration++;
            }
            else
            {
                Best = Current;
                StagnantGeneration = 0;
            }
        }
        
        public void Finish() => EndTime = DateTime.Now;

        public override string ToString() =>
            $"Runtime: {(DateTime.Now - StartTime).TotalSeconds} seconds, Generations: {Generations}, Fitness: {Best.Fitness}";
    }
}