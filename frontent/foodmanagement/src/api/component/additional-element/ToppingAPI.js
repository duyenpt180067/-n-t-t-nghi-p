import BaseAPI from '../../base/BaseAPI.js';

class ToppingAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "Topping";
    }
}

export default new ToppingAPI();