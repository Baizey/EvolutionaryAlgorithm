using System;
using System.Collections;
using EvolutionaryAlgorithm.Core;
using EvolutionaryAlgorithm.Core.Individual;

namespace EvolutionaryAlgorithm.Template.Mutation
{
    public class OneMaxStaticOptimalMutation : IMutation<BitArray>
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
            var bufferN = n * Math.Log(n) - n;
            var bufferK = k * Math.Log(k) - k;
            var bufferKn = Math.Abs(n - k) * Math.Log(Math.Abs(n - k)) - Math.Abs(n - k);
            return Math.Exp(bufferN) / (Math.Exp(bufferK) * Math.Exp(bufferKn));
        }

        private BooleanIndividual Mutate(BooleanIndividual individual)
        {
            var roll = _random.NextDouble();
            var mutations = 0;
            for (; mutations < _odds.Length; mutations++)
            {
                roll -= _odds[mutations];
                if (roll <= 0) break;
            }

            for (var m = 0; m < mutations; m++)
            {
                var index = _random.Next(individual.Count);
                individual.Flip(index);
            }

            return individual;
        }

        public IIndividual<BitArray> Mutate(IIndividual<BitArray> individual) => Mutate((BooleanIndividual) individual);
    }
}