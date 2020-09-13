namespace EvolutionaryAlgorithm.Core.Abstract
{
    public interface IMutation<TDataStructure, TGene>
    {
        IIndividual<TDataStructure, TGene> Mutate(IIndividual<TDataStructure, TGene> child,
            IIndividual<TDataStructure, TGene> parent);
    }
}