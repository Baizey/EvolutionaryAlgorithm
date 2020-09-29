using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Template.Basics.ParentSelector
{
    public class LambdaParentSelector : IBitSingleParentSelector
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

        public IBitIndividual Select(int index, IPopulation<IBitIndividual, BitArray, bool> population) =>
            _selector.Invoke(population);
    }
}