using System.Collections;
using EvolutionaryAlgorithm.Bit.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Template.Basics.ParentSelector
{
    public class BestFitnessParentSelector<T> : IBitSingleParentSelector<T> where T : IBitIndividual
    {
        public IEvolutionaryAlgorithm<T, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        public T Select(int index, IPopulation<T, BitArray, bool> population) => 
            population.Best;
    }
}