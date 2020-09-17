using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class FirstParentSelector : IBitParentSelector
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public IBitIndividual Select(IPopulation<IBitIndividual, BitArray, bool> population) =>
            (IBitIndividual) population[0];
    }
}