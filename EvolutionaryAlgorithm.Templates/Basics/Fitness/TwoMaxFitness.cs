using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Template.Basics.Fitness
{
    public class TwoMaxFitness<TIndividual> : IFitness<TIndividual, BitArray, bool>
        where TIndividual : IBitIndividual
    {
        public IEvolutionaryAlgorithm<TIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public double Evaluate(TIndividual individual) => Math.Max(individual.Ones, individual.Zeros);
    }
}