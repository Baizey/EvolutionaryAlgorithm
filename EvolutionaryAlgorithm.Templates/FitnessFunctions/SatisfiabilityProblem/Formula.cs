using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EvolutionaryAlgorithm.Template.FitnessFunctions.SatisfiabilityProblem
{
    public class Formula
    {
        public Formula(int size, int variables)
        {
            var random = new Random();
            for (var j = 0; j < size; j++)
            {
                Variable.Add(random.Next(variables));
                Desired.Add(random.NextDouble() >= 0.5);
            }
        }

        public List<bool> Desired { get; } = new List<bool>();
        public List<int> Variable { get; } = new List<int>();
        public bool IsSatisfied(BitArray bitArray) => Desired.Where((t, i) => bitArray[Variable[i]] == t).Any();
    }
}