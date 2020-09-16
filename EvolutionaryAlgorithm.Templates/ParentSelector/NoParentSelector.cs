using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class NoParentSelector : IBitParentSelector
    {
        public IBitIndividual Select(IBitPopulation population) =>
            null;
    }
}