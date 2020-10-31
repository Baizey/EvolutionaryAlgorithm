using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EvolutionaryAlgorithm.Template.SatisfiabilityProblem.Structures
{
    public class Problem
    {
        public List<Formula> Formulas { get; set; }
        public int Satisfied(BitArray bitArray) => Formulas.Count(f => f.IsSatisfied(bitArray));
    }
}