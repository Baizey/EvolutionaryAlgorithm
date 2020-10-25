using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Infrastructure;
using EvolutionaryAlgorithm.Core.Parameters;

namespace EvolutionaryAlgorithm.Template.OneLambdaLambda
{
    public class CrossoverPhase : IBitMutation<IEndogenousBitIndividual>
    {
        private readonly Random _random = new Random();
        private IndividualStorage<IEndogenousBitIndividual, BitArray, bool> _storage;
        private readonly MutationApplier _applier = new MutationApplier();
        private IEndogenousBitIndividual _xMark;
        private IParameters _parameters;
        private int[] _flips;
        private readonly double _c;
        public IEvolutionaryAlgorithm<IEndogenousBitIndividual, BitArray, bool> Algorithm { get; set; }

        public CrossoverPhase(double repairChance)
        {
            _c = repairChance;
        }

        private int Mutations()
        {
            var odds = _applier.CalculateOdds(_parameters.MutationRate, _parameters.GeneCount);
            var roll = _random.NextDouble();
            var mutations = 0;
            for (; mutations < odds.Length && roll >= odds[mutations]; mutations++)
                roll -= odds[mutations];
            return mutations;
        }

        private void MutationPhase()
        {
            var x = Algorithm.Population[0];
            var bodies = _storage.Get(-1, _parameters.Lambda);
            _storage.Dump(-1, bodies);

            var mutations = Mutations();
            _flips = new int[mutations];
            var currentFlips = new int[mutations];
            for (var i = 0; i < bodies.Count; i++)
            {
                var body = bodies[i];
                x.CopyTo(body);
                for (var m = 0; m < mutations; m++)
                {
                    var r = _random.Next(_parameters.GeneCount);
                    body.Flip(r);
                    currentFlips[m] = r;
                }

                body.Fitness = Algorithm.Fitness.Evaluate(body);

                if (i == 0 || body.Fitness > _xMark.Fitness)
                {
                    body.CopyTo(_xMark);
                    Array.Copy(currentFlips, 0, _flips, 0, mutations);
                }
            }
        }

        public void Initialize()
        {
            _parameters = Algorithm.Parameters;
            _storage = new IndividualStorage<IEndogenousBitIndividual, BitArray, bool>(Algorithm);
            _xMark = _storage.Get(-1, 1)[0];
            _flips = new int[0];
            MutationPhase();
        }

        public void Update() => MutationPhase();

        public void Mutate(int index, IEndogenousBitIndividual y) => _applier.MutatePart(y, _flips, _c);
    }
}