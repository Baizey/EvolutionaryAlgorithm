using System;
using System.Collections;
using System.Linq;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Parameters;
using EvolutionaryAlgorithm.Core.Statistics;

namespace EvolutionaryAlgorithm.GUI.Models
{
    public class StatisticsView
    {
        public IndividualView[] History { get; set; }
        public IParameters[] Parameters { get; set; }
        public int StepSize { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IndividualView Best { get; set; }
        public IndividualView Current { get; set; }
        public IndividualView Previous { get; set; }
        public long StagnantGeneration { get; set; }
        public long Generations { get; set; }

        public StatisticsView(IUiEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool> statistics,
            bool includeHistory)
        {
            StartTime = statistics.StartTime;
            EndTime = statistics.EndTime ?? DateTime.Now;
            Best = statistics.Best == null ? null : new IndividualView(statistics.Best);
            Current = statistics.Best == null ? null : new IndividualView(statistics.Current);
            Previous = statistics.Best == null ? null : new IndividualView(statistics.Previous);
            StagnantGeneration = statistics.StagnantGeneration;
            Generations = statistics.Generations;
            if (includeHistory)
            {
                StepSize = statistics.GeneHistory.StepSize;
                History = statistics.GeneHistory?.Items.Select(e => new IndividualView(e)).ToArray();
                Parameters = statistics.ParameterHistory?.Items;
            }
        }
    }
}