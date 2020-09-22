using System;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class EvolutionaryAlgorithmCancelledException : ArgumentException
    {
    }
    
    public class EvolutionaryAlgorithmArgumentException : ArgumentException
    {
        public EvolutionaryAlgorithmArgumentException(string s) : base(s)
        {
        }
    }
}