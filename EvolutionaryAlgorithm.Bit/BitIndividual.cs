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
        int Ones { get; set; }
        int Zeros { get; }
    }

    public class BitIndividual : IBitIndividual
    {
        private int _ones = -1;
        public double Fitness { get; set; }
        public BitArray Genes { get; set; }
        public int Count => Genes.Count;

        public int Ones
        {
            get
            {
                if (_ones != -1) return _ones;
                _ones = 0;
                for (var i = 0; i < Count; i++)
                    if (Genes[i])
                        _ones++;
                return _ones;
            }
            set => _ones = value;
        }

        public int Zeros => Count - Ones;

        public BitIndividual(int size, bool defaultValue) => Genes = new BitArray(size, defaultValue);

        public BitIndividual(IBitIndividual individual)
        {
            Fitness = individual.Fitness;
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
            other.Fitness = Fitness;
            other.Reset();
        }

        public void Reset()
        {
            _ones = -1;
        }

        public bool Flip(int i) => Genes[i] = !Genes[i];
        public virtual object Clone() => new BitIndividual(this);

        public int CompareTo(IIndividual<BitArray, bool> other) => Math.Sign(Fitness - other.Fitness);

        public override string ToString() =>
            Fitness + " -> " + string.Join("", Genes.Cast<bool>().Select(e => e ? "1" : "0"));
    }
}