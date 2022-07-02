import BaseAPI from '../../base/BaseAPI.js';
import BaseAPIConfig from '../../base/BaseAPIConfig.js';

class CategoryAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "Category";
    }

    /**
     * Phương thức lấy các danh mục thường đặt
     * Create: PTDuyen()
     */
    async getPopularCategory() {
        const { data } = await BaseAPIConfig.get(`${this.controller}/GetPopularCategories`);
        return data;
    }
}

export default new CategoryAPI();