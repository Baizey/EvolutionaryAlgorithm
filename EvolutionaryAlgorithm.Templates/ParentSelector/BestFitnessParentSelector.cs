using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class BestFitnessParentSelector : IBitParentSelector
    {
        public IIndividual<BitArray, bool> Select(IPopulation<BitArray, bool> population) =>
            population.Individuals.Aggregate((a, b) => a.Fitness > b.Fitness ? a : b);
    }
}