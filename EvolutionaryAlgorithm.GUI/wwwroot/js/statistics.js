class Statistics {
    /**
     * @param {{
     *     history: Individual[],
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
        this.individuals = data.history;
        this.parameters = data.parameters;
        this.generations = data.generations;
        this.stagnantGenerations = data.stagnantGenerations;
        this.current = data.current;
        this.previous = data.previous;
        this.best = data.best;
        this.startTime = data.startTime;
        this.endTime = data.endTime;
    }
}