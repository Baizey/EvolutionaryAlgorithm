using System.Collections;
using System.Threading.Tasks;
using EvolutionaryAlgorithm.Core.Abstract;
using EvolutionaryAlgorithm.Core.Bit;

namespace EvolutionaryAlgorithm.Template.Algorithm
{
    public class BitEvolutionaryAlgorithm : IBitEvolutionaryAlgorithm
    {
        public IPopulation<BitArray, bool> Population { get; set; }
        public IFitness<BitArray, bool> Fitness { get; set; }
        public IMutator<BitArray, bool> Mutator { get; set; }
        public IGenerationFilter<BitArray, bool> GenerationFilter { get; set; }
        public IIndividual<BitArray, bool> Best => Population.Best;

        public BitEvolutionaryAlgorithm()
        {
        }

        public BitEvolutionaryAlgorithm(
            IPopulation<BitArray, bool> initialPopulation,
            IFitness<BitArray, bool> fitness,
            IMutator<BitArray, bool> mutator,
            IGenerationFilter<BitArray, bool> generationFilter)
        {
            Population = initialPopulation;
            Fitness = fitness;
            Mutator = mutator;
            GenerationFilter = generationFilter;

            foreach (var individual in Population.Individuals)
                Fitness.Evaluate(individual);
        }

        private void EvolveOneGeneration()
        {
            var newGeneration = Mutator.Create(Population, Fitness);
            GenerationFilter.Filter(Population, newGeneration);
            Population.Generation++;
        }

        public async Task Evolve()
        {
            EvolveOneGeneration();
        }
    }
}