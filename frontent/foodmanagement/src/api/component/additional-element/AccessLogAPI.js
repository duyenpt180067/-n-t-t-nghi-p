import BaseAPI from '../../base/BaseAPI.js';

class AccessLogAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "AccessLog";
    }
}

export default new AccessLogAPI();