using System;
using System.Collections;
using System.Collections.Generic;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Algorithm;
using EvolutionaryAlgorithm.Core.Infrastructure;
using EvolutionaryAlgorithm.Core.Parameters;

namespace EvolutionaryAlgorithm.Template.Mutations
{
    public class MutateAndRepairMutation : IBitMutation<IBitIndividual>
    {
        private readonly Random _random = new Random();
        private IndividualStorage<IBitIndividual, bool[], bool> _storage;
        private readonly MutationApplier _applier = new MutationApplier();
        private IBitIndividual _xMark;
        private IParameters _parameters;
        private int[] _flips;
        private readonly double _repairChance;
        public IEvolutionaryAlgorithm<IBitIndividual, bool[], bool> Algorithm { get; set; }

        public MutateAndRepairMutation(double repairChance)
        {
            _repairChance = repairChance;
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

            var mutations = Math.Min(Algorithm.Parameters.GeneCount, Mutations());
            _flips = new int[mutations];
            var currentFlips = new int[mutations];
            var flipped = new HashSet<int>();
            for (var i = 0; i < bodies.Count; i++)
            {
                flipped.Clear();
                var body = bodies[i];
                x.CopyTo(body);
                for (var m = 0; m < mutations; m++)
                {
                    int r;
                    do r = _random.Next(_parameters.GeneCount);
                    while (flipped.Contains(r));
                    flipped.Add(r);
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
            _storage = new IndividualStorage<IBitIndividual, bool[], bool>(Algorithm);
            _xMark = _storage.Get(-1, 1)[0];
            _flips = new int[0];
            MutationPhase();
        }

        public void Update() => MutationPhase();

        public void Mutate(int index, IBitIndividual y) =>
            _applier.MutatePart(y, _repairChance, _flips);
    }
}