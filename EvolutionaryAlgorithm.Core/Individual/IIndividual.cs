using System;
using System.Collections;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Individual
{
    public interface IIndividual<T> :
        ICloneable,
        IComparable<IIndividual<T>>
    {
        public int Count { get; }
        public double? Fitness { get; set; }
        public T Genes { get; set; }
    }

    public interface IBooleanIndividual :
        IIndividual<BitArray>,
        IEnumerable
    {
        public bool this[int i]
        {
            get => Genes[i];
            set => Genes[i] = value;
        }

        bool Flip(int i);
    }

    public interface IObjectIndividual<T> :
        IIndividual<List<T>>,
        IEnumerable<T>
        where T : ICloneable
    {
        public T this[int i]
        {
            get => Genes[i];
            set => Genes[i] = value;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => Genes.GetEnumerator();
    }
}