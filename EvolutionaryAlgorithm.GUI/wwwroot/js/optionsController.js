﻿class OptionsController {
    constructor() {
        this.termination = new Input('termination');
        this.seconds = new Input('seconds');
        this.fitnessLimit = new Input('fitnessLimit');
        this.generations = new Input('generations');

        this.variables = new Input('variables');
        this.formulas = new Input('formulas');
        this.formulaSize = new Input('formula_size');

        this.limitFactor = new Input('limitFactor');

        this.jump = new Input('jump');
        this.nodes = new Input('nodes');
        this.edgeChance = new Input('edgeChance');
        this.geneCount = new Input('geneCount');
        this.lambda = new Input('lambda');
        this.mu = new Input('mu');
        this.mutationRate = new Input('mutationRate');
        this.beta = new Input('beta');
        this.learningRate = new Input('learningRate');
        this.observationPhase = new Input('observationPhase');
        this.repairChance = new Input('repairChance');
        this.rows = [
            this.limitFactor,
            this.variables,
            this.formulas,
            this.formulaSize,
            this.nodes,
            this.edgeChance,
            this.seconds,
            this.fitnessLimit,
            this.generations,
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
        this.terminationInput = document.getElementById('input_termination');
        this.heuristicInput = document.getElementById('input_heuristic');
        this.fitnessInput = document.getElementById('input_fitness');
        const self = this;
        this.terminationInput.onchange = () => {
            self.updateOptions()
        }
        this.heuristicInput.onchange = () => {
            self.updateOptions()
        }
        this.fitnessInput.onchange = () => {
            self.updateOptions()
        }

        this.updateOptions();
        this.api = new Api();
    }

    async run() {
        const body = {
            termination: this.terminationInput.value
        }
        switch (this.terminationInput.value) {
            case 'Fitness':
                switch (this.fitnessInput.value) {
                    case 'Satisfiability':
                        body.fitness = Math.floor(this.formulas.value - 0);
                        break;
                    case 'MinimumSpanningTree':
                        body.fitness = this.fitnessLimit.value - 0;
                        break;
                }
                break;
            case 'Time':
                body.seconds = Math.floor(this.fitnessLimit.value - 0);
                break;
            case 'Stagnation':
            case 'Generations':
                body.generations = Math.floor(this.fitnessLimit.value - 0);
                break;
        }
        await this.api.run(body);
    }

    async initialize() {
        const fitness = this.fitnessInput.value;
        const heuristic = this.heuristicInput.value;
        const body = {
            heuristic: heuristic,
            fitness: fitness
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
            case 'MinimumSpanningTree':
                body.nodes = Math.floor(this.nodes.value - 0);
                body.edgeChance = this.edgeChance.value - 0;
                break;
            case 'Satisfiability':
                body.geneCount = Math.floor(this.variables.value - 0);
                body.variables = Math.floor(this.variables.value - 0);
                body.formulas = Math.floor(this.formulas.value - 0);
                body.formulaSize = Math.floor(this.formulaSize.value - 0);
                break;
        }

        switch (heuristic) {
            case 'Asymmetric':
                body.mu = 1;
                body.lambda = 1;
                body.learningRate = this.learningRate.value - 0;
                body.mutationRate = Math.floor(this.mutationRate.value - 0);
                body.observationPhase = Math.floor(this.observationPhase.value - 0);
                break;
            case 'StagnationDetection':
                body.mu = 1;
                body.limitFactor = Math.floor(this.limitFactor.value - 0);
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

        await this.api.initialize(body);
    }

    updateOptions() {
        this.rows.forEach(r => r.hide());

        // Almost everything uses this
        if (this.learningRate.value <= 1) this.learningRate.set(2);
        this.learningRate.show();
        this.lambda.show();
        this.mutationRate.show();

        switch (this.terminationInput.value) {
            case 'Fitness':
                switch (this.fitnessInput.value) {
                    case 'MinimumSpanningTree':
                        this.fitnessLimit.show();
                        break;
                }
                break;
            case 'Time':
                this.seconds.show();
                break;
            case 'Stagnation':
            case 'Generations':
                this.generations.show();
                break;
        }
        switch (this.fitnessInput.value) {
            case 'OneMax':
            case 'LeadingOnes':
                this.geneCount.show();
                break;
            case 'Jump':
                this.geneCount.show();
                this.jump.show();
                break;
            case 'MinimumSpanningTree':
                this.nodes.show();
                this.edgeChance.show();
                break;
            case 'Satisfiability':
                this.variables.show();
                this.formulas.show();
                this.formulaSize.show();
                break;
        }
        switch (this.heuristicInput.value) {
            case 'Asymmetric':
                this.lambda.hide();
                this.observationPhase.show();
                if (this.learningRate.value >= 1) this.learningRate.set(0.05);
                break;
            case 'StagnationDetection':
                this.limitFactor.show();
                break;
            case 'Repair':
                this.repairChance.show();
                break;
            case 'SingleEndogenous':
                break;
            case 'MultiEndogenous':
                break;
            case 'HeavyTail':
                this.beta.show();
                break;
        }
    }
}