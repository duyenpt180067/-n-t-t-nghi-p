import BaseAPI from '../../base/BaseAPI.js';

class CartDetailAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "CartDetail";
    }
}

export default new CartDetailAPI();