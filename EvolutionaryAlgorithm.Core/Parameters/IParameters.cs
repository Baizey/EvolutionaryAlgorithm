﻿using System;

namespace EvolutionaryAlgorithm.Core.Parameters
{
    public interface IParameters : ICloneable
    {
        public int GeneCount { get; set; }
        public int Mu { get; set; }
        public int Lambda { get; set; }
        public int MutationRate { get; set; }
    }
}