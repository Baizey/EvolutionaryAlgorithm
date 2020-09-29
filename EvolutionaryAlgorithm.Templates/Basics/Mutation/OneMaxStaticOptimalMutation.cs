using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Template.Basics.Mutation
{
    public class DynamicMutation
    {
        private static DynamicMutation _instance;
        public static DynamicMutation Instance => _instance ??= new DynamicMutation();

        private Random _random = new Random();

        private readonly Dictionary<long, Dictionary<long, double[]>> _lookup =
            new Dictionary<long, Dictionary<long, double[]>>();

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

        private double[] GetOdds(long n, long p)
        {
            if (_lookup.ContainsKey(n) && _lookup[n].ContainsKey(p))
                return _lookup[n][p];
            var odds = Enumerable.Range(0, 20).Select(k =>
                    Math.Pow(1D - 1D / n, n - k)
                    * Math.Pow(1D / n, k)
                    * GetnCk(n, k))
                .ToArray();
            _lookup[n][p] = odds;
            return odds;
        }


        public void Mutate(IBitIndividual individual, long n, long p)
        {
            var odds = GetOdds(n, p);
            var roll = _random.NextDouble();

            foreach (var d in odds)
            {
                if (roll < d) break;
                roll -= d;
                individual.Flip(_random.Next(individual.Size));
            }
        }
    }

    public class OneMaxStaticOptimalMutation : IBitMutation
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        private double[] _odds;
        private Random _random;

        public void Initialize()
        {
            _random = new Random();
            var n = Algorithm.Population[0].Size;
            _odds = Enumerable.Range(0, 10).Select(k =>
                    Math.Pow(1D - 1D / n, n - k)
                    * Math.Pow(1D / n, k)
                    * GetnCk(n, k))
                .ToArray();
        }

        public void Update()
        {
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

        public async Task Mutate(int index, IBitIndividual child)
        {
            var roll = _random.NextDouble();

            foreach (var k in _odds)
            {
                if (roll < k) break;
                roll -= k;
                child.Flip(_random.Next(child.Size));
            }
        }
    }
}