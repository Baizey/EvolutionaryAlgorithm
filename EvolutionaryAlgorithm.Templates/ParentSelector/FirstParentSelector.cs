using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class
        FirstParentSelector<T> : IBitSingleParentSelector<T> where T : IBitIndividual
    {
        public IEvolutionaryAlgorithm<T, bool[], bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        public T Select(int index) =>
            Algorithm.Population[0];
    }
}