# EvolutionaryAlgorithm
 
# Pre-usage steps:

Install .net core 3.1.X sdk (https://dotnet.microsoft.com/download/dotnet-core/3.1)

Install an IDE such as Jetbrains Rider or Visual Studio, or your prefered IDE as long as it has support for C#

# Accessing GUI for Jetbrains Rider

In the top right corner of the IDE setup a run configuration for EvolutionaryAlgorithm.GUI

with the options set as such (any not mentioned should be left empty)

Project: EvolutionaryAlgorithm.GUI

Target: .NETCoreApp,Version=v3.1

Exe path: <your solution location>/EvolutionaryAlgorithm/EvolutionaryAlgorithm.GUI/bin/Debug/netcoreapp3.1/EvolutionaryAlgorithm.GUI.dll
 
Working directory: <your solution location>/EvolutionaryAlgorithm/EvolutionaryAlgorithm.GUI

You should only need to set the project and the rest will be auto configured.

From here you can apply changes and click ok.

Now you can run the new configuration, it should log that the website is up at localhost:5001

you should however go to localhost:5001/Home in order to use the GUI

# Algorithm framework

An algorithm is initialized like:
    var algorithm = new BitEvolutionaryAlgorithm<IBitIndividual>()
 
 From here you need to provide 4 different things to it, initial parameters
 
## Parameters

You can provide 4 global parameters with the provided Parameters implementation,

the gene count, the initial p in p/n for mutation rates, the value of lambda and mu used in the general structure mu + (lamda, lambda), i.e keep mu individuals per generation, generate lambda new individuals for each generation.

    algorithm.UsingParameters(new Parameters
                {
                    GeneCount = 500,
                    MutationRate = 1,
                    Lambda = 1,
                    Mu = 1,
                });


## Statistics

statistics keeps track of a number of basic things

- Generations generated
- Current generation's best
- Previous generation's best
- All time best individual
- Runtime start (if algorithm has begun running)
- Runtime end (if algorithm has terminated)
- number of stagnant generation in a row (no improvement from Previous to Current after each new generation)

For most cases 

                algorithm.UsingBasicStatistics()
                
is more than enough, but for custom statistics

                algorithm.UsingStatistics(...);
                
 can be utilized
 
## HyperHeuristics

This is where most of the algorithm is located, it's purpose is to act as a state machine between different GenerationGenerators such as if you use StagnationDetection

Most implementations uses SimpleHeuristic which simply acts as a wrapper for a GenerationGenerator

GenerationGenerator's hold 2 things, a Mutator and a GenerationFilter

### Mutator
A mutator is a wrapper which handles applying the mutations for the generator, herein are crossovers and clonings sub-categories of mutation. An example could be

                Mutator = new Mutator<IEndogenousBitIndividual, BitArray, bool>()
                .CloneGenesFrom(new BestParentSelector<IEndogenousBitIndividual>())
                .ThenApply(new EndogenousMutation(learningRate));
     
## GenerationFilter

A GenerationFilter is where the culling of the next generation happens.

It simply filters between the old population and the newly generated individuals and curate who is part of the next generation.

A side note is that it also returns a list of the discarded individuals os that they can be reused for future generating and save memory allocation on the algorithm.

## Fitness function

Lastly a fitness function needs to be provided, it takes an individual and returns a double

                algorithm.UsingEvaluation(new OneMaxFitness<IEndogenousBitIndividual>());
