using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class NoParentSelector : IBitParentSelector
    {
        public IIndividual<BitArray, bool> Select(IPopulation<BitArray, bool> population) => null;
    }
}