using System;
using System.Collections;
using EvolutionaryAlgorithm.Core.Population;

namespace EvolutionaryAlgorithm.BitImplementation
{
    public interface IEndogenousBitIndividual : IBitIndividual
    {
        public double MutationRate { get; set; }
    }


    public class EndogenousBitIndividual : BitIndividual, IEndogenousBitIndividual
    {
        public double MutationRate { get; set; }

        public static Population<IEndogenousBitIndividual, BitArray, bool> FromRandom(int mutationRate) =>
            new Population<IEndogenousBitIndividual, BitArray, bool>(Generate(mutationRate));

        public static Func<int, EndogenousBitIndividual> Generate(double mutationRate)
        {
            var random = new Random();
            return g => new EndogenousBitIndividual(g, () => random.NextDouble() >= 0.5) {MutationRate = mutationRate};
        }

        public override object Clone() => new EndogenousBitIndividual(this);

        public EndogenousBitIndividual(int size, bool defaultValue) : base(size, defaultValue)
        {
        }

        public EndogenousBitIndividual(IEndogenousBitIndividual individual) : base(individual) =>
            MutationRate = individual.MutationRate;

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