import BaseAPI from '../../base/BaseAPI.js';
import BaseAPIConfig from '../../base/BaseAPIConfig';

class FoodAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "Food";
    }

    /**
     * Phương thức lấy dữ liệu theo Id
     * Create: PTDuyen(16/08/2021)
     */
    async getFoodByCode(code) {
        const { data } = await BaseAPIConfig.get(`${this.controller}/GetByCode/${code}`);
        return data;
    }

    /**
     * Phương thức lấy dữ liệu theo Id
     * Create: PTDuyen(16/08/2021)
     */
    async getFoodPopular(categoryId) {
        const { data } = await BaseAPIConfig.post(`${this.controller}/GetPopularFood`, categoryId);
        return data;
    }

    /**
     * Phương thức tạo bản ghi mới
     * Create: PTDuyen(16/08/2021)
     */
    async postFood(body) {
        return await BaseAPIConfig.post(`${this.controller}/AddFood`, body);
    }

    /**
     * Hàm cập nhật dữ liệu
     * Create: PTDuyen(16/08/2021)
     * @param {*} body 
     */
    async putFood(body) {
        return await BaseAPIConfig.put(`${this.controller}/UpdateFood`, body);
    }
}

export default new FoodAPI();