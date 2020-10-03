using System;
using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.HyperHeuristic.GenerationGenerator.Mutation;

namespace EvolutionaryAlgorithm.Template.Endogenous
{
    public class EndogenousMutation
        : IMutation<IEndogenousBitIndividual, BitArray, bool>
    {
        private readonly MutationApplier _applier = new MutationApplier();
        private readonly int _learningRate;
        private readonly Random _random = new Random();
        private int _geneCount;
        public IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> Algorithm { get; set; }

        public EndogenousMutation(int learningRate) => _learningRate = learningRate;

        public void Initialize() => _geneCount = Algorithm.Parameters.GeneCount;

        public void Update()
        {
        }

        public Task Mutate(int index, IEndogenousBitIndividual child)
        {
            if (_random.NextDouble() >= 0.5)
                child.MutationRate /= _learningRate;
            else
                child.MutationRate *= _learningRate;
            _applier.Mutate(child, child.MutationRate, _geneCount);
            return Task.CompletedTask;
        }
    }
}