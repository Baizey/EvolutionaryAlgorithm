class Statistics {
    /**
     * @param {{
     *     history: CheapIndividual[],
     *     parameters: Parameters[],
     *     generations: number,
     *     stagnantGenerations: number,
     *     current: Individual,
     *     previous: Individual,
     *     best: Individual,
     *     startTime: Date
     *     endTime: Date | null
     * }} data
     */
    constructor(data) {
        data ||= {};
        this.history = data.history;
        this.parameters = data.parameters;
        this.individuals = data.history;
        this.parameters = data.parameters;
        this.generations = data.generations;
        this.stagnantGenerations = data.stagnantGenerations;
        this.current = new Individual(data.current);
        this.previous = new Individual(data.previous);
        this.best = new Individual(data.best);
        this.startTime = data.startTime;
        this.endTime = data.endTime;
    }
}