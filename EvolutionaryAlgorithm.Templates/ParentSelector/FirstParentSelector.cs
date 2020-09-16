using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.ParentSelector
{
    public class FirstParentSelector : IBitParentSelector
    {
        public IBitIndividual Select(IBitPopulation population) =>
            (IBitIndividual) population[0];
    }
}