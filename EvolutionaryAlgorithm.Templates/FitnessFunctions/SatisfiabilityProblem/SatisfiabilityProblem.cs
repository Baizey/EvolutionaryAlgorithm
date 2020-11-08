using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EvolutionaryAlgorithm.Template.FitnessFunctions.SatisfiabilityProblem
{
    public class SatisfiabilityProblem
    {
        public static SatisfiabilityProblem Generate(
            int variables,
            double formulaRatio,
            int formulaSize = 3) =>
            Generate(
                variables,
                (int) Math.Ceiling(variables * formulaRatio),
                formulaSize);

        public static SatisfiabilityProblem Generate(
            int variables,
            int formulas,
            int formulaSize = 3) => new SatisfiabilityProblem
        {
            Formulas = Enumerable.Range(0, formulas)
                .Select(_ => new Formula(formulaSize, variables))
                .ToList()
        };

        public IList<Formula> Formulas { get; private set; } = new List<Formula>();
        public int Satisfied(BitArray bitArray) => Formulas.Count(f => f.IsSatisfied(bitArray));
    }
}