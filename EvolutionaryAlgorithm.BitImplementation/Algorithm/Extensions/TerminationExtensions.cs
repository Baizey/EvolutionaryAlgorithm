using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm.Terminations;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions
{
    public static class TerminationExtensions
    {
        public static IBitEvolutionaryAlgorithm EvolveUntilTimeout(this IBitEvolutionaryAlgorithm algorithm, TimeSpan time) =>
            algorithm.UsingTermination(new TimeoutTermination<IBitIndividual, BitArray, bool>(time));
        
        public static IBitEvolutionaryAlgorithm EvolveUntilFitnessExceed(this IBitEvolutionaryAlgorithm algorithm, double time) =>
            algorithm.UsingTermination(new FitnessTermination<IBitIndividual, BitArray, bool>(time));
        
        public static IBitEvolutionaryAlgorithm EvolveUntilFitnessExceed(this IBitEvolutionaryAlgorithm algorithm, Func<IBitEvolutionaryAlgorithm, double> time) =>
            algorithm.UsingTermination(new FitnessTermination<IBitIndividual, BitArray, bool>(algo => time((IBitEvolutionaryAlgorithm) algo)));
        
        public static IBitEvolutionaryAlgorithm EvolveUntilStagnationExceed(this IBitEvolutionaryAlgorithm algorithm, double time) =>
            algorithm.UsingTermination(new StagnationTermination<IBitIndividual, BitArray, bool>(time));
        
        public static IBitEvolutionaryAlgorithm EvolveUntilStagnationExceed(this IBitEvolutionaryAlgorithm algorithm, Func<IBitEvolutionaryAlgorithm, double> time) =>
            algorithm.UsingTermination(new StagnationTermination<IBitIndividual, BitArray, bool>(algo => time((IBitEvolutionaryAlgorithm) algo)));
    }
}