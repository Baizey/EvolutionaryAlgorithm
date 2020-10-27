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

class Input {
    constructor(id) {
        this._row = document.getElementById('row_' + id);
        this._input = document.getElementById('input_' + id);
    }

    show() {
        this._row.classList.remove('hidden');
    }

    hide() {
        this._row.classList.add('hidden');
    }

    set(value) {
        this._input.value = value;
    }

    get value() {
        return this._input.value;
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
        if (method === 'POST' || method === 'PUT') options.body = JSON.stringify(body);
        else path += `?${Object.keys(body).map(key => `data.${key}=${body[key]}`).join('&')}`;
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
     * @param {Object} body
     * @returns Promise<void>
     */
    initialize(body = {}) {
        return this._call('Initialize', 'POST', body);
    }

    /**
     * @param body
     * @returns Promise<void>
     */
    run() {
        return this._call('Run', 'PUT');
    }

    /**
     * @returns Promise<void>
     */
    pause() {
        return this._call('Pause', 'DELETE');
    }

    async display() {
        const statistics = await api.getStatistics();
        const mutationRateData = [];
        for (let i = 0; i < statistics.history.length; i++)
            mutationRateData.push({x: i * statistics.stepSize, y: statistics.parameters[i].mutationRate});
        const fitnessData = [];
        for (let i = 0; i < statistics.history.length; i++)
            fitnessData.push({x: i * statistics.stepSize, y: statistics.history[i].fitness});
        const lambdaData = [];
        for (let i = 0; i < statistics.history.length; i++)
            lambdaData.push({x: i * statistics.stepSize, y: statistics.parameters[i].lambda});

        new CanvasJS.Chart("secondaryGraph", {
            animationEnabled: false,
            theme: "light2",
            title: {text: "Mutation rate"},
            axisX: {title: 'Generation'},
            axisY: {title: 'Fitness'},
            axisY2: {title: 'Mutation rate'},
            data: [{
                name: 'Fitness',
                type: "line",
                showInLegend: true,
                yValueFormatString: "#### fitness",
                xValueFormatString: "Generation ########",
                axisYType: 'secondary',
                indexLabelFontSize: 12,
                dataPoints: fitnessData
            }, {
                name: 'Mutation rate',
                type: "line",
                yValueFormatString: "#### mutation rate",
                xValueFormatString: "Generation ########",
                showInLegend: true,
                indexLabelFontSize: 12,
                dataPoints: mutationRateData
            }]
        }).render();
        new CanvasJS.Chart("ternaryGraph", {
            animationEnabled: false,
            theme: "light2",
            title: {text: "Lambda"},
            axisX: {title: 'Generation'},
            axisY: {title: 'Fitness'},
            axisY2: {title: 'Lambda'},
            data: [{
                name: 'Fitness',
                type: "line",
                showInLegend: true,
                yValueFormatString: "#### fitness",
                xValueFormatString: "Generation ########",
                axisYType: 'secondary',
                indexLabelFontSize: 12,
                dataPoints: fitnessData
            }, {
                name: 'Lambda',
                type: "line",
                showInLegend: true,
                yValueFormatString: "#### lambda",
                xValueFormatString: "Generation ########",
                indexLabelFontSize: 12,
                dataPoints: lambdaData
            }]
        }).render();
    }
}

const api = new Api();

class OptionsController {
    constructor() {
        this.datapoints = new Input('datapoints');
        this.jump = new Input('jump');
        this.geneCount = new Input('geneCount');
        this.lambda = new Input('lambda');
        this.mu = new Input('mu');
        this.mutationRate = new Input('mutationRate');
        this.beta = new Input('beta');
        this.learningRate = new Input('learningRate');
        this.observationPhase = new Input('observationPhase');
        this.repairChance = new Input('repairChance');
        this.rows = [
            this.jump,
            this.geneCount,
            this.lambda,
            this.mu,
            this.mutationRate,
            this.beta,
            this.learningRate,
            this.observationPhase,
            this.repairChance,
        ];
        this.heuristicInput = document.getElementById('input_heuristic');
        this.fitnessInput = document.getElementById('input_fitness');
        const self = this;
        this.heuristicInput.onchange = () => {
            self.updateOptions()
        }
        this.fitnessInput.onchange = () => {
            self.updateOptions()
        }

        this.updateOptions();
        this.api = new Api();
    }

    initialize() {
        const fitness = this.fitnessInput.value;
        const heuristic = this.heuristicInput.value;
        const body = {
            heuristic: heuristic,
            fitness: fitness,
            datapoints: Math.floor(this.datapoints.value - 0),
        };
        switch (fitness) {
            case 'OneMax':
                body.geneCount = Math.floor(this.geneCount.value - 0);
                break;
            case 'LeadingOnes':
                body.geneCount = Math.floor(this.geneCount.value - 0);
                break;
            case 'Jump':
                body.geneCount = Math.floor(this.geneCount.value - 0);
                body.jump = Math.floor(this.jump.value - 0);
                break;
        }
        switch (heuristic) {
            case 'Asymmetric':
                body.mu = 1;
                body.lambda = 1;
                body.learningRate = this.learningRate.value - 0;
                body.observationPhase = Math.floor(this.observationPhase.value - 0);
                break;
            case 'StagnationDetection':
                body.mu = 1;
                body.lambda = Math.floor(this.lambda.value - 0);
                body.mutationRate = Math.floor(this.mutationRate.value - 0);
                break;
            case 'Repair':
                body.mu = 1;
                body.lambda = Math.floor(this.lambda.value - 0);
                body.learningRate = this.learningRate.value - 0;
                body.repairChance = this.repairChance.value - 0;
                break;
            case 'SingleEndogenous':
                body.mu = 1;
                body.lambda = Math.floor(this.lambda.value - 0);
                body.learningRate = this.learningRate.value - 0;
                break;
            case 'MultiEndogenous':
                body.mu = Math.floor(this.lambda.value - 0);
                body.lambda = Math.floor(this.lambda.value - 0);
                body.learningRate = this.learningRate.value - 0;
                break;
            case 'HeavyTail':
                body.mu = 1;
                body.lambda = Math.floor(this.lambda.value - 0);
                body.beta = this.beta.value - 0;
                break;
        }

        this.api.initialize(body).finally();
    }

    updateOptions() {
        this.rows.forEach(r => r.hide());
        this.geneCount.show();
        switch (this.fitnessInput.value) {
            case 'OneMax':
                this.geneCount.show();
                break;
            case 'LeadingOnes':
                this.geneCount.show();
                break;
            case 'Jump':
                this.geneCount.show();
                this.jump.show();
                break;
        }
        switch (this.heuristicInput.value) {
            case 'Asymmetric':
                this.observationPhase.show();
                this.learningRate.show();
                break;
            case 'StagnationDetection':
                this.lambda.show();
                this.mutationRate.show();
                break;
            case 'Repair':
                this.lambda.show();
                this.learningRate.show();
                this.repairChance.show();
                break;
            case 'SingleEndogenous':
                this.lambda.show();
                this.learningRate.show();
                break;
            case 'MultiEndogenous':
                this.lambda.show();
                this.learningRate.show();
                break;
            case 'HeavyTail':
                this.lambda.show();
                this.beta.show();
                break;
        }
    }
}

const optionsController = new OptionsController();

async function updateView() {
    const running = await api.isRunning();
    await api.display();
    if (running) await updateView();
}

document.getElementById('button_initialize').onclick = () => {
    optionsController.initialize();
}
document.getElementById('button_run').onclick = () => {
    api.run().then(() => updateView());
}
document.getElementById('button_pause').onclick = () => {
    api.pause();
}