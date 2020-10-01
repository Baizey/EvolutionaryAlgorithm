using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;

namespace EvolutionaryAlgorithm.Core.Algorithm.Parameters
{
    public class StaticParameters
        : IParameters
    {
        public StaticParameters()
        {
        }

        public StaticParameters(IParameters parameters)
        {
            GeneCount = parameters.GeneCount;
            Mu = parameters.Mu;
            Lambda = parameters.Lambda;
            MutationRate = parameters.MutationRate;
        }

        public int GeneCount { get; set; }
        public int Mu { get; set; }
        public int Lambda { get; set; }
        public int MutationRate { get; set; }

        public object Clone() => new StaticParameters(this);
    }
}