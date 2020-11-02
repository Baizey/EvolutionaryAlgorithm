class GraphNode {
    constructor(data) {
        data ||= {};
        this.id = data.id;
        this.x = data.x;
        this.y = data.y;
    }
}

class GraphEdge {
    constructor(data) {
        data ||= {};
        this.id = data.id;
        this.from = data.from;
        this.to = data.to;
    }
}