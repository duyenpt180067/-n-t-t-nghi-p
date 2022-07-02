<template>
<div class="content-init">
    <div class="flex content-head">
        <div class="content-title">Danh sách sản phẩm</div>
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
                <input placeholder="Nhập tên sản phẩm, mã thực phẩ... để tìm kiếm" 
                        type="text" 
                        ref="Filter" 
                        id="filter"
                        v-model="keyword"
                        v-tooltip.bottom-end="{offset: '5px', content:'Nhập tên sản phẩm, mã sản phẩm, tác giả và nhấp Enter để tìm kiếm', classes:'tooltip'}" @keyup.enter="getFilterPage()">
                <div class="content-filter-search logo-icon other-icon pointer"></div>
            </div>
            <v-combobox class="w-1/3 category-data m-l-12" 
                        @getSelect="category_model=$event;getFilterPage();"
                        :combobox_valid="false" 
                        :item_text="'CategoryName'" 
                        :items="categoryList" 
                        :vmodel="category_model"
                        :multiple="false"
                        :readonly="false">
            </v-combobox>
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
                  :actions="actions" :checkPaging="true" :displayName="displayName" @changeCrud="changeCrud" @openForm="openForm" @getFilterPage="getFilterPage"
                  @changeShowDeleteMulti="showDeleteMulti=$event" ref="CommonTable" @crudObject="crudObject">
    </common-table>
</div>
</template>

<style>

.content .content-action{
    padding: 15px 12px;
    background-color: #fff;
}
.content .content-action .action-left{
    flex: 1;
}
.content .content-filter{
    position: relative;
    width: 340px;
}
.content-filter input {
    padding-right: 30px !important;
}
.content-filter .content-filter-search {
    position: absolute;
    right: 0;
    width: 30px;
    font-size: 6px;
    cursor: pointer;
    height: 30px;
    background-position: -987px -354px;
}

.content-filter .content-filter-search:hover {
    background-position: -311px -141px;
}

.content .content-action .btn-filter-bar{
    height: 100%;
    font-size: 14px;
    justify-content: space-between;
    width: 60px;
    padding: 0 8px;
    border-radius: 3px;
    border: 1px solid var(--gray-color);
    margin-left: 10px;
    transition: .2s;
}

.content .content-action .btn-action{
    width: fit-content;
}

.content .content-action .btn-filter-bar:hover{
    font-weight: bold;
    background-color: #f0f2f5;
}
</style>

<script>
import FoodAPI from '../../../../api/component/food/FoodAPI'
import CategoryAPI from '../../../../api/component/food/CategoryAPI'
import CommonTable from '../../../base/CommonTable.vue';
import { Actions} from '../../../../base/vi/Resources.js';
import { CRUD, PopupState, ToastMessageIcon } from '../../../../base/Resources.js';
import { ToastMgs } from '../../../../base/vi/Resources.js';
import Base from '../../../../base/Base.js';
import VCombobox from '@/components/base/VCombobox';
export default {
    components: { CommonTable, VCombobox },
    name:'FoodAdmin',
    props:{
        loader: Boolean,

    },
    async created() {
        try {
            // hiển thị loader
            this.changeLoader(true);
            this.category_model = this.categoryList[0];
            this.layoutConfig = await FoodAPI.getLayoutConfig();
            this.dataList = await FoodAPI.getFilterPaging(this.paramGet);
            let categoryData = await CategoryAPI.getFilterPaging({CategoryFilter:null,CategoryStatus:null});
            this.categoryList = this.categoryList.concat(categoryData.data);
            this.changeLoader(false);
        } catch (error) {
            this.changeLoader(false);
            this.listMgs.push({
                showToast: true,
                titleMgs: "Tải dữ liệu thất bại! Vui lòng kiểm tra lại kết nối mạng hoặc liên hệ để được trợ giúp",
                iconToast: 'error'
            });
        }
    },
    data(){
        return {
            layoutConfig: [],
            dataList:[],
            objectName: 'Food',
            displayName:'Danh mục',
            paramGet: {
                CategoryId: null,
                FoodFilter: null,
                PageNumber: 1,
                PageSize: 20
            },
            // các chức năng
            actions: Actions,
            keyword:"",
            entityDetail: {
                FoodCode: '', 
                FoodName:'', 
                Descriptions: '', 
                FoodImage: ''
            },
            formShow: false,
            crud:'post',
            listMgs:[],
            // danh sách category 
            categoryList: [{
                CategoryId:'',
                CategoryName: 'Tất cả danh mục'
            }],
            // object gửi xuống combobox để hiển thị
            category_model: null,
            showDeleteMulti: false,
        }
    },

    methods:{
        settingColumn(){
            this.$refs.CommonTable.setColumn = !this.$refs.CommonTable.setColumn;
        },
        changeLoader(value){
            this.$emit('changeLoader', value);
        },
        async getFilterPage(){
            this.paramGet = {
                FoodFilter: this.keyword,
                CategoryId: this.category_model.CategoryId?this.category_model.CategoryId:null,
                PageNumber: this.$refs.CommonTable.currentPage,
                PageSize: this.$refs.CommonTable.pageSize
            };
            this.showDeleteMulti = false;
            this.dataList = await FoodAPI.getFilterPaging(this.paramGet)
        },
        async reload(){
            this.keyword = null;
            await this.getFilterPage();
        },

        /**
         * Đổi trạng thái crud
         * Create: PTDuyen(10/09/2021)
         */
        changeCrud(action){
            this.crud = action;
        },

        /**
         * Đổi trạng thái đóng mở form
         * Create: PTDuyen(10/09/2021)
         */
        changeForm(action){
            this.formShow = action;
        },

        //  /**
        //  * Mở form
        //  * Create: PTDuyen(09/09/2021)
        //  */
        // openForm(data, action) {
        //     this.formShow = true;
        //     this.crud = action;
        //     if ((this.crud == CRUD.Read)||(this.crud == CRUD.Put)||this.crud == CRUD.Copy) {
        //         this.entityDetail = Object.assign({}, data);
        //     }
        //     else {
        //     }
        // },

        /**
         * Mở form
         * Create: PTDuyen(09/09/2021)
         */
        openForm(data, action) {
            this.formShow = true;
            this.crud = action;
            this.entityDetail = {
                FoodCode: '', 
                FoodId:'',
                FoodName:'', 
                Descriptions: '', 
                FoodImage: ''
            };
            if(data){
                this.entityDetail = data;
            }
            // if ((this.crud == CRUD.Read)||(this.crud == CRUD.Put)||this.crud == CRUD.Copy) {
            //     this.entityDetail = Object.assign({}, data);
            // }
            if(this.crud == CRUD.Copy){
                this.$router.push({path: '/app/food-detail/copy', name: 'CopyFoodDetail', params:{copyCode: this.entityDetail.FoodCode}});
            }
            else {
                this.$router.push({path: '/app/food-detail', name: 'FoodDetail', params:{code: this.entityDetail.FoodCode}});
            }
            // else if (this.crud == CRUD.Copy){
            //     this.entityDetail = Object.assign({}, data);
            //     if(ballotDetail.ballot_code.includes("PC")){
            //         this.$router.push({name:'AddPaymentDetail', params:{copyId: ballotDetail.ballot_id}});
            //     }
            //     else{
            //         this.$router.push({name:'AddReceiptDetail', params:{copyId: ballotDetail.ballot_id}});
            //     }
            // }
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
                    this.changeLoader(true);
                    // thành công
                    // console.log(this.listChoose);
                    let listDelete = this.$refs.CommonTable.listChoose.map(item => item.FoodId);
                    var { data } = await FoodAPI.deleteMultiObj(listDelete);
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
                        titleMgs: Base.stringFormat(ToastMgs.PostError,[obj.FoodName]),
                        iconToast: ToastMessageIcon.error
                    })
                }
                // nếu là cập nhật lỗi
                else if (this.crud == CRUD.Put){
                    this.listMgs.push({
                        showToast: true,
                        titleMgs: Base.stringFormat(ToastMgs.PutError,[obj.FoodName]),
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
    },
}
</script>

<style>

</style>