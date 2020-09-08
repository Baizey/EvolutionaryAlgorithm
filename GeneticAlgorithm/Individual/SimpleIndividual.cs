using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Individual
{
    public class SimpleIndividual<T> : IndividualBase<List<T>>, IEnumerable<T>
        where T : IGene
    {
        public override int Count => Genes.Count;

        // ReSharper disable once PublicConstructorInAbstractClass
        public SimpleIndividual(List<T> genes) : base(genes)
        {
        }

        // ReSharper disable once PublicConstructorInAbstractClass
        public SimpleIndividual(int size, Func<T> generator) : base(Enumerable.Range(0, size)
            .Select(_ => generator.Invoke()).ToList())
        {
        }

        public T this[int i]
        {
            get => Genes[i];
            set => Genes[i] = value;
        }

        public override IIndividual Clone() => new SimpleIndividual<T>(Genes.Select(gene => (T) gene.Clone()).ToList());

        public IEnumerator GetEnumerator() => Genes.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => Genes.GetEnumerator();

        public override string ToString() => Genes.ToString();
    }
}