using EvolutionaryAlgorithm.BitImplementation.Abstract;

namespace EvolutionaryAlgorithm.Template.Selection
{
    public static class GenerationFilterExtensions
    {
        public static IBitEvolutionaryAlgorithm UsingElitismGenerationFilter(this IBitEvolutionaryAlgorithm algo) =>
            (IBitEvolutionaryAlgorithm) algo.UsingGenerationFilter(new ElitismGenerationFilter());
    }
}