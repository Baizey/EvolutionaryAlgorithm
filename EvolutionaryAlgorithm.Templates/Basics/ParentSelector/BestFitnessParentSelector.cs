using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.Basics.ParentSelector
{
    public class BestFitnessParentSelector : IBitParentSelector
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        public IBitIndividual Select(IPopulation<IBitIndividual, BitArray, bool> population) =>
            population.Best;
    }
}