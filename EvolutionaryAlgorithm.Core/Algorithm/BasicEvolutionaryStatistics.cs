using System;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class
        BasicEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public List<TIndividual> Generations { get; } = new List<TIndividual>();

        public IIndividual<TGeneStructure, TGene> Best { get; private set; }
        public IIndividual<TGeneStructure, TGene> Last => Generations.Last();
        public int StagnantGeneration { get; private set; }

        public int CurrentGeneration => Generations.Count;

        public void Start(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo)
        {
            StartTime = DateTime.Now;
            Best = algo.Best;
            Update(algo);
        }

        public void Update(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo)
        {
            var clone = (TIndividual) algo.Best.Clone();

            if (clone.Fitness > Best.Fitness)
                Best = clone;

            if (clone.Fitness <= Last.Fitness)
                StagnantGeneration++;
            else
                StagnantGeneration = 0;

            Generations.Add(clone);
        }

        public void Finish(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo) => EndTime = DateTime.Now;
    }
}