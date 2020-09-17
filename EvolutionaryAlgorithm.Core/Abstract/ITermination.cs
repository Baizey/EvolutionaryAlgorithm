using System;

namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface ITermination<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        public bool IsDone(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algorithm);
    }

    public class FitnessTermination<TIndividual, TGeneStructure, TGene>
        : ITermination<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        private readonly Func<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>, double> _limit;

        public FitnessTermination(double limit) => _limit = _ => limit;

        public FitnessTermination(Func<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>, double> limit) =>
            _limit = limit;

        public bool IsDone(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algorithm) =>
            algorithm.Best.Fitness >= _limit.Invoke(algorithm);
    }

    public class StagnationTermination<TIndividual, TGeneStructure, TGene>
        : ITermination<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        private readonly Func<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>, double> _limit;

        public StagnationTermination(double limit) => _limit = _ => limit;

        public StagnationTermination(Func<IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene>, double> limit) =>
            _limit = limit;

        public bool IsDone(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algorithm) =>
            algorithm.Statistics.StagnantGeneration >= _limit.Invoke(algorithm);
    }

    public class TimeoutTermination<TIndividual, TGeneStructure, TGene>
        : ITermination<TIndividual, TGeneStructure, TGene>
        where TGeneStructure : ICloneable
        where TIndividual : IIndividual<TGeneStructure, TGene>
    {
        private readonly TimeSpan _limit;

        public TimeoutTermination(TimeSpan limit) => _limit = limit;

        public bool IsDone(IEvolutionaryAlgorithm<TIndividual, TGeneStructure, TGene> algorithm) =>
            algorithm.Statistics.RunTime >= _limit;
    }
}