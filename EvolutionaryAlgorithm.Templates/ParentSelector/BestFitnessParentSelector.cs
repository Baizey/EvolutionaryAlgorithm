using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.ParentSelector
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

        public T Select(int index) => 
            Algorithm.Population.Best;
    }
}