using System;
using System.Collections;
using EvolutionaryAlgorithm.BitImplementation;
using EvolutionaryAlgorithm.Core.Statistics;
using EvolutionaryAlgorithm.Core.Terminations;
using EvolutionaryAlgorithm.GUI.Controllers.Services;
using EvolutionaryAlgorithm.GUI.Services.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EvolutionaryAlgorithm.GUI.Controllers
{
    [ApiController]
    public class EvolutionaryAlgorithmController : Controller
    {
        private readonly IEvolutionaryAlgorithmService _service;

        public EvolutionaryAlgorithmController(IEvolutionaryAlgorithmService service) => _service = service;

        [HttpGet("IsRunning")]
        public bool IsRunning() => _service.IsRunning;

        [HttpGet("Statistics")]
        public IUiEvolutionaryStatistics<IEndogenousBitIndividual, BitArray, bool> GetStatistics() =>
            _service.Statistics;

        [HttpPost("Create")]
        public void Run()
        {
            const int n = 5000;

            _service.Initialize(
                FitnessFunctions.OneMax,
                Heuristics.SingleEndogenous,
                geneCount: n,
                mutationRate: 2,
                mu: 1,
                lambda: (int) (3 * Math.Log(n)),
                datapoints: 5000,
                learningRate: 2);
            _service.Run(new FitnessTermination<IEndogenousBitIndividual, BitArray, bool>(n));
        }

        [HttpDelete("/Terminate")]
        public void Terminate() => _service.Terminate();
    }
}