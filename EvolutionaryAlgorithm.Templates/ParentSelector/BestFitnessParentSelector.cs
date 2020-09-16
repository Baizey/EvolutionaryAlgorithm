using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class BestFitnessParentSelector : IBitParentSelector
    {
        public IEvolutionaryAlgorithm<BitArray, bool> Algorithm { get; set; }

        public IBitIndividual Select(IBitPopulation population) =>
            (IBitIndividual) population.Individuals.Aggregate((a, b) => a.Fitness > b.Fitness ? a : b);
    }
}