class Graph {

    /**
     * @param {Node[]} nodes
     * @param {{from: number, to: number, id: number}[]} edges
     */
    graph2D(nodes, edges) {
        this.clear();
        this.formatter = (graph, data) => {
            const ids = data.edges.map(e => `#p${e.id}`).join(', ');
            graph.edges().removeClass('highlighted');
            graph.edges().toggleClass('highlighted', false);
            graph.edges(ids).addClass('highlighted');
        };
        const element = document.createElement('div');
        element.setAttribute('style', 'width: 100%; height: 100%; display: block; text-align: left;')
        this.element.appendChild(element)
        this.graph = new cytoscape({
            container: element,
            autolock: true,
            style: cytoscape.stylesheet()
                .selector('edge').style({
                    'width': 5,
                    'height': 5,
                    'line-color': '#2a6cd6',
                    'opacity': 0.2,
                }).selector('edge.highlighted').style({
                    'width': 10,
                    'height': 10,
                    'line-color': '#2a6cd6',
                    'opacity': 1,
                })
        });
        for (let i = 0; i < nodes.length; i++)
            nodes[i] = {
                group: 'nodes',
                data: {
                    weight: 75,
                    id: `n${nodes[i].id}`
                },
                position: {
                    x: nodes[i].x,
                    y: nodes[i].y
                }
            }
        for (let i = 0; i < edges.length; i++)
            edges[i] = {
                group: 'edges',
                data: {
                    id: `p${edges[i].id}`,
                    source: `n${edges[i].from}`,
                    target: `n${edges[i].to}`
                },
            }
        this.graph.add(nodes);
        this.graph.add(edges);

        this._nodes = nodes;
        this._edges = [];
        this.graph.fit();
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
     * @param {function(any, Statistics)} formatter
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