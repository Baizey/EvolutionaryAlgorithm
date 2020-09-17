using System;
using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Template.Mutation
{
    public class OneMaxStaticOptimalMutation : IBitMutation
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        private readonly double[] _odds;
        private readonly Random _random;

        public OneMaxStaticOptimalMutation(int n)
        {
            _random = new Random();
            _odds = Enumerable.Range(0, 10).Select(k =>
                    Math.Pow(1D - 1D / n, n - k)
                    * Math.Pow(1D / n, k)
                    * GetnCk(n, k))
                .ToArray();
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

        public IBitIndividual Mutate(IBitIndividual child)
        {
            var roll = _random.NextDouble();

            foreach (var k in _odds)
            {
                if (roll < k) break;
                roll -= k;
                child.Flip(_random.Next(child.Size));
            }

            return child;
        }
    }
}