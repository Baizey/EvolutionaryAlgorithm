using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class WorstFitnessParentSelector : IBitParentSelector
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public IBitIndividual Select(IPopulation<IBitIndividual, BitArray, bool> population) =>
            (IBitIndividual) population.Individuals.Aggregate((a, b) => a.Fitness < b.Fitness ? a : b);
    }
}