﻿using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IIndividual<TGeneStructure, TGene> :
        ICloneable,
        IComparable<IIndividual<TGeneStructure, TGene>>
    {
        public int Size { get; }
        public double Fitness { get; set; }
        public TGeneStructure Genes { get; set; }
        public void CloneGenesTo(IIndividual<TGeneStructure, TGene> other);
        public void Reset();
    }

    public interface IObjectIndividual<T> : IIndividual<List<T>, T>
        where T : ICloneable
    {
        public T this[int i]
        {
            get => Genes[i];
            set => Genes[i] = value;
        }
    }
}