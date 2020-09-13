using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Fitness
{
    public static class FitnessExtension
    {
        public static IBitEvolutionaryAlgorithm
            UsingOneMaxFitness(this IBitEvolutionaryAlgorithm algo) =>
            (IBitEvolutionaryAlgorithm) algo.UsingFitness(new OneMaxFitness());

        public static IBitEvolutionaryAlgorithm
            UsingLeadingOnesFitness(this IBitEvolutionaryAlgorithm algo) =>
            (IBitEvolutionaryAlgorithm) algo.UsingFitness(new LeadingOnesFitness());
    }
}