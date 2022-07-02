import BaseAPI from '../../base/BaseAPI.js';

class QuestionAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "Question";
    }
}

export default new QuestionAPI();