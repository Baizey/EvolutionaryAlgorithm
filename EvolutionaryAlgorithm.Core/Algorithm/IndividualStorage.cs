using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm.Core.Abstract;

namespace EvolutionaryAlgorithm.Core.Algorithm
{
    public class IndividualStorage<TIndividual, TGeneStructure, TGene>
        : IIndividualStorage<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        private List<TIndividual> _reserves;
        private readonly TIndividual _example;

        public IndividualStorage(TIndividual example) => _example = example;

        private void Refill(int amount)
        {
            _reserves ??= new List<TIndividual>(amount);
            var missing = amount - _reserves.Count;
            for (var i = 0; i < missing; i++)
                _reserves.Add((TIndividual) _example.Clone());
        }

        public List<TIndividual> Get(int amount)
        {
            Refill(amount);
            if (_reserves.Count == amount)
            {
                var used = _reserves;
                _reserves = null;
                return used;
            }
            else
            {
                var used = _reserves.GetRange(0, amount);
                _reserves = _reserves.GetRange(amount, _reserves.Count - amount);
                return used;
            }
        }

        public void Dump(List<TIndividual> bodies)
        {
            if (_reserves == null)
                _reserves = bodies;
            else
                _reserves.AddRange(bodies);
        }
    }
}