using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EvolutionaryAlgorithm.GUI.Models;
using EvolutionaryAlgorithm.GUI.Models.Enums;
using EvolutionaryAlgorithm.GUI.Models.Input;
using EvolutionaryAlgorithm.GUI.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace EvolutionaryAlgorithm.GUI.Controllers
{
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    public class EvolutionaryAlgorithmController : Controller
    {
        private readonly IEvolutionaryAlgorithmService _service;

        public EvolutionaryAlgorithmController(IEvolutionaryAlgorithmService service) => _service = service;

        [HttpGet("IsRunning")]
        public bool IsRunning() => _service.IsRunning;

        [HttpGet("Nodes")]
        public IEnumerable<ViewNode> GetNodes() => _service.Nodes?
            .Select(e => new ViewNode(e))
            .ToList();

        [HttpGet("Edges")]
        public IEnumerable<ViewEdge> GetEdges() => _service.Edges?
            .Select(e => new ViewEdge(e))
            .ToList();

        [HttpGet("Statistics")]
        public StatisticsView GetStatistics() =>
            _service.Statistics;

        [HttpPost("Initialize")]
        public void Initialize([FromBody] InitializeInput data)
        {
            if (!Enum.TryParse(data.Fitness, out FitnessFunctions fitness))
                throw new InvalidDataException($"{nameof(data.Fitness)} ({data.Fitness}) is not valid");
            if (!Enum.TryParse(data.Heuristic, out Heuristics heuristic))
                throw new InvalidDataException($"{nameof(data.Heuristic)} ({data.Heuristic}) is not valid");

            _service.Initialize(
                fitness,
                heuristic,
                mu: data.Mu,
                geneCount: data.GeneCount,
                lambda: data.Lambda,
                mutationRate: data.MutationRate ?? 1,
                learningRate: data.LearningRate ?? 2,
                observationPhase: data.ObservationPhase ?? 10,
                repairChance: data.RepairChance ?? 1,
                beta: data.Beta ?? 1.5D,
                jump: data.Jump ?? 5,
                nodes: data.Nodes ?? 20,
                edgeChance: data.EdgeChance ?? 0.5,
                formulas: data.Formulas ?? 20,
                variables: data.Variables ?? 60,
                formulaSize: data.FormulaSize ?? 3,
                limitFactor: data.LimitFactor ?? 1
            );
        }

        [HttpPut("Run")]
        public bool Run([FromBody] RunInput data)
        {
            if (!Enum.TryParse(data.Termination, out Termination termination))
                throw new InvalidDataException($"{nameof(data.Termination)} ({data.Termination}) is not valid");

            return _service.Run(
                termination,
                fitness: data.Fitness ?? _service.Algorithm.Parameters.GeneCount,
                seconds: data.Seconds ?? 10,
                generations: data.Generations ?? 1000
            );
        }

        [HttpDelete("Pause")]
        public void Pause() => _service.Pause();
    }
}