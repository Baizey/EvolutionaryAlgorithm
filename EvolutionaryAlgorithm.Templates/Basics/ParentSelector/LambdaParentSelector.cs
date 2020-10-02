using System;
using System.Collections;
using EvolutionaryAlgorithm.Bit.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Template.Basics.ParentSelector
{
    public class LambdaParentSelector<T> : IBitSingleParentSelector<T> where T : IBitIndividual
    {
        private readonly Func<IPopulation<T, BitArray, bool>, T> _selector;
        public IEvolutionaryAlgorithm<T, BitArray, bool> Algorithm { get; set; }

        public LambdaParentSelector(Func<IPopulation<T, BitArray, bool>, T> selector) =>
            _selector = selector;

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        public T Select(int index, IPopulation<T, BitArray, bool> population) =>
            _selector.Invoke(population);
    }
}