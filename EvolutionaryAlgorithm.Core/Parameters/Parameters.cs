namespace EvolutionaryAlgorithm.Core.Parameters
{
    public class Parameters : IParameters
    {
        public Parameters()
        {
        }

        public Parameters(IParameters parameters)
        {
            GeneCount = parameters.GeneCount;
            Mu = parameters.Mu;
            Lambda = parameters.Lambda;
            MutationRate = parameters.MutationRate;
        }

        public int GeneCount { get; set; }
        public int Mu { get; set; }
        public int Lambda { get; set; }
        public double MutationRate { get; set; }

        public object Clone() => new Parameters(this);

        public void CopyTo(IParameters other)
        {
            other.GeneCount = GeneCount;
            other.Mu = Mu;
            other.Lambda = Lambda;
            other.MutationRate = MutationRate;
        }
    }
}