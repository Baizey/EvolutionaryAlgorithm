using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Template.Basics.ParentSelector
{
    public class WorstFitnessParentSelector : IBitSingleParentSelector
    {
        private IBitIndividual _worst;
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize() => Update();

        public void Update() => _worst = Algorithm.Population.Aggregate((a, b) => a.Fitness < b.Fitness ? a : b);

        public IBitIndividual Select(int index, IPopulation<IBitIndividual, BitArray, bool> population) => _worst;
    }
}