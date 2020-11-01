class OptionsController {
    constructor() {
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
                this.mutationRate.show();
                this.observationPhase.show();
                
                if (this.learningRate.value >= 1) this.learningRate.set(0.05);
                this.learningRate.show();
                break;
            case 'StagnationDetection':
                this.lambda.show();
                this.mutationRate.show();
                break;
            case 'Repair':
                this.lambda.show();
                this.mutationRate.show();
                this.repairChance.show();
                
                if (this.learningRate.value <= 1) this.learningRate.set(2);
                this.learningRate.show();
                break;
            case 'SingleEndogenous':
                this.lambda.show();
                this.mutationRate.show();
                
                if (this.learningRate.value <= 1) this.learningRate.set(2);
                this.learningRate.show();
                break;
            case 'MultiEndogenous':
                this.lambda.show();
                this.mutationRate.show();
                
                if (this.learningRate.value <= 1) this.learningRate.set(2);
                this.learningRate.show();
                break;
            case 'HeavyTail':
                this.mutationRate.show();
                this.lambda.show();
                this.beta.show();
                break;
        }
    }
}