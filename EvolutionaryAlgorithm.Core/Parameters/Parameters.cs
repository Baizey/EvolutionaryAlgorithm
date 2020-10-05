﻿namespace EvolutionaryAlgorithm.Core.Parameters
{
    public class Parameters
        : IParameters
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
        public int MutationRate { get; set; }

        public object Clone() => new Parameters(this);
    }
}