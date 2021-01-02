using System;
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

        private readonly Dictionary<int, Dictionary<double, double[]>> _mutationLookup =
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

        private void MutateLazy(IBitIndividual individual, double p, int n)
        {
            var roll = _random.NextDouble();
            foreach (var d in CalculateOdds(p, n))
            {
                if (roll < d) break;
                roll -= d;
                individual.Flip(_random.Next(individual.Count));
            }
        }

        private void MutateDistinct(IBitIndividual individual, double p, int n)
        {
            var roll = _random.NextDouble();
            var flipped = new HashSet<int>();
            foreach (var d in CalculateOdds(p, n))
            {
                if (roll < d) break;
                roll -= d;
                int r;
                do r = _random.Next(individual.Count);
                while (flipped.Contains(r));
                individual.Flip(r);
                flipped.Add(r);
            }
        }

        public void Mutate(IBitIndividual individual, int n) => Mutate(individual, individual.MutationRate, n);

        public void Mutate(IBitIndividual individual, double p, int n)
        {
            if (p >= n / 32D)
                MutateDistinct(individual, p, n);
            else
                MutateLazy(individual, p, n);
        }

        public void MutatePart(IBitIndividual individual, double p, int[] indexes) =>
            MutatePart(individual, p, new SpecializedQueue(indexes, _random));

        public void MutatePart(IBitIndividual individual, double p, SpecializedQueue indexes)
        {
            var n = indexes.Count;
            var roll = _random.NextDouble();
            foreach (var d in CalculateOdds(p, n))
            {
                if (roll < d || indexes.IsEmpty) break;
                roll -= d;
                individual.Flip(indexes.TakeOne());
            }
        }

        public void MutateAsymmetric(IBitIndividual individual, double p, double zeroPart, double onePart)
        {
            onePart *= p;
            zeroPart *= p;
            var genes = individual.Genes;
            var ones = individual.Ones;
            var zeroes = genes.Length - ones;

            var oneLookup = new SpecializedQueue(ones, _random);
            var zeroLookup = new SpecializedQueue(zeroes, _random);
            for (var i = 0; i < genes.Length; i++)
                if (genes[i])
                    oneLookup.Add(i);
                else
                    zeroLookup.Add(i);

            MutatePart(individual, onePart, oneLookup);
            MutatePart(individual, zeroPart, zeroLookup);
        }
    }

    public class SpecializedQueue
    {
        private readonly int[] _internalArray;
        private readonly Random _random;

        public int Count { get; private set; }
        public bool IsEmpty => Count == 0;

        public SpecializedQueue(int capacity, Random random)
        {
            _internalArray = new int[capacity];
            _random = random;
        }

        public SpecializedQueue(int[] internalArray, Random random)
        {
            _random = random;
            _internalArray = internalArray;
            Count = internalArray.Length;
        }

        public void Add(int i) => _internalArray[Count++] = i;

        public int TakeOne()
        {
            var i = _random.Next(Count);
            var removed = _internalArray[i];
            _internalArray[i] = _internalArray[--Count];
            return removed;
        }
    }
}