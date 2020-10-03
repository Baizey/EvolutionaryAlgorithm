using System;
using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.OneLambdaLambda
{
    public class CrossoverPhase : IBitMutation<IBitIndividual>
    {
        private readonly MutationPhase _mutationPhase;
        private IBitIndividual _xMark;
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public CrossoverPhase()
        {
            _mutationPhase = new MutationPhase();
        }

        public void Initialize()
        {
            _mutationPhase.Algorithm = Algorithm;
            _mutationPhase.Initialize();
            Update();
        }

        public void Update()
        {
            _mutationPhase.Update();
            _xMark = _mutationPhase.Select(0);
        }

        public async Task Mutate(int index, IBitIndividual y)
        {
            // TODO: implement, Flip each bit in y(i) that is different in x′ with probability c;
            throw new NotImplementedException();
        }
    }
}