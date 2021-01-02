using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Statistics;

namespace EvolutionaryAlgorithm.Template.Statistics
{
    public class LambdaBasicStatistics<T> : BasicEvolutionaryStatistics<T, bool[], bool>
        where T : IBitIndividual
    {
        public override string ToString()
        {
            return base.ToString() + $", Lambda: {Algorithm.Parameters.Lambda}";
        }
    }
}