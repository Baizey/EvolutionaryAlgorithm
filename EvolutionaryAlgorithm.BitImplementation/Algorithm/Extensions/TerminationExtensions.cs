using System;
using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm.Extensions
{
    public static class TerminationExtensions
    {
        public static Task EvolveUntilTimeout(this IBitEvolutionaryAlgorithm algorithm, TimeSpan time) =>
            algorithm.EvolveUntil(new TimeoutTermination<IBitIndividual, BitArray, bool>(time));
        
        public static Task EvolveUntilFitnessExceed(this IBitEvolutionaryAlgorithm algorithm, double time) =>
            algorithm.EvolveUntil(new FitnessTermination<IBitIndividual, BitArray, bool>(time));
        
        public static Task EvolveUntilFitnessExceed(this IBitEvolutionaryAlgorithm algorithm, Func<IBitEvolutionaryAlgorithm, double> time) =>
            algorithm.EvolveUntil(new FitnessTermination<IBitIndividual, BitArray, bool>(algo => time((IBitEvolutionaryAlgorithm) algo)));
        
        public static Task EvolveUntilStagnationExceed(this IBitEvolutionaryAlgorithm algorithm, double time) =>
            algorithm.EvolveUntil(new StagnationTermination<IBitIndividual, BitArray, bool>(time));
        
        public static Task EvolveUntilStagnationExceed(this IBitEvolutionaryAlgorithm algorithm, Func<IBitEvolutionaryAlgorithm, double> time) =>
            algorithm.EvolveUntil(new StagnationTermination<IBitIndividual, BitArray, bool>(algo => time((IBitEvolutionaryAlgorithm) algo)));
    }
}