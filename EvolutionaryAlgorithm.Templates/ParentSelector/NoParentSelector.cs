using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class NoParentSelector : IBitParentSelector
    {
        public IEvolutionaryAlgorithm<BitArray, bool> Algorithm { get; set; }

        public IBitIndividual Select(IBitPopulation population) =>
            null;
    }
}