import BaseAPIConfig from './BaseAPIConfig';

export default class BaseAPI {

    constructor() {
        this.controller = null;
    }

    /**
     * Phương thức lấy tất cả dữ liệu
     * Create: PTDuyen(16/08/2021)
     */
    async getAll() {
        const { data } = await BaseAPIConfig.get(`${this.controller}`);
        if (typeof(data) == 'string') {
            return [];
        } else return data;
    }

    /**
     * Phương thức lấy dữ liệu theo Id
     * Create: PTDuyen(16/08/2021)
     */
    async getObjById(id) {
        const { data } = await BaseAPIConfig.get(`${this.controller}/${id}`);
        return data;
    }

    /**
     * Phương thức lấy dữ liệu theo Id
     * Create: PTDuyen(16/08/2021)
     */
    async getObjByCode(code) {
        const { data } = await BaseAPIConfig.post(`${this.controller}/GetObjByCode`, code);
        return data;
    }

    /**
     * Phương thức tạo bản ghi mới
     * Create: PTDuyen(16/08/2021)
     */
    async postObj(body) {
        return await BaseAPIConfig.post(`${this.controller}`, body);
    }

    /**
     * Hàm cập nhật dữ liệu
     * Create: PTDuyen(16/08/2021)
     * @param {*} body 
     */
    async putObj(body) {
        return await BaseAPIConfig.put(`${this.controller}`, body);
    }

    /**
     * Hàm xóa bản ghi
     * Create: PTDuyen(16/08/2021)
     * @param {*} id 
     */
    async deleteObj(id) {
        return await BaseAPIConfig.delete(`${this.controller}/${id}`);
    }

    /**
     * Hàm xóa nhiều bản ghi
     * Create: PTDuyen(16/08/2021)
     * @param {*} body 
     */
    async deleteMultiObj(body) {
        return await BaseAPIConfig.delete(`${this.controller}/DeleteMulti`, { data: body });
    }

    /**
     * Phương thức phân trang
     * Create: PTDuyen()
     */
    async getFilterPaging(body) {
        const { data } = await BaseAPIConfig.post(`${this.controller}/GetFilterPaging`, body);
        return data
    }

    /**
     * Phương thức lấy layout
     * Create: PTDuyen(16/08/2021)
     */
    async getLayoutConfig() {
        const { data } = await BaseAPIConfig.get(`${this.controller}/GetLayoutConfig`);
        if (typeof(data) == 'string') {
            return [];
        } else return data;
    }

    /**
     * Hàm lấy mã code mới
     * Create: PTDuyen(16/08/2021)
     * @returns 
     */
    async getNewCode() {
        const { data } = await BaseAPIConfig.get(`${this.controller}/NewCode`);
        return data;
    }

    /**
     * Phương thức phân trang
     * Create: PTDuyen()
     */
    async uploadFile(body) {
        const { data } = await BaseAPIConfig.post(`File`, body);
        return data
    }
}