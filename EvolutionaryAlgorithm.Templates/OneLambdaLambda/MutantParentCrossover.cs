using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase.Helpers;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Algorithm.Crossover;

namespace EvolutionaryAlgorithm.Template.OneLambdaLambda
{
    public class MutantParentCrossover : SingleParentCrossoverBase<IBitIndividual, BitArray, bool>
    {
        private readonly int _lambdaC;
        private readonly ISingleParentSelector<IBitIndividual, BitArray, bool> _select;
        private IndividualStorage<IBitIndividual, BitArray, bool> _storage;

        public MutantParentCrossover(
            ISingleParentSelector<IBitIndividual, BitArray, bool> initialSelector,
            int lambdaM,
            int lambdaC
        )
            : base(new MutantParentSelector(initialSelector, lambdaM))
        {
            _lambdaC = lambdaC;
            _select = initialSelector;
        }

        public override void Initialize()
        {
            base.Initialize();
            _storage = new IndividualStorage<IBitIndividual, BitArray, bool>(Algorithm);
        }

        public override async Task Crossover(int index, IBitIndividual child, IBitIndividual parent)
        {
            var bodies = _storage.Get(index, _lambdaC);
            var initial = _select.Select(index, Algorithm.Population);

            foreach (var body in bodies)
            {
                initial.CloneGenesTo(body);
                // TODO: implement mutation based on parent
                throw new NotImplementedException();
            }

            bodies.ForEach(b => b.Fitness = Algorithm.Fitness.Evaluate(b));
            var best = bodies.Aggregate((a, b) => a.Fitness > b.Fitness ? a : b);
            best.CloneGenesTo(child);
            _storage.Dump(index, bodies);
        }
    }
}