using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions;

namespace EvolutionaryAlgorithm.Template.Selection
{
    public static class GenerationFilterExtensions
    {
        public static IBitEvolutionaryAlgorithm UsingElitismGenerationFilter(this IBitEvolutionaryAlgorithm algo) =>
            algo.UsingGenerationFilter(new ElitismGenerationFilter());
    }
}