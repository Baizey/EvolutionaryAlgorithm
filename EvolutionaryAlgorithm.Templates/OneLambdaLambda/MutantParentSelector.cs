using System;
using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;
using EvolutionaryAlgorithm.Core.Abstract.MutationPhase.Helpers;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.OneLambdaLambda
{
    public class MutantParentSelector : IBitSingleParentSelector
    {
        private IIndividualStorage<IBitIndividual, BitArray, bool> _storage;
        private readonly ISingleParentSelector<IBitIndividual, BitArray, bool> _select;
        private readonly int _lambdaM;

        public MutantParentSelector(
            ISingleParentSelector<IBitIndividual, BitArray, bool> initialSelector,
            int lambdaM)
        {
            _lambdaM = lambdaM;
            _select = initialSelector;
        }

        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
            _storage = new IndividualStorage<IBitIndividual, BitArray, bool>(Algorithm);
            _select.Algorithm = Algorithm;
            _select.Initialize();
        }

        public void Update() => _select.Update();

        public IBitIndividual Select(int index, IPopulation<IBitIndividual, BitArray, bool> population)
        {
            var initial = _select.Select(index, Algorithm.Population);
            var bodies = _storage.Get(index, _lambdaM);

            foreach (var body in bodies)
            {
                initial.CloneGenesTo(body);
                // TODO: implement mutation based on parent
                throw new NotImplementedException();
            }

            bodies.ForEach(b => b.Fitness = Algorithm.Fitness.Evaluate(b));
            _storage.Dump(index, bodies);
            return bodies.Aggregate((a, b) => a.Fitness > b.Fitness ? a : b);
        }
    }
}