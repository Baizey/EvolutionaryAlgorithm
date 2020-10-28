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
    async getStatistics(includeHistory = true) {
        return new Statistics(await this._call('Statistics', 'GET', {includeHistory: includeHistory}));
    }

    /**
     * @param {Object} body
     * @returns Promise<void>
     */
    initialize(body = {}) {
        return this._call('Initialize', 'POST', body);
    }

    /**
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
}