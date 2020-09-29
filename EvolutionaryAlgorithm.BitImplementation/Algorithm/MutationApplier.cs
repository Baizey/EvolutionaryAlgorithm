using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.BitImplementation.Abstract;

namespace EvolutionaryAlgorithm.BitImplementation.Algorithm
{
    public class MutationApplier
    {
        private readonly Random _random = new Random();

        private readonly Dictionary<long, Dictionary<int, double[]>> _lookup =
            new Dictionary<long, Dictionary<int, double[]>>();

        // Reasonably efficient way of calculating
        // n! / ((n - k)! * k!)
        // Source: https://stackoverflow.com/questions/12983731/algorithm-for-calculating-binomial-coefficient/12992171
        private static double GetnCk(long k, long n)
        {
            double sum = 0;
            for (long i = 0; i < k; i++)
                sum += Math.Log10(n - i) - Math.Log10(i + 1);
            return Math.Pow(10, sum);
        }

        private void CalculateOdds(double p, long n)
        {
            if (!_lookup.ContainsKey(n))
                _lookup[n] = new Dictionary<int, double[]>();
            if (_lookup[n].ContainsKey((int) p)) return;

            var list = new List<double>(16);
            var covered = 0D;

            for (var k = 0; covered < 0.99999D; k++)
            {
                var value = Math.Pow(1D - p / n, n - k)
                            * Math.Pow(p / n, k)
                            * GetnCk(k, n);
                list.Add(value);
                covered += value;
            }

            _lookup[n][(int) p] = list.ToArray();
        }

        public double[] GetOdds(int p, int n)
        {
            CalculateOdds(p, n);
            return _lookup[n][p];
        }

        public void Mutate(IBitIndividual individual, int p, int n)
        {
            var odds = GetOdds(p, n);
            var roll = _random.NextDouble();

            foreach (var d in odds)
            {
                if (roll < d) break;
                roll -= d;
                individual.Flip(_random.Next(individual.Size));
            }
        }
    }
}