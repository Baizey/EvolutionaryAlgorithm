using System;
using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public class TwoMaxFitness : IBitFitness
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public double Evaluate(IBitIndividual individual)
        {
            var ones = individual.Count(e => e);
            return Math.Max(ones, individual.Size - ones);
        }
    }
}