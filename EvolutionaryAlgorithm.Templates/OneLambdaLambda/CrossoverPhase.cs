using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.OneLambdaLambda
{
    public class CrossoverPhase : IBitMutation<IBitIndividual>
    {
        private MutationApplier _applier = new MutationApplier();
        private readonly MutationPhase _mutationPhase;
        private IBitIndividual _xMark;
        private int _geneCount;
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public CrossoverPhase()
        {
            _mutationPhase = new MutationPhase();
        }

        public void Initialize()
        {
            _geneCount = Algorithm.Parameters.GeneCount;
            _mutationPhase.Algorithm = Algorithm;
            _mutationPhase.Initialize();
            Update();
        }

        public void Update()
        {
            _mutationPhase.Update();
            _xMark = _mutationPhase.Select(0);
        }

        public void Mutate(int index, IBitIndividual y)
        {
            var lookup = new List<int>();
            for (var i = 0; i < _geneCount; i++)
                if (_xMark[i] != y[i])
                    _applier.Mutate(y, i, 5, _geneCount);

            // TODO: implement, Flip each bit in y(i) that is different in x′ with probability c;
            throw new NotImplementedException();
        }
    }
}