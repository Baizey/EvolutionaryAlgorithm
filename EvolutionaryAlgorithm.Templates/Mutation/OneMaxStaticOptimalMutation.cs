using System;
using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Mutation
{
    public class OneMaxStaticOptimalMutation : IBitMutation
    {
        private readonly double[] _odds;
        private readonly Random _random;

        public OneMaxStaticOptimalMutation(int n)
        {
            _random = new Random();
            _odds = new double[10];
            for (var k = 0; k < _odds.Length; k++)
                _odds[k] = Math.Pow(1D - 1D / n, n - k)
                           * Math.Pow(1D / n, k)
                           * GetnCk(n, k);
        }

        // Reasonably efficient way of calculating
        // n! / ((n - k)! * k!)
        // Source: https://stackoverflow.com/questions/12983731/algorithm-for-calculating-binomial-coefficient/12992171
        private static double GetnCk(long n, long k)
        {
            double sum = 0;
            for (long i = 0; i < k; i++)
                sum += Math.Log10(n - i) - Math.Log10(i + 1);
            return Math.Pow(10, sum);
        }

        private IBitIndividual Mutate(IBitIndividual individual)
        {
            var roll = _random.NextDouble();
            foreach (var k in _odds)
            {
                if (roll < k) break;
                roll -= k;
                individual.Flip(_random.Next(individual.Size));
            }

            return individual;
        }

        public IIndividual<BitArray, bool>
            Mutate(IIndividual<BitArray, bool> child, IIndividual<BitArray, bool> parent) =>
            Mutate((IBitIndividual) child);
    }
}