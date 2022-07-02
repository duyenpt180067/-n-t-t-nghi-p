import BaseAPI from '../../base/BaseAPI.js';

class FaqAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "Faq";
    }
}

export default new FaqAPI();