using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Template.Endogenous
{
    public interface IEndogenousBitIndividual : IBitIndividual
    {
        public int MutationRate { get; set; }
    }

    public class EndogenousBitIndividual : BitIndividual, IEndogenousBitIndividual
    {
        public static Func<int, EndogenousBitIndividual> Generate(int learningRate)
        {
            var random = new Random();
            return g => new EndogenousBitIndividual(g, () => random.NextDouble() >= 0.5) {MutationRate = learningRate};
        }

        public int MutationRate { get; set; }

        public new void CloneGenesTo(IIndividual<BitArray, bool> other)
        {
            base.CloneGenesTo(other);
            ((IEndogenousBitIndividual) other).MutationRate = MutationRate;
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