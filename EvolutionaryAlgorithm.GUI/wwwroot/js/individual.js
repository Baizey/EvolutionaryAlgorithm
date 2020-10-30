class CheapIndividual {
    /**
     * @param {{
     *     fitness: number,
     * }} data
     */
    constructor(data) {
        data ||= {};
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
        data ||= {};
        this.genes = data.genes || [];
        this._ones = -1;
    }

    get ones() {
        if (this._ones >= 0) return this._ones;
        return this._ones = this.genes.filter(e => e).length;
    }

    get zeroes() {
        return this.genes.length - this.ones;
    }
}