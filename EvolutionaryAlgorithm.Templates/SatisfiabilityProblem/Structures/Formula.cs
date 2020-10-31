using System.Collections;
using System.Linq;

namespace EvolutionaryAlgorithm.Template.SatisfiabilityProblem.Structures
{
    public class Formula
    {
        public bool[] Desired { get; set; }
        public int[] Variable { get; set; }
        public bool IsSatisfied(BitArray bitArray) => Desired.Where((t, i) => bitArray[Variable[i]] == t).Any();
    }
}