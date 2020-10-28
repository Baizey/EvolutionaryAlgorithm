using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Statistics;

namespace EvolutionaryAlgorithm.GUI.Models
{
    public class StatisticsView
    {
        public IEnumerable<CheapIndividualView> History { get; set; }
        public IEnumerable<IParameters> Parameters { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IndividualViewView Best { get; set; }
        public IndividualViewView Current { get; set; }
        public IndividualViewView Previous { get; set; }
        public long StagnantGeneration { get; set; }
        public long Generations { get; set; }

        public StatisticsView(IUiEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool> statistics,
            bool includeHistory)
        {
            StartTime = statistics.StartTime;
            EndTime = statistics.EndTime ?? DateTime.Now;
            Best = statistics.Best == null ? null : new IndividualViewView(statistics.Best);
            Current = statistics.Best == null ? null : new IndividualViewView(statistics.Current);
            Previous = statistics.Best == null ? null : new IndividualViewView(statistics.Previous);
            StagnantGeneration = statistics.StagnantGeneration;
            Generations = statistics.Generations;


            if (includeHistory)
            {
                History = statistics.GeneHistory.Select(e => new CheapIndividualView(e));
                Parameters = statistics.ParameterHistory;
                statistics.GeneHistory = new List<IEndogenousBitIndividual>();
                statistics.ParameterHistory = new List<IParameters>();
            }
        }
    }
}