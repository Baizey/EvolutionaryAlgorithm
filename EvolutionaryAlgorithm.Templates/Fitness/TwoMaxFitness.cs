using System;
using System.Linq;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public class TwoMaxFitness : IBitFitness
    {
        public double Evaluate(IBitIndividual individual)
        {
            var ones = individual.Count(e => e);
            return Math.Max(ones, individual.Size - ones);
        }
    }
}