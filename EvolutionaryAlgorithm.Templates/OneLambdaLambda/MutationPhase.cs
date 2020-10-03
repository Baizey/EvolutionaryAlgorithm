using System;
using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.Bit.Abstract;
using EvolutionaryAlgorithm.Bit.Algorithm;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;
using EvolutionaryAlgorithm.Core.Abstract.Infrastructure;
using EvolutionaryAlgorithm.Core.Algorithm;

namespace EvolutionaryAlgorithm.Template.OneLambdaLambda
{
    public class MutationPhase : IBitSingleParentSelector<IBitIndividual>
    {
        private readonly Random _random = new Random();
        private IIndividualStorage<IBitIndividual, BitArray, bool> _storage;
        private IParameters _parameters;
        private IBitIndividual _original;
        private double[] _odds;
        private IBitIndividual _x;

        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
            _parameters = Algorithm.Parameters;
            _storage = new IndividualStorage<IBitIndividual, BitArray, bool>(Algorithm);
            _odds = new MutationApplier().GetOdds(_parameters.MutationRate, _parameters.GeneCount);
            Update();
        }

        public void Update()
        {
            _original = Algorithm.Population[0];
            var bodies = _storage.Get(0, _parameters.Lambda);
            _storage.Dump(0, bodies);

            var mutations = Mutations();
            foreach (var body in bodies)
            {
                _original.CloneGenesTo(body);
                for (var i = 0; i < mutations; i++)
                    body.Flip(_random.Next(_parameters.GeneCount));
            }

            bodies.ForEach(b => b.Fitness = Algorithm.Fitness.Evaluate(b));
            _x = bodies.Aggregate((a, b) => a.Fitness > b.Fitness ? a : b);
        }

        private int Mutations()
        {
            var roll = _random.NextDouble();
            var mutations = 0;
            for (; mutations < _odds.Length && roll >= _odds[mutations]; mutations++)
                roll -= _odds[mutations];
            return mutations;
        }

        public IBitIndividual Select(int index) => _x;
    }
}