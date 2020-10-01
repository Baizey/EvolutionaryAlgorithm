using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.BitImplementation.Algorithm;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Template.Basics.Mutation
{
    public class OneMaxStaticOptimalMutation : IBitMutation
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        private readonly MutationApplier _applier = new MutationApplier();
        private int _geneCount;

        public void Initialize()
        {
            _geneCount = Algorithm.Parameters.GeneCount;
        }

        public void Update()
        {
        }

        public async Task Mutate(int index, IBitIndividual child) => _applier.Mutate(child, 1, _geneCount);
    }
}