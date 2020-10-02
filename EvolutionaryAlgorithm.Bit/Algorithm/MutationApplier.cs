using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Bit.Abstract;

namespace EvolutionaryAlgorithm.Bit.Algorithm
{
    //
    // Calculates and applies mutation on bits with p/n chance of independently flipping
    //
    public class MutationApplier
    {
        // The precision which we apply mutation
        // Default set to handling 99.999% of amounts of flips
        private const double Precision = 0.99999D;
        private readonly Random _random = new Random();

        private readonly Dictionary<int, Dictionary<int, double[]>> _lookup =
            new Dictionary<int, Dictionary<int, double[]>>();

        // Reasonably efficient way of calculating
        // n! / ((n - k)! * k!)
        // Source: https://stackoverflow.com/questions/12983731/algorithm-for-calculating-binomial-coefficient/12992171
        private static double GetnCk(int k, int n)
        {
            double sum = 0;
            for (var i = 0; i < k; i++)
                sum += Math.Log10(n - i) - Math.Log10(i + 1);
            return Math.Pow(10, sum);
        }

        private void CalculateOdds(int p, int n)
        {
            if (!_lookup.ContainsKey(n))
                _lookup[n] = new Dictionary<int, double[]>();
            if (_lookup[n].ContainsKey(p)) return;

            var list = new List<double>(16);
            var covered = 0D;
            var pOverN = (double) p / n;
            for (var k = 0; covered < Precision; k++)
            {
                var value = Math.Pow(1D - pOverN, n - k)
                            * Math.Pow(pOverN, k)
                            * GetnCk(k, n);
                list.Add(value);
                covered += value;
            }

            _lookup[n][p] = list.ToArray();
        }

        public double[] GetOdds(int p, int n)
        {
            CalculateOdds(p, n);
            return _lookup[n][p];
        }

        public void Mutate(IBitIndividual individual, int p, int n)
        {
            CalculateOdds(p, n);
            var roll = _random.NextDouble();
            foreach (var d in _lookup[n][p])
            {
                if (roll < d) break;
                roll -= d;
                individual.Flip(_random.Next(individual.Size));
            }
        }
    }
}