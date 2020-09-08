using System;
using System.Collections;
using System.Linq;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Individual
{
    public class BooleanIndividual : IndividualBase<BitArray>, IEnumerable
    {
        public override int Count => Genes.Count;

        // ReSharper disable once MemberCanBePrivate.Global
        public BooleanIndividual(BitArray genes) : base(genes)
        {
        }

        public BooleanIndividual(bool[] genes) : base(new BitArray(genes))
        {
        }

        public BooleanIndividual(int size, Func<bool> generator) : base(
            new BitArray(Enumerable.Range(0, size).Select(_ => generator.Invoke()).ToArray()))
        {
        }

        public bool Flip(int i) => Genes[i] = !Genes[i];

        public bool this[int i]
        {
            get => Genes[i];
            set => Genes[i] = value;
        }

        public IEnumerator GetEnumerator() => Genes.GetEnumerator();

        public override IIndividual Clone() => new BooleanIndividual((BitArray) Genes.Clone());
    }
}