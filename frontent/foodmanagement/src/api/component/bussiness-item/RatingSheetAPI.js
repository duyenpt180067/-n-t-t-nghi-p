import BaseAPI from '../../base/BaseAPI.js';

class RatingSheetAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "RatingSheet";
    }
}

export default new RatingSheetAPI();