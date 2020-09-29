using System;
using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase;
using EvolutionaryAlgorithm.Template.Basics.Mutation;

namespace EvolutionaryAlgorithm.Template.Endogenous
{
    public class EndogenousMutation
        : IMutation<IEndogenousBitIndividual, BitArray, bool>
    {
        private readonly int _learningRate;
        private readonly Random _random = new Random();
        private int _geneCount;
        public IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> Algorithm { get; set; }

        public EndogenousMutation(int learningRate) => _learningRate = learningRate;

        public void Initialize()
        {
            _geneCount = Algorithm.Parameters.GeneCount;
        }

        public void Update()
        {
        }

        public Task Mutate(int index, IEndogenousBitIndividual child)
        {
            if (_random.NextDouble() >= 0.5)
                child.MutationRate /= _learningRate;
            else
                child.MutationRate *= _learningRate;
            DynamicMutation.Instance.Mutate(child, _geneCount, child.MutationRate);
            return Task.CompletedTask;
        }
    }
}