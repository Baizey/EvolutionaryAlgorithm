﻿using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.Template.Endogenous
{
    public interface IEndogenousBitIndividual : IBitIndividual
    {
        public int MutationRate { get; set; }
    }


    public class EndogenousBitIndividual : BitIndividual, IEndogenousBitIndividual
    {
        public static Population<IEndogenousBitIndividual, BitArray, bool> FromRandom(int mutationRate) =>
            new Population<IEndogenousBitIndividual, BitArray, bool>(Generate(mutationRate));

        public static Func<int, EndogenousBitIndividual> Generate(int mutationRate)
        {
            var random = new Random();
            return g => new EndogenousBitIndividual(g, () => random.NextDouble() >= 0.5) {MutationRate = mutationRate};
        }

        public int MutationRate { get; set; }

        public override void CloneGenesTo(IIndividual<BitArray, bool> other)
        {
            base.CloneGenesTo(other);
            ((IEndogenousBitIndividual) other).MutationRate = MutationRate;
        }

        public override object Clone()
        {
            return new EndogenousBitIndividual(this);
        }

        public EndogenousBitIndividual(int size, bool defaultValue) : base(size, defaultValue)
        {
        }

        public EndogenousBitIndividual(IBitIndividual individual) : base(individual)
        {
        }

        public EndogenousBitIndividual(BitArray bitArray) : base(bitArray)
        {
        }

        public EndogenousBitIndividual(bool[] genes) : base(genes)
        {
        }

        public EndogenousBitIndividual(int size, Func<bool> generator) : base(size, generator)
        {
        }
    }
}