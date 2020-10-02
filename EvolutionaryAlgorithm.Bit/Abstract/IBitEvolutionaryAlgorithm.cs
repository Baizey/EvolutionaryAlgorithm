using System.Collections;
using EvolutionaryAlgorithm.Bit.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase.Helpers;

namespace EvolutionaryAlgorithm.BitImplementation.Abstract
{
    public interface IBitEvolutionaryAlgorithm<T>
        : IEvolutionaryAlgorithm<T, BitArray, bool>
        where T : IBitIndividual
    {
    }

    public interface IBitPopulation<T> : IPopulation<T, BitArray, bool> where T : IBitIndividual
    {
    }

    public interface IBitFitness<T> : IFitness<T, BitArray, bool> where T : IBitIndividual
    {
    }

    public interface IBitHyperHeuristic<T> : IHyperHeuristic<T, BitArray, bool> where T : IBitIndividual
    {
    }

    public interface IBitGenerationGenerator<T> : IGenerationGenerator<T, BitArray, bool> where T : IBitIndividual
    {
    }

    public interface IBitGenerationFilter<T> : IGenerationFilter<T, BitArray, bool> where T : IBitIndividual
    {
    }

    public interface IBitMutator<T> : IMutator<T, BitArray, bool> where T : IBitIndividual
    {
    }

    public interface IBitMutation<T> : IMutation<T, BitArray, bool> where T : IBitIndividual
    {
    }

    public interface IBitSingleParentSelector<T> : ISingleParentSelector<T, BitArray, bool> where T : IBitIndividual
    {
    }

    public interface IBitMultiParentSelector<T> : IMultiParentSelector<T, BitArray, bool> where T : IBitIndividual
    {
    }
}