using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Statistics;

namespace EvolutionaryAlgorithm.Template.OneLambdaLambda
{
    public class LambdaBasicStatistics<T> : BasicEvolutionaryStatistics<T, BitArray, bool>
        where T : IBitIndividual
    {
        public override string ToString()
        {
            return base.ToString() + $", Lambda: {Algorithm.Parameters.Lambda}";
        }
    }
}