using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Terminations;
using EvolutionaryAlgorithm.GUI.Controllers.Services;
using EvolutionaryAlgorithm.GUI.Controllers.Services.Enums;
using EvolutionaryAlgorithm.GUI.Controllers.Services.Extensions;
using EvolutionaryAlgorithm.GUI.Models;
using EvolutionaryAlgorithm.GUI.Services.Enums;
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
        public bool IsRunning() => _service.IsRunning;

        [HttpGet("Statistics")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public StatisticsView GetStatistics(bool includeHistory) =>
            _service.Statistics?.MapToView(includeHistory);

        [HttpPost("Create")]
        [ProducesResponseType(204)]
        public void Run()
        {
            const int n = 5000;

            _service.Initialize(
                FitnessFunctions.OneMax,
                Heuristics.Asymmetric,
                mutationRate: 2,
                geneCount: n,
                mu: 1,
                lambda: (int) (3 * Math.Log(n)),
                datapoints: 50000,
                beta: 1.5,
                learningRate: 2);
            _service.Run(new FitnessTermination<IEndogenousBitIndividual, BitArray, bool>(n));
        }

        [HttpDelete("/Terminate")]
        [ProducesResponseType(204)]
        public void Terminate() => _service.Terminate();
    }
}