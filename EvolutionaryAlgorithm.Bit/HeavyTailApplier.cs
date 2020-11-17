using System;
using System.Collections.Generic;
using System.Linq;

namespace EvolutionaryAlgorithm.BitImplementation
{
    public class HeavyTailApplier
    {
        private readonly Dictionary<int, double[]> _heavyTailLookup = new Dictionary<int, double[]>();
        private readonly Random _random = new Random();
        private readonly int _n;

        public HeavyTailApplier(int n) => _n = n;

        public double[] CalculateOdds(int p, double beta)
        {
            if (_heavyTailLookup.TryGetValue(p, out var cache))
                return cache;

            var c = 0D;
            var allOdds = new double[_n / 2];
            for (var i = 1; i <= allOdds.Length; i++)
                c += Math.Pow(i, -beta);
            for (var alpha = 1; alpha <= allOdds.Length; alpha++)
                allOdds[alpha - 1] = 1 / c * Math.Pow(alpha, -beta);

            // Fill out odds according to p
            var odds = new double[_n / 2];
            var index = p - 1;
            Array.Copy(allOdds, 0, odds, index, odds.Length - index);
            if (index != 1)
            {
                Array.Reverse(allOdds);
                Array.Copy(allOdds, allOdds.Length - p, odds, 0, index);
            }

            // Normalize
            var sum = odds.Sum();
            for (var i = 0; i < odds.Length; i++)
                odds[i] /= sum;
            _heavyTailLookup[p] = odds;
            return odds;
        }

        public int GenerateP(int heavyTailP, double beta)
        {
            var odds = CalculateOdds(heavyTailP, beta);
            var roll = _random.NextDouble();
            for (var i = 0; i < odds.Length; i++)
            {
                if (roll < odds[i]) return i + 1;
                roll -= odds[i];
            }

            return heavyTailP;
        }
    }
}