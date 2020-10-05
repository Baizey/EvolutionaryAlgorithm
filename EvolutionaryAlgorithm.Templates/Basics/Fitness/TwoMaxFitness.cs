using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Fitness;

namespace EvolutionaryAlgorithm.Template.Basics.Fitness
{
    public class TwoMaxFitness<TIndividual> : IBitFitness<TIndividual>
        where TIndividual : IBitIndividual
    {
        public IEvolutionaryAlgorithm<TIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public double Evaluate(TIndividual individual) => Math.Max(individual.Ones, individual.Zeros);
    }
}