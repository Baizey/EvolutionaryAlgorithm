using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public class TwoMaxFitness : IBitFitness
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        public double Evaluate(IBitIndividual individual) => Math.Max(individual.Ones, individual.Zeros);
    }
}