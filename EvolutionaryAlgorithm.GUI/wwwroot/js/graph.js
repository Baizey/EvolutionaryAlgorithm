class Graph {

    asymmetric() {
        this.formatter = statistics => ({
            primary: {x: statistics.generations, y: statistics.current.fitness},
            secondary: {x: statistics.generations, y: statistics.r0},
            ternary: {x: statistics.generations, y: statistics.r1},
        });
        this.graph = new CanvasJS.Chart(this.id, {
            animationEnabled: false,
            theme: "light2",
            title: {text: 'Mutation focus'},
            axisX: {title: 'Generations'},
            axisY: {title: 'Fitness'},
            axisY2: {title: 'Focus in percent', minimum: 0, maximum: 1},
            data: [{
                name: 'Fitness',
                type: "line",
                showInLegend: true,
                xValueFormatString: "generation 0",
                yValueFormatString: "#####0 fitness",
                indexLabelFontSize: 12,
                dataPoints: [],
            }, {
                name: 'Zeros',
                type: "line",
                axisYType: 'secondary',
                showInLegend: true,
                xValueFormatString: 'generation 0',
                yValueFormatString: '0% on zeroes',
                indexLabelFontSize: 12,
                dataPoints: []
            }, {
                name: 'Ones',
                type: "line",
                axisYType: 'secondary',
                showInLegend: true,
                xValueFormatString: 'generation 0',
                yValueFormatString: '0% on ones',
                indexLabelFontSize: 12,
                dataPoints: []
            }]
        });
        this.update();
    }

    stagnation() {
        this.formatter = statistics => {
            if (statistics.inStagnation)
                return {
                    primary: {x: statistics.generations, y: statistics.current.fitness},
                    secondary: {x: statistics.generations, y: statistics.stagnationProgress}
                }
            else
                return {
                    primary: {x: statistics.generations, y: statistics.current.fitness},
                    ternary: {x: statistics.generations, y: statistics.stagnationProgress}
                }
        };
        this.graph = new CanvasJS.Chart(this.id, {
            animationEnabled: false,
            theme: "light2",
            title: {text: 'Progress towards stagnation'},
            axisX: {title: 'Generations'},
            axisY: {title: 'Fitness'},
            axisY2: {title: 'Progress in percent', minimum: 0, maximum: 1},
            data: [{
                name: 'Fitness',
                type: "line",
                showInLegend: true,
                xValueFormatString: "Generation 0",
                yValueFormatString: "0 fitness",
                indexLabelFontSize: 12,
                dataPoints: [],
            }, {
                name: 'In stagnation',
                type: "scatter",
                axisYType: 'secondary',
                showInLegend: true,
                xValueFormatString: "Generation 0",
                yValueFormatString: "0% progress",
                indexLabelFontSize: 12,
                dataPoints: [],
            }, {
                name: 'Towards stagnation',
                type: "scatter",
                axisYType: 'secondary',
                showInLegend: true,
                xValueFormatString: "Generation 0",
                yValueFormatString: "0% progress",
                indexLabelFontSize: 12,
                dataPoints: [],
            }]
        });
        this.update();
    }

    lambda() {
        this.formatter = statistics => {
            return {
                primary: {x: statistics.generations, y: statistics.current.fitness},
                secondary: {x: statistics.generations, y: statistics.parameters.lambda},
            }
        };
        this.graph = new CanvasJS.Chart(this.id, {
            animationEnabled: false,
            theme: "light2",
            title: {text: 'Lambda evolution'},
            axisX: {title: 'Generations'},
            axisY: {title: 'Fitness'},
            axisY2: {title: 'Lambda'},
            data: [{
                name: 'Fitness',
                type: "line",
                showInLegend: true,
                xValueFormatString: "generation 0",
                yValueFormatString: "0 fitness",
                indexLabelFontSize: 12,
                dataPoints: [],
            }, {
                name: 'Lambda',
                type: "line",
                axisYType: 'secondary',
                showInLegend: true,
                xValueFormatString: 'generation 0',
                yValueFormatString: '0 lambda',
                indexLabelFontSize: 12,
                dataPoints: []
            }]
        });
        this.update();
    }

    mutation() {
        this.formatter = statistics => {
            return {
                primary: {x: statistics.generations, y: statistics.current.fitness},
                secondary: {x: statistics.generations, y: statistics.parameters.mutationRate}
            }
        };
        this.graph = new CanvasJS.Chart(this.id, {
            animationEnabled: false,
            theme: "light2",
            title: {text: 'Mutation rate evolution'},
            axisX: {title: 'Generations'},
            axisY: {title: 'Fitness'},
            axisY2: {title: 'Mutation rate'},
            data: [{
                name: 'Fitness',
                type: "line",
                showInLegend: true,
                xValueFormatString: "generation 0",
                yValueFormatString: "0 fitness",
                indexLabelFontSize: 12,
                dataPoints: [],
            }, {
                name: 'Mutation rate',
                type: "line",
                axisYType: 'secondary',
                showInLegend: true,
                xValueFormatString: 'generation 0',
                yValueFormatString: '0 mutation rate',
                indexLabelFontSize: 12,
                dataPoints: []
            }]
        });
        this.update();
    }

    _calcY(individual) {
        const step = 50 / (individual.genes.length / 2);
        const stepsTaken = individual.ones > individual.genes.length / 2 ? individual.zeroes : individual.ones;
        const minimum = 50 - step * stepsTaken;
        const maximum = 50 + step * stepsTaken;
        const range = maximum - minimum;
        
        const genes = individual.genes;
        let max = 0, min = 0, count = 0;
        let maxValue = genes.length;
        let minValue = 1;
        for (let i = 0; i < genes.length; i++) {
            if (genes[i]) {
                count += i + 1;
                min += minValue++;
                max += maxValue--;
            }
        }
        const progress = (count - min) / (max - min);
        return minimum + range * progress;
    }

    search(geneCount) {
        this.formatter = statistics => {
            const y = this._calcY(statistics.current);
            return {primary: {x: statistics.current.ones, y: y, label: statistics.generations}}
        };
        const height = 100;
        this.graph = new CanvasJS.Chart(this.id, {
            animationEnabled: false,
            theme: "light2",
            title: {text: 'Search space'},
            axisX: {title: 'Ones', minimum: 0, maximum: geneCount},
            axisY: {title: '', minimum: 0, maximum: height},
            data: [{
                name: 'Explored',
                type: "scatter",
                showInLegend: true,
                xValueFormatString: "##### ones",
                yValueFormatString: "#####",
                indexLabelFontSize: 12,
                dataPoints: [],
            }, {
                name: 'Border',
                type: "line",
                markerType: 'none',
                showInLegend: false,
                yValueFormatString: "",
                xValueFormatString: "",
                indexLabelFontSize: 12,
                dataPoints: [
                    {x: 0, y: height / 2},
                    {x: geneCount / 2, y: height},
                    {x: geneCount, y: height / 2},
                    {x: geneCount / 2, y: 0},
                    {x: 0, y: height / 2}
                ]
            }]
        });
        this.update();
    }

    /**
     * @param {string} id
     * @param {function(Statistics):{
     *      primary: {x: number, y: number}, 
     *      secondary: {x: number, y: number},
     *      ternary: {x: number, y: number}
     *  }} formatter
     */
    constructor(id, formatter = null) {
        this.id = id;
        this.element = document.getElementById(id);
        this.graph = null;
        this.formatter = formatter;
    }

    /**
     * @param {Statistics} statistics
     */
    add(statistics) {
        if (!statistics || !this.formatter) return;
        const dataPoints = this.formatter(statistics);
        const p = this.graph.options.data[0];
        const s = this.graph.options.data[1];
        const t = this.graph.options.data[2];
        if (dataPoints.primary)
            p.dataPoints.push(dataPoints.primary);
        if (dataPoints.secondary)
            s.dataPoints.push(dataPoints.secondary);
        if (dataPoints.ternary)
            t.dataPoints.push(dataPoints.ternary);
        this.update();
    }

    update() {
        this.graph.render();
    }

    clear() {
        for (let i = 0; i < this.element.children.length; i++)
            this.element.children[i].remove();
        this.graph = null;
        this.formatter = null;
    }
}