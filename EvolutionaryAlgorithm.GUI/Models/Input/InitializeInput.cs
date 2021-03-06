﻿using EvolutionaryAlgorithm.GUI.Models.Enums;

namespace EvolutionaryAlgorithm.GUI.Models.Input
{
    public class InitializeInput
    {
        public string Fitness { get; set; }
        public string Heuristic { get; set; }
        public int GeneCount { get; set; }
        public int Lambda { get; set; }
        public int Mu { get; set; }
        public double? Beta { get; set; }
        public int? MutationRate { get; set; }
        public double? LearningRate { get; set; }
        public int? Jump { get; set; }
        public int? ObservationPhase { get; set; }
        public double? RepairChance { get; set; }
        public int? Nodes { get; set; }
        public double? EdgeChance { get; set; }
        public int? Variables { get; set; }
        public int? Formulas { get; set; }
        public int? FormulaSize { get; set; }
        public int? LimitFactor { get; set; }
    }
}