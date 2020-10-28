class Graph {
    lambda() {
        this.formatter = statistics => {
            const start = statistics.generations - statistics.history.length;
            const fitness = [];
            for (let i = 0; i < statistics.history.length; i++)
                fitness.push({x: start + i, y: statistics.history[i].fitness});
            const lambda = [];
            for (let i = 0; i < statistics.history.length; i++)
                lambda.push({x: start + i, y: statistics.parameters[i].lambda});
            return {primary: fitness, secondary: lambda}
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
                xValueFormatString: "generation #",
                yValueFormatString: "# fitness",
                indexLabelFontSize: 12,
                dataPoints: [],
            }, {
                name: 'Lambda',
                type: "line",
                axisYType: 'secondary',
                showInLegend: true,
                xValueFormatString: 'generation #',
                yValueFormatString: '# lambda',
                indexLabelFontSize: 12,
                dataPoints: []
            }]
        });
        this.update();
    }

    mutation() {
        this.formatter = statistics => {
            const start = statistics.generations - statistics.history.length;
            const fitness = [];
            for (let i = 0; i < statistics.history.length; i++)
                fitness.push({x: start + i, y: statistics.history[i].fitness});
            const mutation = [];
            for (let i = 0; i < statistics.history.length; i++)
                mutation.push({x: start + i, y: statistics.parameters[i].mutationRate});
            return {primary: fitness, secondary: mutation}
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
                xValueFormatString: "generation #",
                yValueFormatString: "# fitness",
                indexLabelFontSize: 12,
                dataPoints: [],
            }, {
                name: 'Mutation rate',
                type: "line",
                axisYType: 'secondary',
                showInLegend: true,
                xValueFormatString: 'generation #',
                yValueFormatString: '# mutation rate',
                indexLabelFontSize: 12,
                dataPoints: []
            }]
        });
        this.update();
    }

    search(geneCount) {
        this.dataFormatter = statistics => ({primary: []});
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
                xValueFormatString: "#",
                yValueFormatString: "# ones",
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
     * @param {function(Statistics):{primary: {x: number, y: number}[], secondary: {x: number, y: number}[]}} formatter
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
        if (dataPoints.primary)
            p.dataPoints = p.dataPoints.concat(dataPoints.primary);
        if (dataPoints.secondary)
            s.dataPoints = s.dataPoints.concat(dataPoints.secondary);
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