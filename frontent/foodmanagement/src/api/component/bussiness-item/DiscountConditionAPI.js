import BaseAPI from '../../base/BaseAPI.js';

class DiscountConditionAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "DiscountCondition";
    }
}

export default new DiscountConditionAPI();