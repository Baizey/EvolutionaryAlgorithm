class CheapIndividual {
    /**
     * @param {{
     *     fitness: number,
     * }} data
     */
    constructor(data) {
        this.fitness = data.fitness;
    }
}

class Individual extends CheapIndividual {
    /**
     * @param {{
     *     mutationRate: number,
     *     fitness: number,
     *     genes: boolean[]
     * }} data
     */
    constructor(data) {
        super(data);
        this.genes = data.genes;
    }
}