using System;
using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.Core.Population;
using static System.Linq.Enumerable;

namespace EvolutionaryAlgorithm.BitImplementation
{
    public interface IBitIndividual : IIndividual<BitArray, bool>
    {
        public bool this[int i]
        {
            get => Genes[i];
            set => Genes[i] = value;
        }

        bool Flip(int i);
        int Ones { get; }
        int Zeros { get; }
    }

    public class BitIndividual : IBitIndividual
    {
        public static Population<IBitIndividual, BitArray, bool> FromRandom(int mutationRate) =>
            new Population<IBitIndividual, BitArray, bool>(Generate(mutationRate));

        public static Func<int, IBitIndividual> Generate(double mutationRate)
        {
            var random = new Random();
            return g => new BitIndividual(g, () => random.NextDouble() >= 0.5) {MutationRate = mutationRate};
        }
        
        public double Fitness { get; set; }
        public double MutationRate { get; set; }
        public BitArray Genes { get; set; }
        public int Count => Genes.Count;

        public int Ones
        {
            get
            {
                var ones = 0;
                for (var i = 0; i < Count; i++)
                    if (Genes[i])
                        ones++;
                return ones;
            }
        }

        public int Zeros => Count - Ones;

        public BitIndividual(int size, bool defaultValue) => Genes = new BitArray(size, defaultValue);

        public BitIndividual(IBitIndividual individual)
        {
            Fitness = individual.Fitness;
            MutationRate = individual.MutationRate;
            Genes = (BitArray) individual.Genes.Clone();
        }


        public BitIndividual(BitArray bitArray) => Genes = bitArray;

        public BitIndividual(bool[] genes) : this(new BitArray(genes))
        {
        }

        public BitIndividual(int size, Func<bool> generator)
            : this(Range(0, size).Select(_ => generator.Invoke()).ToArray())
        {
        }

        public virtual void CopyTo(IIndividual<BitArray, bool> other)
        {
            Genes.CopyTo(other.Genes);
            other.MutationRate = MutationRate;
            other.Fitness = Fitness;
        }

        public bool Flip(int i) => Genes[i] = !Genes[i];
        public virtual object Clone() => new BitIndividual(this);

        public virtual int CompareTo(IIndividual<BitArray, bool> other)
        {
            var r = Math.Sign(Fitness - other.Fitness);
            return r != 0 ? r : Math.Sign(((IBitIndividual) other).MutationRate - MutationRate);
        }

        public override string ToString() =>
            Fitness + " -> " + string.Join("", Genes.Cast<bool>().Select(e => e ? "1" : "0"));
    }
}