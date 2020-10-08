using System;
using EvolutionaryAlgorithm.Core.Infrastructure;

namespace EvolutionaryAlgorithm.Core.Parameters
{
    public interface IParameters : ICloneable, ICopyTo<IParameters>
    {
        public int GeneCount { get; set; }
        public int Mu { get; set; }
        public int Lambda { get; set; }
        public double MutationRate { get; set; }
    }
}