import BaseAPI from '../../base/BaseAPI.js';

class DiscountAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "Discount";
    }
}

export default new DiscountAPI();