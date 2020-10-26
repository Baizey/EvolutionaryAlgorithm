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

class Api {
    constructor() {
    }

    /**
     * @param {string} path
     * @param {string} method
     * @param {object} body
     * @returns {Promise<any>}
     * @private
     */
    async _call(path, method = 'GET', body = {}) {
        const options = {
            method: method,
            headers: {'Content-Type': 'application/json'},
        }
        if (method === 'POST') options.body = body;
        else path += `?${Object.keys(body).map(key => `${key}=${body[key]}`).join('&')}`;
        const response = await fetch(`https://localhost:5001/${path}`, options);
        const result = await response.text();
        try {
            return JSON.parse(result);
        } catch (e) {
            return null;
        }
    }

    /**
     * @returns {Promise<boolean>}
     */
    isRunning() {
        return this._call('IsRunning');
    }

    /**
     * @returns {Promise<Statistics>}
     */
    getStatistics(includeHistory = true) {
        return this._call('Statistics', 'GET', {includeHistory: includeHistory});
    }

    /**
     * @returns Promise<void>
     */
    create() {
        return this._call('Create', 'POST');
    }

    /**
     * @returns Promise<void>
     */
    terminate() {
        return this._call('Terminate', 'DELETE');
    }

    async display() {
        console.log("Display");
        const statistics = await api.getStatistics();
        const mutationRateData = [];
        for (let i = 0; i < statistics.history.length; i++)
            mutationRateData.push({x: i * statistics.stepSize, y: statistics.parameters[i].mutationRate});
        const fitnessData = [];
        for (let i = 0; i < statistics.history.length; i++)
            fitnessData.push({x: i * statistics.stepSize, y: statistics.history[i].fitness});

        const chart = new CanvasJS.Chart("chartContainer", {
            animationEnabled: false,
            theme: "light2",
            title: {
                text: "Generation statistics"
            },
            axisX: {
                title: 'Generation'
            },
            axisY: {
                title: 'Self adjusting parameter'
            },
            axisY2: {
                title: 'Fitness'
            },
            data: [{
                name: 'Mutation rate',
                type: "line",
                yValueFormatString: "#### mutation rate",
                xValueFormatString: "Generation ########",
                showInLegend: true,
                indexLabelFontSize: 12,
                dataPoints: mutationRateData
            }, {
                name: 'Fitness',
                type: "line",
                showInLegend: true,
                yValueFormatString: "#### fitness",
                xValueFormatString: "Generation ########",
                axisYType: 'secondary',
                indexLabelFontSize: 12,
                dataPoints: fitnessData
            }]
        });
        chart.render();
    }
}

const api = new Api();