class Statistics {
    /**
     * @param {{
     *     history: CheapIndividual[],
     *     parameters: Parameters,
     *     generations: number,
     *     stagnantGenerations: number,
     *     current: Individual,
     *     previous: Individual,
     *     best: Individual,
     *     startTime: Date,
     *     endTime: Date | null,
     *     r0: number,
     *     r1: number,
     *     inStagnation: boolean,
     *     stagnationProgress: number
     * }} data
     */
    constructor(data) {
        data ||= {};
        this.parameters = data.parameters;
        this.individuals = data.history;
        this.generations = data.generations;
        this.stagnantGenerations = data.stagnantGenerations;
        this.current = new Individual(data.current);
        this.previous = new Individual(data.previous);
        this.best = new Individual(data.best);
        this.startTime = data.startTime;
        this.endTime = data.endTime;
        this.r0 = data.r0;
        this.r1 = data.r1;
        this.inStagnation = data.inStagnation;
        this.stagnationProgress = data.stagnationProgress;
    }
}