using System;
using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Algorithm.Crossover;

namespace EvolutionaryAlgorithm.Template.OneLambdaLambda
{
    public class MutantParentCrossover : SingleParentCrossoverBase<IBitIndividual, BitArray, bool>
    {
        private readonly int _lambdaC;
        private readonly IParentSelector<IBitIndividual, BitArray, bool> _select;
        private IndividualStorage<IBitIndividual, BitArray, bool> _storage;

        public MutantParentCrossover(
            IParentSelector<IBitIndividual, BitArray, bool> initialSelector,
            int lambdaM,
            int lambdaC
        )
            : base(new MutantParentSelector(initialSelector, lambdaM))
        {
            _lambdaC = lambdaC;
            _select = initialSelector;
        }

        public new void Initialize()
        {
            base.Initialize();
            _storage = new IndividualStorage<IBitIndividual, BitArray, bool>(Algorithm);
        }

        public new void Update() => base.Update();

        public override void Crossover(IBitIndividual child, IBitIndividual parent)
        {
            var bodies = _storage.Get(_lambdaC);
            var initial = _select.Select(Algorithm.Population);

            foreach (var body in bodies)
            {
                initial.CloneGenesTo(body);
                // TODO: implement mutation based on parent
                throw new NotImplementedException();
            }

            bodies.ForEach(b => b.Fitness = Algorithm.Fitness.Evaluate(b));
            var best = bodies.Aggregate((a, b) => a.Fitness > b.Fitness ? a : b);
            best.CloneGenesTo(child);
            _storage.Dump(bodies);
        }
    }
}