import BaseAPI from '../../base/BaseAPI.js';

class AnswerAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "Answer";
    }
}

export default new AnswerAPI();