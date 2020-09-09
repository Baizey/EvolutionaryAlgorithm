using System.Threading.Tasks;

namespace EvolutionaryAlgorithm.Core
{
    public interface IEvolutionaryAlgorithm<T>
    {
        Task Evolve();
    }
}