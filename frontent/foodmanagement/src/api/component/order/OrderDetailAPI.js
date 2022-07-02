import BaseAPI from '../../base/BaseAPI.js';

class OrderDetailAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "OrderDetail";
    }
}

export default new OrderDetailAPI();