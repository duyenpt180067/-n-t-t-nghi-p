import BaseAPI from '../../base/BaseAPI.js';
import BaseAPIConfig from '../../base/BaseAPIConfig.js';

class CommentAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "Comment";
    }

    async postListComment(body) {
        return await BaseAPIConfig.post(`${this.controller}/AddMultiComment`, body);
    }
}

export default new CommentAPI();