using System;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Gene
{
    public abstract class GeneBase<T> : IGene
    {
        public T Value;

        public GeneBase(T value) => Value = value;

        public GeneBase(Func<T> generator) => Value = generator.Invoke();

        public override string ToString() => Value.ToString();
    }
}