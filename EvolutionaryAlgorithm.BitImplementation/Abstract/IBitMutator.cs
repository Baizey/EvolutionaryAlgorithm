﻿using System.Collections;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;

namespace EvolutionaryAlgorithm.BitImplementation.Abstract
{
    public interface IBitMutator : IMutator<IBitIndividual, BitArray, bool>
    {
    }
}