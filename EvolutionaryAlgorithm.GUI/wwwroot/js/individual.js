class Individual {
    /**
     * @param {{
     *     mutationRate: number,
     *     fitness: number,
     *     genes: boolean[]
     * }} data
     */
    constructor(data) {
        this.mutationRate = data.mutationRate;
        this.fitness = data.fitness;
        this.genes = data.genes;
    }
}