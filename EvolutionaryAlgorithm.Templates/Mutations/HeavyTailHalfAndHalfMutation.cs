﻿using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Parameters;

namespace EvolutionaryAlgorithm.Template.Mutations
{
    public class HeavyTailHalfAndHalfMutation : IBitMutation<IBitIndividual>
    {
        private readonly MutationApplier _applier = new MutationApplier();
        private IParameters _parameters;
        private readonly double _beta;
        private int _n, _upperTail, _lowerTail;
        private int _upperP;
        private int _lowerP;
        private int _tailMax;
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public HeavyTailHalfAndHalfMutation(double beta) => _beta = beta;

        public void Initialize()
        {
            _parameters = Algorithm.Parameters;
            _n = _parameters.GeneCount;
            _tailMax = _n / 2;
            Update();
        }

        public void Update()
        {
            _upperTail = (int) Math.Min(_tailMax, _parameters.MutationRate * 2);
            _lowerTail = (int) Math.Max(1, _parameters.MutationRate / 2);
            _lowerP = _applier.HeavyTail(_lowerTail, _n, _beta);
            _upperP = _applier.HeavyTail(_upperTail, _n, _beta);
        }

        public void Mutate(int index, IBitIndividual child)
        {
            if (index < _parameters.Lambda / 2)
            {
                child.MutationRate = _lowerTail;
                _applier.Mutate(child, _lowerP, _n);
            }
            else
            {
                child.MutationRate = _upperTail;
                _applier.Mutate(child, _upperP, _n);
            }
        }
    }
}