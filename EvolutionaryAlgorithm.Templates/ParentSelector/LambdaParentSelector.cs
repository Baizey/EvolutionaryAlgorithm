using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class LambdaParentSelector : IBitParentSelector
    {
        private readonly Func<IPopulation<IBitIndividual, BitArray, bool>, IBitIndividual> _selector;
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public LambdaParentSelector(Func<IPopulation<IBitIndividual, BitArray, bool>, IBitIndividual> selector) =>
            _selector = selector;

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        public IBitIndividual Select(IPopulation<IBitIndividual, BitArray, bool> population) =>
            _selector.Invoke(population);
    }
}