using System;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class BasicEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        : IEvolutionaryStatistics<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public TIndividual Best { get; private set; }
        public int StagnantGeneration { get; private set; }

        public long Generations { get; private set; }

        public void Start(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo)
        {
            StartTime = DateTime.Now;
            Best = (TIndividual) algo.Best.Clone();
        }

        public void Update(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo)
        {
            Generations++;
            if (algo.Best.Fitness <= Best.Fitness)
                StagnantGeneration++;
            else
                StagnantGeneration = 0;

            if (algo.Best.Fitness > Best.Fitness)
                Best = (TIndividual) algo.Best.Clone();
        }

        public void Finish(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algo) => EndTime = DateTime.Now;
    }
}