import BaseAPI from '../../base/BaseAPI.js';

class BlogAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "Blog";
    }
}

export default new BlogAPI();