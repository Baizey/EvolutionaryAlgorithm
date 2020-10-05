using System;
using System.Collections;
using System.Collections.Generic;

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

        private readonly Dictionary<int, Dictionary<double, double[]>> _lookup =
            new Dictionary<int, Dictionary<double, double[]>>();

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

        private void CalculateOdds(double p, int n)
        {
            if (!_lookup.ContainsKey(n))
                _lookup[n] = new Dictionary<double, double[]>();
            if (_lookup[n].ContainsKey(p)) return;

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

            _lookup[n][p] = list.ToArray();
        }

        public double[] GetOdds(int p, int n)
        {
            CalculateOdds(p, n);
            return _lookup[n][p];
        }

        public void Mutate(IBitIndividual individual, int index, double p, int n)
        {
            if (_random.NextDouble() <= p / n) 
                individual.Flip(index);
        }

        public void Mutate(IBitIndividual individual, double p, int n)
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

        private void MutatePart(IBitIndividual individual, IReadOnlyList<int> lookup, double p)
        {
            var n = lookup.Count;
            CalculateOdds(p, n);
            var roll = _random.NextDouble();
            foreach (var d in _lookup[n][p])
            {
                if (roll < d) break;
                roll -= d;
                individual.Flip(lookup[_random.Next(n)]);
            }
        }

        public void MutateZeroes(IBitIndividual individual, int p)
        {
            var genes = individual.Genes;
            var lookup = new int[individual.Zeros];
            for (int i = 0, counter = 0; i < genes.Count; i++)
                if (!genes[i])
                    lookup[counter++] = i;
            MutatePart(individual, lookup, p);
        }

        public void MutateOnes(IBitIndividual individual, int p)
        {
            var genes = individual.Genes;
            var lookup = new int[individual.Ones];
            for (int i = 0, counter = 0; i < genes.Count; i++)
                if (genes[i])
                    lookup[counter++] = i;
            MutatePart(individual, lookup, p);
        }

        public void Mutate(IBitIndividual individual, int p, double zeroPart, double onePart)
        {
            onePart *= p;
            zeroPart *= p;
            var genes = individual.Genes;

            var oneLookup = new int[individual.Ones];
            var zeroLookup = new int[individual.Zeros];
            for (int i = 0, o = 0, z = 0; i < genes.Count; i++)
                if (genes[i]) oneLookup[o++] = i;
                else zeroLookup[z++] = i;

            MutatePart(individual, oneLookup, onePart);
            MutatePart(individual, zeroLookup, zeroPart);
        }
    }
}