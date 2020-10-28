using System;
using System.Collections;
using System.IO;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Terminations;
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
    public class EvolutionaryAlgorithmController : Controller
    {
        private readonly IEvolutionaryAlgorithmService _service;

        public EvolutionaryAlgorithmController(IEvolutionaryAlgorithmService service) => _service = service;

        [HttpGet("IsRunning")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public bool IsRunning() => _service.IsRunning;

        [HttpGet("Statistics")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public StatisticsView GetStatistics() =>
            _service.Statistics;

        [HttpPost("Initialize")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
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
                jump: data.Jump ?? 5
            );
        }

        [HttpPut("Run")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public bool Run() => _service.Run(
            new FitnessTermination<IEndogenousBitIndividual, BitArray, bool>(
                _service.Algorithm.Parameters.GeneCount));

        [HttpDelete("Pause")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public void Pause() => _service.Pause();
    }
}