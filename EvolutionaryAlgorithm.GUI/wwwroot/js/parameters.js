class Parameters {
    /**
     * @param {{
     *     geneCount: number,
     *     mutationRate: number,
     *     mu: number,
     *     lambda: number
     * }} data
     */
    constructor(data) {
        this.geneCount = data.geneCount;
        this.lambda = data.lambda;
        this.mu = data.mu;
        this.mutationRate = data.mutationRate;
    }
}