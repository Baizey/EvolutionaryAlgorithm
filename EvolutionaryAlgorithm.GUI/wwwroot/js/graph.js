class Graph {

    /**
     * @param {{x: number, y: number}[]} nodes
     */
    graph(nodes) {
        this.clear();
        const self = this;
        this.formatter = (graph, _, edges) => {
            edges = edges.map(e => ({
                data: {id: `e${e.id}`, source: `n${e.from}`, target: `n${e.to}`},
                group: 'edges'
            }));
            graph.remove(self._edges);
            self._edges = edges;
            graph.add(edges);
        };
        this.graph = new cytoscape({container: this.element});
        for (let i = 0; i < nodes.length; i++) {
            nodes[i].group = 'nodes'
            nodes[i].data = {id: `n${i}`}
        }
        this._nodes = nodes;
        this._edges = [];
        this.graph.add(nodes);
    }

    asymmetric() {
        this.clear();
        this.formatter = (graph, statistics) => {
            const p = graph.options.data[0].dataPoints;
            const s = graph.options.data[1].dataPoints;
            const t = graph.options.data[2].dataPoints;
            p.push({x: statistics.generations, y: statistics.current.fitness})
            s.push({x: statistics.generations, y: statistics.r0})
            t.push({x: statistics.generations, y: statistics.r1})
            graph.render();
        };
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
        this.clear();
        this.formatter = (graph, statistics) => {
            const p = graph.options.data[0].dataPoints;
            const s = graph.options.data[statistics.inStagnation ? 1 : 2].dataPoints;
            p.push({x: statistics.generations, y: statistics.current.fitness})
            s.push({x: statistics.generations, y: statistics.stagnationProgress})
            graph.render();
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
        this.clear();
        this.formatter = (graph, statistics) => {
            const p = graph.options.data[0].dataPoints;
            const s = graph.options.data[1].dataPoints;
            p.push({x: statistics.generations, y: statistics.current.fitness})
            s.push({x: statistics.generations, y: statistics.parameters.lambda})
            graph.render();
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
        this.clear();
        this.formatter = (graph, statistics) => {
            const p = graph.options.data[0].dataPoints;
            const s = graph.options.data[1].dataPoints;
            p.push({x: statistics.generations, y: statistics.current.fitness})
            s.push({x: statistics.generations, y: statistics.parameters.mutationRate})
            graph.render();
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
        this.clear();
        this.formatter = (graph, statistics) => {
            const p = graph.options.data[0].dataPoints;
            const y = this._calcY(statistics.current);
            p.push({x: statistics.current.ones, y: y})
            graph.render();
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
     * @param {function(any, Statistics, {id: string, from: string, to: string}[])} formatter
     */
    constructor(id, formatter = null) {
        this.id = id;
        this.element = document.getElementById(id);
        this.graph = null;
        this.formatter = formatter;
    }

    /**
     * @param {Statistics} statistics
     * @param {{id: string, source: string, target: string}[]} edges
     */
    add(statistics, edges) {
        if (!statistics || !this.formatter) return;
        this.formatter(this.graph, statistics, edges);
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