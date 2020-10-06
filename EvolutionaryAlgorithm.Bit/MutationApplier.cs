using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EvolutionaryAlgorithm.BitImplementation
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

        private readonly Dictionary<int, Dictionary<double, double[]>> _mutationLookup =
            new Dictionary<int, Dictionary<double, double[]>>();

        private readonly Dictionary<int, Dictionary<int, double[]>> _heavyTailLookup =
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

        public double[] CalculateOdds(double p, int n)
        {
            if (!_mutationLookup.ContainsKey(n))
                _mutationLookup[n] = new Dictionary<double, double[]>();
            if (_mutationLookup[n].ContainsKey(p)) return _mutationLookup[n][p];

            var list = new List<double>(16);
            var covered = 0D;
            for (var k = 0; covered < Precision; k++)
            {
                var value = Math.Pow(1D - p / n, n - k)
                            * Math.Pow(p / n, k)
                            * GetnCk(k, n);
                list.Add(value);
                covered += value;
            }

            _mutationLookup[n][p] = list.ToArray();
            return _mutationLookup[n][p];
        }

        public int HeavyTail(int p, int n, double beta)
        {
            double[] odds;
            if (!_heavyTailLookup.ContainsKey(n)) _heavyTailLookup[n] = new Dictionary<int, double[]>();
            if (!_heavyTailLookup[n].ContainsKey(p))
            {
                var c = 0D;
                var allOdds = new double[n / 2];
                for (var i = 1; i < allOdds.Length; i++)
                    c += Math.Pow(i, -beta);
                for (var alpha = 1; alpha < allOdds.Length; alpha++)
                    allOdds[alpha] = 1 / c * Math.Pow(alpha, -beta);

                // Fill out odds according to p
                odds = new double[n / 2];
                for (var i = p; i < odds.Length; i++)
                    odds[i] = allOdds[i - p];
                for (var i = p; i > 0; i--)
                    odds[i] = allOdds[i];

                // Normalize
                var sum = odds.Sum();
                for (var i = 0; i < odds.Length; i++)
                    odds[i] /= sum;
                _heavyTailLookup[n][p] = odds;
            }

            odds = _heavyTailLookup[n][p];
            var roll = _random.NextDouble();
            for (var i = 1; i < odds.Length; i++)
            {
                if (roll < odds[i]) return i;
                roll -= odds[i];
            }

            return p;
        }

        public void Mutate(IBitIndividual individual, int index, double p, int n)
        {
            if (_random.NextDouble() <= p / n)
                individual.Flip(index);
        }

        public void Mutate(IBitIndividual individual, double p, int n)
        {
            var roll = _random.NextDouble();
            foreach (var d in CalculateOdds(p, n))
            {
                if (roll < d) break;
                roll -= d;
                individual.Flip(_random.Next(individual.Count));
            }
        }

        private void MutatePart(IBitIndividual individual, IReadOnlyList<int> lookup, double p)
        {
            var n = lookup.Count;
            var roll = _random.NextDouble();
            foreach (var d in CalculateOdds(p, n))
            {
                if (roll < d) break;
                roll -= d;
                individual.Flip(lookup[_random.Next(n)]);
            }
        }

        public void MutateZeroes(IBitIndividual individual, double p)
        {
            var genes = individual.Genes;
            var lookup = new int[individual.Zeros];
            for (int i = 0, c = 0; i < genes.Count; i++)
                if (!genes[i])
                    lookup[c++] = i;
            MutatePart(individual, lookup, p);
        }

        public void MutateOnes(IBitIndividual individual, double p)
        {
            var genes = individual.Genes;
            var lookup = new int[individual.Ones];
            for (int i = 0, c = 0; i < genes.Count; i++)
                if (genes[i])
                    lookup[c++] = i;
            MutatePart(individual, lookup, p);
        }

        public void MutateAsymmetric(IBitIndividual individual, double p, double zeroPart, double onePart)
        {
            onePart *= p;
            zeroPart *= p;
            var genes = individual.Genes;

            var oneLookup = new int[individual.Ones];
            var zeroLookup = new int[individual.Zeros];
            for (int i = 0, o = 0, z = 0; i < genes.Count; i++)
                if (genes[i])
                    oneLookup[o++] = i;
                else
                    zeroLookup[z++] = i;

            MutatePart(individual, oneLookup, onePart);
            MutatePart(individual, zeroLookup, zeroPart);
        }
    }
}