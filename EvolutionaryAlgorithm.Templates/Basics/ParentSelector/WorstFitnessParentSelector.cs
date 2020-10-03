using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.Basics.ParentSelector
{
    public class WorstFitnessParentSelector<T> : IBitSingleParentSelector<T> where T : IBitIndividual
    {
        private T _worst;
        public IEvolutionaryAlgorithm<T, BitArray, bool> Algorithm { get; set; }

        public void Initialize() => Update();

        public void Update() => _worst = Algorithm.Population.Aggregate((a, b) => a.Fitness < b.Fitness ? a : b);

        public T Select(int index) => _worst;
    }
}