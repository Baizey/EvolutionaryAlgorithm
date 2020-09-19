using System.Collections;
using System.Collections.Generic;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class FirstParentSelector : IBitParentSelector
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public IBitIndividual Select(List<IBitIndividual> population) =>
            population[0];
    }
}