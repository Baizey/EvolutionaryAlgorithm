﻿using System;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class BasicEvolutionaryStatistics<TGeneStructure, TGene> : IEvolutionaryStatistics<TGeneStructure, TGene>
        where TGeneStructure : ICloneable
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public List<IIndividual<TGeneStructure, TGene>> Generations { get; } =
            new List<IIndividual<TGeneStructure, TGene>>();

        public IIndividual<TGeneStructure, TGene> Best { get; private set; }
        public IIndividual<TGeneStructure, TGene> Last => Generations.Last();
        public int StagnantGeneration { get; private set; }

        public int CurrentGeneration => Generations.Count;

        public void Start(IEvolutionaryAlgorithm<TGeneStructure, TGene> algo)
        {
            StartTime = DateTime.Now;
            Update(algo);
        }

        public void Update(IEvolutionaryAlgorithm<TGeneStructure, TGene> algo)
        {
            var clone = algo.Best.Clone() as IIndividual<TGeneStructure, TGene>;

            if (Best == null || algo.Best.Fitness > Best.Fitness)
                Best = clone;

            if (algo.Best.Fitness <= Last.Fitness)
                StagnantGeneration++;
            else
                StagnantGeneration = 0;

            Generations.Add(clone);
        }

        public void Finish(IEvolutionaryAlgorithm<TGeneStructure, TGene> algo) => EndTime = DateTime.Now;
    }
}