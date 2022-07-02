<template>
<div class="content-init">
    <div class="flex content-head">
        <div class="content-title">Danh sách câu hỏi thường gặp</div>
        <div class="btn flex-center">
            <button class="admin-btn-primary add-btn" @click="openForm(null, 'post')">
                <i class="fa fa-plus"></i>
                Thêm mới
            </button>
        </div>
    </div>
    <div class="content-action flex">
        <div class="action-left flex">
            <div class="content-filter flex">
                <input placeholder="Nhập câu hỏi, mã câu hỏi để tìm kiếm" 
                        type="text" 
                        ref="Filter" 
                        id="filter"
                        v-model="keyword"
                        v-tooltip.bottom-end="{offset: '5px', content:'Nhấp Enter để tìm kiếm', classes:'tooltip'}" @keyup.enter="getFilterPage()">
                <div class="content-filter-search logo-icon other-icon pointer"></div>
            </div>
            <div class="admin-btn-normal m-l-12 pointer" style="padding: 5px 10px;" v-show="showDeleteMulti" @click="showPopup('delete')">Xóa</div>
        </div>
        
        <div class="action-right flex">
            
            <div class="btn-filter-bar btn-action flex-center" v-tooltip.bottom="{content:'Lấy lại dữ liệu', classes:'tooltip', offset: '5px'}" @click="reload()">
                <div class="refresh-icon other-icon logo-icon pointer"></div>
            </div>
            <div class="btn-filter-bar btn-action flex-center" v-tooltip.bottom="{content:'Thiết lập cột', classes:'tooltip', offset: '5px'}" @click="settingColumn()">
                <div class="setting-icon other-icon logo-icon pointer"></div>
            </div>
        </div>
    </div>
    <common-table :oneAction="false" :layoutConfig="layoutConfig" :dataList="dataList" :objectName="objectName" :formShow="formShow" @closeForm="formShow=false;"
                  :actions="actions" :checkPaging="false" :displayName="displayName" @changeCrud="changeCrud" @openForm="openForm" @getFilterPage="getFilterPage"
                  @changeShowDeleteMulti="changeShowDeleteMulti" ref="CommonTable" @crudObject="crudObject">
    </common-table>
    <faq-detail  v-if="formShow" 
                    @closeForm="formShow=false;" 
                    @crudObject="crudObject" 
                    :crud="crud"
                    @btnXForm="btnXForm"
                    :entityDetail="entityDetail" 
                    @changeCrud="changeCrud"
                    ref="FaqDetail"></faq-detail>
</div>
</template>

<style>

</style>

<script>
import FaqAPI from '../../../../../api/component/bussiness-item/FaqAPI'
import CommonTable from '../../../../base/CommonTable.vue';
import { Actions} from '../../../../../base/vi/Resources.js';
import { CRUD, PopupState, ToastMessageIcon } from '../../../../../base/Resources.js';
import { ToastMgs } from '../../../../../base/vi/Resources.js';
import Base from '../../../../../base/Base.js';
import FaqDetail from './FaqDetail.vue';
export default {
    components: { CommonTable, FaqDetail },
    name:'FaqAdmin',
    props:{
        loader: Boolean,

    },
    async created() {
        try {
            // hiển thị loader
            this.changeLoader(true);
            this.layoutConfig = await FaqAPI.getLayoutConfig();
            //this.dataList = await FaqAPI.getFilterPaging(this.paramGet);
            await this.getFilterPage();
            this.changeLoader(false);
        } catch (error) {
            this.changeLoader(false);
            console.log(error);
        }
    },
    data(){
        return {
            layoutConfig: [],
            dataList:[],
            objectName: 'Faq',
            displayName:'Faq',
            paramGet: {
                FaqFilter: null,
                PageNumber: 1,
                PageSize: 20
            },
            // các chức năng
            actions: Actions,
            keyword:"",
            entityDetail: {
                FaqCode: '', 
                FaqName:'', 
                FaqQuestion: '', 
                FaqAnswer: ''
            },
            formShow: false,
            crud:'post',
            listMgs:[],
            showDeleteMulti: false,
        }
    },

    methods:{

        changeLoader(value){
            this.$emit('changeLoader', value);
        },
        async getFilterPage(){
            this.paramGet = {
                FaqFilter: this.keyword,
                PageNumber: this.$refs.CommonTable.currentPage,
                PageSize: this.$refs.CommonTable.pageSize
            };
            this.dataList = await FaqAPI.getFilterPaging(this.paramGet);
            this.$refs.CommonTable.totalPages = this.dataList.totalPage;
        },
        async reload(){
            this.keyword = "";
            await this.getFilterPage();
        },

        /**
         * Đổi trạng thái crud
         * Create: PTDuyen(10/09/2021)
         */
        changeCrud(action){
            this.crud = action;
        },

        changeShowDeleteMulti(value){
            this.showDeleteMulti = value;
        },

        /**
         * Đổi trạng thái đóng mở form
         * Create: PTDuyen(10/09/2021)
         */
        changeForm(action){
            this.formShow = action;
        },

         /**
         * Mở form
         * Create: PTDuyen(09/09/2021)
         */
        openForm(data, action) {
            this.formShow = true;
            this.crud = action;
            if ((this.crud == CRUD.Read)||(this.crud == CRUD.Put)||this.crud == CRUD.Copy) {
                this.entityDetail = Object.assign({}, data);
            }
            else {
                this.entityDetail = {
                    FaqCode: '', 
                    FaqName:'', 
                    Descriptions: '', 
                    FaqImage: ''
                };
            }
        },

        showPopup(action){
            this.$refs.CommonTable.showPopup(action);
        },

        btnXForm(obj, oldObj, crud, objectCode){
            this.$refs.CommonTable.btnXForm(obj, oldObj, crud, objectCode);
        },

        /**
         * Thực hiện các hành động thêm sửa xóa
         * Create: PTDuyen(28/09/2021)
         */
        async crudObject(obj){
            try{
                this.keyword = "";
                //  nếu là xóa
                if (this.crud == CRUD.Delete) {
                    console.log(this.$refs.CommonTable.listChoose);
                    this.changeLoader(true);
                    // thành công
                    // console.log(this.listChoose);
                    let listDelete = this.$refs.CommonTable.listChoose.map(item => item.FaqId);
                    var { data } = await FaqAPI.deleteMultiObj(listDelete);
                    this.changeLoader(false);
                    if(data.Data){
                        this.message = data.data;
                        this.$refs.CommonTable.showPopup(PopupState.Duplicate);
                    }
                    else {
                        this.listMgs.push({
                            showToast: true,
                            titleMgs: Base.stringFormat(ToastMgs.DeleteSuccess, [this.message]),
                            iconToast: ToastMessageIcon.success
                        })
                        this.listChoose = [];
                        // tải lại dữ liệu, trở về trang 1
                        this.$refs.CommonTable.currentPage = 1;
                        this.$refs.CommonTable.$refs.ChooseAll.checked = false;
                        await this.getFilterPage();
                    }
                }
                else {
                    if(this.$refs.FaqDetail.validateForm() == true){
                        // nếu là thêm mới hoặc nhân bản
                        if((this.crud == CRUD.Post)||(this.crud == CRUD.Copy)){
                            // thực hiện thêm mới
                            // thêm loader
                            this.changeLoader(true);
                            let res = await FaqAPI.postObj(this.$refs.FaqDetail.faq);
                            // bỏ loader
                            this.changeLoader(false);
                            // nếu statusCode = 201
                            if(res.status == 201){
                                // thông báo thành công
                                this.listMgs.push({
                                    showToast: true,
                                    titleMgs: ToastMgs.PostSuccess,
                                    iconToast: ToastMessageIcon.success
                                })
                                // tải lại dữ liệu
                                // trở về trang 1
                                this.$refs.CommonTable.currentPage = 1;
                                await this.getFilterPage();
                                if(this.$refs.FaqDetail.post_save == true){
                                    await this.postSave();
                                }
                                else this.formShow = false;
                            } 
                            // nếu không thêm mới thành công
                            else {
                                //Base.formatObj(this.$refs.FaqDetail.faq);
                                this.$refs.FaqDetail.message = res.data.data[0];
                                this.$refs.FaqDetail.$refs.FaqCode.classList.add("is-invalid");
                                this.$refs.FaqDetail.showPopup(PopupState.Duplicate);
                            }
                            // bỏ loader
                            this.changeLoader(false);
                            return;
                        }
                        // nếu là cập nhật
                        else if(this.crud == CRUD.Put){
                            //  thực hiện cập nhật
                            // nếu dữ liệu bị thay đổi
                            if(Base.checkChangeData(this.$refs.FaqDetail.faq, this.entityDetail, this.crud, 'FaqCode') == false){
                                // thêm loader
                                this.changeLoader(true);
                                //Base.formatObjToSave(this.$refs.FaqDetail.faq);
                                let res = await FaqAPI.putObj(this.$refs.FaqDetail.faq);
                                // bỏ loader
                                this.changeLoader(false);
                                // nếu statusCode = 202
                                if(res.status == 202){
                                    // thông báo thành công
                                    this.listMgs.push({
                                        showToast: true,
                                        titleMgs: ToastMgs.PutSuccess,
                                        iconToast: ToastMessageIcon.success
                                    })
                                    // tải lại dữ liệu
                                    await this.getFilterPage();
                                    if(this.$refs.FaqDetail.post_save == true){
                                        await this.postSave();
                                    }
                                    else {
                                        this.formShow = false;
                                    }
                                } 
                                // nếu cập nhật thất bại
                                else{
                                    this.$refs.FaqDetail.message = res.data.data[0];
                                    this.$refs.FaqCode.classList.add("is-invalid");
                                    this.$refs.FaqDetail.showPopup(PopupState.Duplicate);
                                }
                                this.changeLoader(false);
                                return;
                            }
                            // nếu dữ liệu không bị thay đổi 
                            else {
                                // nếu ấn cất và thêm
                                if(this.$refs.FaqDetail.post_save == true){
                                    await this.postSave();
                                }
                                // nếu không
                                else{
                                    this.formShow = false;
                                }
                            }
                        }
                    }
                }
            }
            catch(ex){
                // console.log(ex);
                this.changeLoader(false);
                if (this.crud == CRUD.Delete){
                    this.listMgs.push({
                        showToast: true,
                        titleMgs: Base.stringFormat(ToastMgs.DeleteError, [this.message]),
                        iconToast: ToastMessageIcon.error
                    })
                }
                // nếu là thêm mới hoặc nhân bản lỗi
                else if((this.crud == CRUD.Post)||(this.crud == CRUD.Copy)){
                    this.listMgs.push({
                        showToast: true,
                        titleMgs: Base.stringFormat(ToastMgs.PostError,[obj.FaqName]),
                        iconToast: ToastMessageIcon.error
                    })
                }
                // nếu là cập nhật lỗi
                else if (this.crud == CRUD.Put){
                    this.listMgs.push({
                        showToast: true,
                        titleMgs: Base.stringFormat(ToastMgs.PutError,[obj.FaqName]),
                        iconToast: ToastMessageIcon.error
                    })
                }
                else {
                    this.listMgs.push({
                        showToast: true,
                        titleMgs: ToastMgs.ExceptionError,
                        iconToast: ToastMessageIcon.error
                    })
                }
                this.changeLoader(false);
            }
        },
        /**
         * Nếu là hành động cất và thêm
         * Create: PTDuyen(11/10/2021)
         */
        async postSave(){
            // reset lại validate
            this.$refs.FaqDetail.$nextTick(() => { this.$refs.FaqDetail.$v.$reset() })
            // đổi hành động về thêm mới
            this.changeCrud(CRUD.Post);
            // faq chuyển về giá trị ban đầu
            this.$refs.FaqDetail.faq = {
                FaqCode: '', 
                FaqName:'', 
                FaqQuestion: '', 
                FaqAnswer: ''
            };
            // lấy mã ncc mới
            await this.$refs.FaqDetail.getNewCode();
            // focus vào ô input object_code
            this.$refs.FaqDetail.$refs.FaqCode.focus();
            // chuyển cất và thêm về false
            this.$refs.FaqDetail.post_save = false;
        },

        settingColumn(){
            this.$refs.CommonTable.setColumn = !this.$refs.CommonTable.setColumn;
        }
    },
}
</script>