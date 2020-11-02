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
     * @returns {Promise<{from: number, to: number, id: number}[]>}
     */
    async getEdges() {
        return (await this._call('Edges', 'GET'));
    }

    /**
     * @returns {Promise<Node[]>}
     */
    async getNodes() {
        return (await this._call('Nodes', 'GET')).map(e => new GraphNode(e));
    }

    /**
     * @returns {Promise<Statistics>}
     */
    async getStatistics() {
        return new Statistics(await this._call('Statistics', 'GET'));
    }

    /**
     * @param {Object} body
     * @returns Promise<void>
     */
    initialize(body = {}) {
        return this._call('Initialize', 'POST', body);
    }

    /**
     * @param {Object} body
     * @returns Promise<void>
     */
    run(body) {
        return this._call('Run', 'PUT', body);
    }

    /**
     * @returns Promise<void>
     */
    pause() {
        return this._call('Pause', 'DELETE');
    }
}