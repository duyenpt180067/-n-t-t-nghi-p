import BaseAPI from '../../base/BaseAPI.js';

class FoodDetailAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "FoodDetail";
    }
}

export default new FoodDetailAPI();