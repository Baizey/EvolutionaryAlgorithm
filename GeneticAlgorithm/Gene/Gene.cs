using System;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Gene
{
    public abstract class Gene<T> : IGene
    {
        public T Value;

        public Gene(T value) => Value = value;

        public Gene(Func<T> generator) => Value = generator.Invoke();

        public override string ToString() => Value.ToString();
    }
}