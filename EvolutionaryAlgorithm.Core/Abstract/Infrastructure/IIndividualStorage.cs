using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Core.Abstract.Infrastructure
{
    public interface IIndividualStorage<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        List<TIndividual> Get(int key, int amount);
        void Dump(int key, List<TIndividual> bodies);
    }
}