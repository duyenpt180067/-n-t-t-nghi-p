<template>
<div class="content-init">
    <div class="flex content-head">
        <div class="content-title">Danh sách điều kiện giảm giá</div>
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
                <input placeholder="Nhập điều kiện giảm giá để tìm kiếm" 
                        type="text" 
                        ref="Filter" 
                        id="filter"
                        v-model="keyword"
                        v-tooltip.bottom-end="{offset: '5px', content:'Nhấp Enter để tìm kiếm', classes:'tooltip'}" @keyup.enter="getFilterPage()">
                <div class="content-filter-search logo-icon other-icon pointer"></div>
            </div>
            <div class="filter-pop m-l-12" style="position:relative;">
                <button class="btn-filter-bar flex-center" type="button" id="btnFilterToggle" @click="showFilter=!showFilter;">
                    <i class="fa fa-filter"></i>
                    <span>Lọc</span>
                </button>
                <div class="pop-filter" :class="{'show-filter':showFilter, 'h-0':!showFilter}">
                    <div class="other-icon logo-icon btn-close pointer" id="btn-close" style="position: absolute;right: 5px;top: 5px;"
                            @click="showFilter=false;">
                    </div>
                    <div class="pop-init">
                        <div class="pop-info1">
                            <div class="m-b-3">
                                <p class="m-b-3">Trạng thái hóa đơn</p>
                                <v-combobox @getSelect="operation=$event" 
                                            :combobox_valid="false" 
                                            :placeholder="''" 
                                            :item_text="'text'" 
                                            :items="listOperation" 
                                            :vmodel="operation"
                                            :multiple="false">
                                </v-combobox>
                            </div>
                        </div>
                        <div class="pop-info3">
                            <p>Thời gian</p>
                            <div class="flex">
                                <div class="w-2/5 m-r-12">
                                    <v-combobox @getSelect="dateFilter=$event" 
                                                :combobox_valid="false" 
                                                :placeholder="''" 
                                                :item_text="'text'" 
                                                :items="listTimeToFilter" 
                                                :vmodel="dateFilter"
                                                :multiple="false">
                                    </v-combobox>
                                </div>
                                <div class="w-3/5 flex">
                                    <div class="w-1/2 m-r-12">
                                        <v-datepicker :dataDate="dateFilter.value.startDate.toString()" 
                                                        @changeDate="changeDate($event, 'startDate')" 
                                                        :checkDateValid="true">
                                        </v-datepicker>
                                    </div>
                                    <div class="w-1/2">
                                        <v-datepicker :dataDate="dateFilter.value.endDate.toString()" 
                                                    @changeDate="changeDate($event, 'endDate')" 
                                                    :checkDateValid="true">
                                        </v-datepicker>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-footer pop-footer flex">
                            <div class="admin-btn-normal btn-delete" @click="resetFilter()">Đặt lại</div>
                            <div class="admin-btn-normal admin-btn-primary" @click="filterMore()">Lọc</div>
                        </div>
                    </div>
                </div>
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
                  :actions="actions" :checkPaging="true" :displayName="displayName" @changeCrud="changeCrud" @openForm="openForm" @getFilterPage="getFilterPage"
                  @changeShowDeleteMulti="changeShowDeleteMulti" ref="CommonTable" @crudObject="crudObject">
    </common-table>
    <discount-condition-detail v-if="formShow" 
                    @closeForm="formShow=false;" 
                    @crudObject="crudObject" 
                    :crud="crud"
                    @btnXForm="btnXForm"
                    :entityDetail="entityDetail" 
                    @changeCrud="changeCrud"
                    ref="DiscountConditionDetail"></discount-condition-detail>
</div>
</template>

<style>

</style>

<script>
import DiscountConditionAPI from '../../../../../api/component/bussiness-item/DiscountConditionAPI'
import CommonTable from '../../../../base/CommonTable.vue';
import VDatepicker from '@/components/base/VDatepicker';
import VCombobox from '@/components/base/VCombobox';
import { Actions} from '../../../../../base/vi/Resources.js';
import { CRUD, PopupState, ToastMessageIcon } from '../../../../../base/Resources.js';
import { ToastMgs, Operation, ListTimeToFilter } from '../../../../../base/vi/Resources.js';
import Base from '../../../../../base/Base.js';
import DiscountConditionDetail from './DiscountConditionDetail.vue';
export default {
    components: { CommonTable, DiscountConditionDetail, VDatepicker, VCombobox },
    name:'DiscountConditionAdmin',
    props:{
        loader: Boolean,

    },
    async created() {
        try {
            // hiển thị loader
            this.changeLoader(true);
            this.layoutConfig = await DiscountConditionAPI.getLayoutConfig();
            //this.dataList = await DiscountConditionAPI.getFilterPaging(this.paramGet);
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
            objectName: 'DiscountCondition',
            displayName:'Điều kiện giảm giá',
            paramGet: {
                DiscountConditionFilter: null,
                PageNumber: 1,
                PageSize: 20,
                DiscountConditionMinInput: null,
                DiscountConditionMaxInput: null,
                ListDiscountConditionObject: null,
                ListUserSatisfy: null
            },
            // các chức năng
            actions: Actions,
            keyword:"",
            entityDetail: {
                DiscountConditionCode: '', 
                DiscountConditionTitle:'', 
                DiscountConditionAmount: '', 
                DiscountConditionMaxAmount: '',
                DiscountConditionStartDate: new Date(), 
                DiscountConditionEndDate: new Date(),
            },
            formShow: false,
            crud:'post',
            listMgs:[],
            showDeleteMulti: false,
            showFilter: false,
            listOperation: Operation,
            operation: Operation[3],
            listTimeToFilter: ListTimeToFilter,
            dateFilter: ListTimeToFilter[0],

        }
    },

    methods:{

        changeLoader(value){
            this.$emit('changeLoader', value);
        },
        async getFilterPage(){
            this.paramGet = {
                DiscountConditionFilter: this.keyword,
                DiscountConditionMinInput: null,
                DiscountConditionMaxInput: null,
                ListDiscountConditionObject: null,
                ListUserSatisfy: null,
                PageNumber: this.$refs.CommonTable.currentPage,
                PageSize: this.$refs.CommonTable.pageSize
            };
            this.dataList = await DiscountConditionAPI.getFilterPaging(this.paramGet);
            this.$refs.CommonTable.totalPages = this.dataList.totalPage;
        },
        async reload(){
            this.keyword = "";
            await this.getFilterPage();
        },
        
        /**
         * Đặt lại điều kiện lọc
         * Create: PTDuyen(29/09/2021)
         */
        resetFilter(){
            this.dateFilter = this.listTimeToFilter[0];
            this.operation = this.listOperation[3];
            this.moreFilter = false;
            this.showFilter=false;
        },

        /**
         * Lay ngày của dateFilter
         * Create:PTDuyen(17/10/2021)
         */
        changeDate(date, model){
            if(date){
                this.dateFilter.value[model] = date;
            }
            else {
                this.dateFilter.value[model] = "";
            }
        },

        hideFilter(){
            this.showFilter=false;
        },

        /**
         * lọc nâng cao
         * Create: PTDuyen(28/09/2021)
         */
        async filterMore(){
            try{
                this.showFilter=false;
                this.moreFilter=true;
                await this.getFilterPage();
                if(this.dataList.data.length > 0){
                    // show toast thành công
                    this.listMgs.push({
                        showToast: true,
                        titleMgs: ToastMgs.GetSuccess,
                        iconToast: ToastMessageIcon.success
                    })
                    this.$refs.Filter.focus();
                }
            }
            catch(e){
                console.log(e);
                this.listMgs.push({
                    showToast: true,
                    titleMgs: ToastMgs.GetError,
                    iconToast: ToastMessageIcon.error
                })
            }
        },
        /**
         * Xóa 1 điều kiện lọc
         */
        async removeACondition(){
           try{
               this.checkCondition();
                await this.getFilterPage();
                if(this.dataList.data.length > 0){
                    // focus vào ô filter
                    this.$refs.Filter.focus();
                }
            }
            catch(ex){
                console.log(ex);
                this.listMgs.push({
                    showToast: true,
                    titleMgs: ToastMgs.GetError,
                    iconToast: ToastMessageIcon.error
                })
            }
        },

        /**
         * Xóa điều kiện lọc
         * Create: PTDuyen(29/09/2021)
         */
        async removeFilterMore(){
            try{
                this.moreFilter = false;
                this.checkFilterHead = false;
                this.operation = this.listOrderStatus[3];
                this.dateFilter = this.listTimeToFilter[0];
                await this.getFilterPage();
                if(this.dataList.data.length > 0){
                    // focus vào ô filter
                    this.$refs.Filter.focus();
                }
            }
            catch(ex){
                this.listMgs.push({
                    showToast: true,
                    titleMgs: ToastMgs.GetError,
                    iconToast: ToastMessageIcon.error
                })
            }
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
                    DiscountConditionCode: '', 
                    DiscountConditionTitle:'', 
                    DiscountConditionAmount: '', 
                    DiscountConditionMaxAmount: '',
                    DiscountConditionStartDate: new Date(), 
                    DiscountConditionEndDate: new Date(),
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
                    let listDelete = this.$refs.CommonTable.listChoose.map(item => item.DiscountConditionId);
                    console.log(listDelete);
                    var { data } = await DiscountConditionAPI.deleteMultiObj(listDelete);
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
                    if(this.$refs.DiscountConditionDetail.validateForm() == true){
                        // nếu là thêm mới hoặc nhân bản
                        if((this.crud == CRUD.Post)||(this.crud == CRUD.Copy)){
                            // thực hiện thêm mới
                            // thêm loader
                            this.changeLoader(true);
                            let res = await DiscountConditionAPI.postObj(this.$refs.DiscountConditionDetail.discountCondition);
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
                                if(this.$refs.DiscountConditionDetail.post_save == true){
                                    await this.postSave();
                                }
                                else this.formShow = false;
                            } 
                            // nếu không thêm mới thành công
                            else {
                                //Base.formatObj(this.$refs.DiscountConditionDetail.discountCondition);
                                this.$refs.DiscountConditionDetail.message = res.data.data[0];
                                this.$refs.DiscountConditionDetail.$refs.DiscountConditionCode.classList.add("is-invalid");
                                this.$refs.DiscountConditionDetail.showPopup(PopupState.Duplicate);
                            }
                            // bỏ loader
                            this.changeLoader(false);
                            return;
                        }
                        // nếu là cập nhật
                        else if(this.crud == CRUD.Put){
                            //  thực hiện cập nhật
                            // nếu dữ liệu bị thay đổi
                            if(Base.checkChangeData(this.$refs.DiscountConditionDetail.discountCondition, this.entityDetail, this.crud, 'DiscountConditionCode') == false){
                                // thêm loader
                                this.changeLoader(true);
                                //Base.formatObjToSave(this.$refs.DiscountConditionDetail.discountCondition);
                                let res = await DiscountConditionAPI.putObj(this.$refs.DiscountConditionDetail.discountCondition);
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
                                    if(this.$refs.DiscountConditionDetail.post_save == true){
                                        await this.postSave();
                                    }
                                    else {
                                        this.formShow = false;
                                    }
                                } 
                                // nếu cập nhật thất bại
                                else{
                                    this.$refs.DiscountConditionDetail.message = res.data.data[0];
                                    this.$refs.DiscountConditionCode.classList.add("is-invalid");
                                    this.$refs.DiscountConditionDetail.showPopup(PopupState.Duplicate);
                                }
                                this.changeLoader(false);
                                return;
                            }
                            // nếu dữ liệu không bị thay đổi 
                            else {
                                // nếu ấn cất và thêm
                                if(this.$refs.DiscountConditionDetail.post_save == true){
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
                        titleMgs: Base.stringFormat(ToastMgs.PostError,[obj.DiscountConditionName]),
                        iconToast: ToastMessageIcon.error
                    })
                }
                // nếu là cập nhật lỗi
                else if (this.crud == CRUD.Put){
                    this.listMgs.push({
                        showToast: true,
                        titleMgs: Base.stringFormat(ToastMgs.PutError,[obj.DiscountConditionName]),
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
            this.$refs.DiscountConditionDetail.$nextTick(() => { this.$refs.DiscountConditionDetail.$v.$reset() })
            // đổi hành động về thêm mới
            this.changeCrud(CRUD.Post);
            // discountCondition chuyển về giá trị ban đầu
            this.$refs.DiscountConditionDetail.discountCondition = {
                DiscountConditionCode: '', 
                DiscountConditionName:'', 
                DiscountConditionQuestion: '', 
                DiscountConditionAnswer: ''
            };
            // lấy mã ncc mới
            await this.$refs.DiscountConditionDetail.getNewCode();
            // focus vào ô input object_code
            this.$refs.DiscountConditionDetail.$refs.DiscountConditionCode.focus();
            // chuyển cất và thêm về false
            this.$refs.DiscountConditionDetail.post_save = false;
        },

        settingColumn(){
            this.$refs.CommonTable.setColumn = !this.$refs.CommonTable.setColumn;
        }
    },
}
</script>