using System.Threading.Tasks;

namespace GeneticAlgorithm.Interfaces
{
    public interface IEvolutionaryAlgorithm
    {
        Task Evolve();
    }
}