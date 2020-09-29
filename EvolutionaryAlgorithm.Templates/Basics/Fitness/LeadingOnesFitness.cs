using System.Collections;
using EvolutionaryAlgorithm.BitImplementation.Abstract;
using EvolutionaryAlgorithm.Core.Abstract.Core;

namespace EvolutionaryAlgorithm.Template.Basics.Fitness
{
    public class LeadingOnesFitness : IBitFitness
    {
        public IEvolutionaryAlgorithm<IBitIndividual, BitArray, bool> Algorithm { get; set; }

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        public double Evaluate(IBitIndividual individual)
        {
            var i = 0;
            for (; i < individual.Size; i++)
                if (!individual[i])
                    break;
            return i;
        }
    }
}