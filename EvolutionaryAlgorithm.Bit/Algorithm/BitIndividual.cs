using System;
using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.Bit.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using static System.Linq.Enumerable;

namespace EvolutionaryAlgorithm.Bit.Algorithm
{
    public class BitIndividual : IBitIndividual
    {
        private int _ones = -1;
        public double Fitness { get; set; }
        public BitArray Genes { get; set; }

        public int Size => Genes.Count;

        public int Ones
        {
            get
            {
                if (_ones != -1) return _ones;
                _ones = 0;
                for (var i = 0; i < Size; i++)
                    if (Genes[i])
                        _ones++;
                return _ones;
            }
            set => _ones = value;
        }

        public int Zeros => Size - Ones;

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

        public virtual void CloneGenesTo(IIndividual<BitArray, bool> other)
        {
            other.Genes = new BitArray(Genes);
            other.Fitness = Fitness;
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