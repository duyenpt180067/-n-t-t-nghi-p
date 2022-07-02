<template>
<div class="content-init">
    <div class="flex content-head">
        <div class="content-title">Danh sách đơn hàng</div>
        <div class="btn flex-center">
            <!-- <button class="admin-btn-primary add-btn" @click="openForm(null, 'post')">
                <i class="fa fa-plus"></i>
                Thêm mới
            </button> -->
        </div>
    </div>
    <div class="content-action flex">
        <div class="action-left flex">
            <div class="content-filter flex">
                <input placeholder="Nhập tên đơn hàng, mã đơn hàng để tìm kiếm" 
                        type="text" 
                        ref="Filter" 
                        id="filter"
                        v-model="keyword"
                        v-tooltip.bottom-end="{offset: '5px', content:'Nhấp Enter để tìm kiếm', classes:'tooltip'}" @keyup.enter="getFilterPage()">
                <div class="content-filter-search logo-icon other-icon pointer"></div>
            </div>
            <div class="filter-pop" style="position:relative;">
                <button class="btn-filter-bar flex-center" type="button" id="btnFilterToggle" @click="showFilter=!showFilter;">
                    <i class="fa fa-filter"></i>
                    <span>Lọc</span>
                </button>
                <div class="pop-filter" :class="{'show-filter':showFilter, 'h-0':!showFilter}">
                    <div class="other-icon logo-icon btn-close pointer" id="btn-close" style="position: absolute;right: 5px;top: 5px;"
                            @click="showFilter=false;">
                    </div>
                    <div class="pop-init">
                        <!-- <div class="pop-info1 m-b-3">
                            <p class="m-b-3">Khách hàng</p>
                            <v-combobox class="user-data" 
                                        @getSelect="getSelect('user_model', $event)" 
                                        :combobox_valid="false" 
                                        :placeholder="''" 
                                        :item_text="['UserName','FullName', 'Phone', 'Address']" 
                                        :items="users"
                                        :vmodel="user_model"
                                        :multiple="true"
                                        :groupName="['UserName','Họ tên', 'Số điện thoại', 'Địa chỉ']"
                                        :display_item="'UserName'"
                                        :readonly="false">
                            </v-combobox>
                        </div> -->
                        <div class="pop-info1">
                            <div class="m-b-3">
                                <p class="m-b-3">Trạng thái hóa đơn</p>
                                <v-combobox @getSelect="orderStatusFilter=$event" 
                                            :combobox_valid="false" 
                                            :placeholder="''" 
                                            :item_text="'text'" 
                                            :items="listOrderStatus" 
                                            :vmodel="orderStatusFilter"
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
                  :actions="actions" :checkPaging="false" :displayName="displayName" @changeCrud="changeCrud" @openForm="openForm" @getFilterPage="getFilterPage"
                  @crudObject="crudOrder" ref="CommonTable" :noAction="true">
    </common-table>
    <order-user-detail @crudObject="crudObject" v-if="formShow && orderRead && orderRead.OrderId" @btnXForm="btnXForm" :orderRead="orderRead"></order-user-detail>
</div>
</template>

<script>
import OrderAPI from '../../../../api/component/order/OrderAPI'
import UserAPI from '../../../../api/component/user/UserAPI'
import CommonTable from '../../../base/CommonTable.vue';
import VDatepicker from '@/components/base/VDatepicker';
import VCombobox from '@/components/base/VCombobox';
import { Actions, ListOrderStatus, ListTimeToFilter } from '../../../../base/vi/Resources.js';
import { CRUD, PopupState, ToastMessageIcon } from '../../../../base/Resources.js';
import { ToastMgs } from '../../../../base/vi/Resources.js';
import Base from '../../../../base/Base.js';
import OrderUserDetail from '../../user/account/OrderUserDetail.vue';
export default {
    components: { CommonTable, VDatepicker, VCombobox, OrderUserDetail },
    name:'OrderAdmin',
    props:{
        loader: Boolean,

    },
    async created() {
        try {
            // hiển thị loader
            this.changeLoader(true);
            this.layoutConfig = await OrderAPI.getLayoutConfig();
            let userData = await UserAPI.getFilterPaging({UserFilter:null,IsEmployee:false, PageNumber:null, PageSize:null});
            this.user_model = this.users;
            this.users = this.users.concat(userData.data);
            this.dataList = await OrderAPI.getFilterPaging(this.paramGet);
            if(this.dataList && this.dataList.data && this.dataList.data.length > 0){
                this.dataList.data.forEach(el => {
                    el.OrderStatusText = ListOrderStatus.filter(ot => ot.value == el.OrderStatus)[0].text;
                })
            }
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
            objectName: 'Order',
            displayName:'Đơn hàng',
            paramGet: {
                OrderStatus: null,
                OrderFilter: null,
                CreatedDateMin: null,
                CreatedDateMax: null,
                ListUserName:null,
                PageNumber: 1,
                PageSize: 20
            },
            // các chức năng
            actions: Actions,
            keyword:"",
            entityDetail: {
                OrderCode: '', 
                OrderName:'', 
                Descriptions: '', 
                OrderImage: ''
            },
            formShow: false,
            crud:'post',
            listMgs:[],
            // danh sách category 
            categoryList: [{
                CategoryId:'',
                CategoryName: 'Tất cả danh mục'
            }],
            // loc theo thoi gian
            listTimeToFilter: ListTimeToFilter,
            dateFilter: ListTimeToFilter[0],
            showFilter:false,
            listOrderStatus:ListOrderStatus,
            orderStatusFilter:ListOrderStatus[0],
            user_model:[],
            users:[{
                UserName:'Tất cả khách hàng',
                UserCode: '',
                FullName: '',
                Address: '',
                Phone: '',
            }],
            orderRead: null,
            showDeleteMulti: false,
        }
    },

    methods:{
        settingColumn(){
            this.$refs.CommonTable.setColumn = !this.$refs.CommonTable.setColumn;
        },

        crudOrder(action, obj){
            if(action == CRUD.Read){
                this.orderRead = obj;
                this.formShow = true;
            }
            else {
                console.log("delete");
            }
        },

        /**
         * Đặt lại điều kiện lọc
         * Create: PTDuyen(29/09/2021)
         */
        resetFilter(){
            this.dateFilter = this.listTimeToFilter[0];
            this.orderStatusFilter = this.listOrderStatus[0];
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
                this.orderStatusFilter = this.listOrderStatus[0];
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

        changeLoader(value){
            this.$emit('changeLoader', value);
        },

        async getFilterPage(){
            this.paramGet = {
                OrderStatus: this.orderStatusFilter.value == -1 ? null : this.orderStatusFilter.value,
                OrderFilter: this.keyword,
                CreatedDateMin: this.dateFilter.value.startDate,
                CreatedDateMax: this.dateFilter.value.endDate,
                ListUserName:null,
                PageNumber: this.$refs.CommonTable.currentPage,
                PageSize: this.$refs.CommonTable.pageSize
            };
            this.dataList = await OrderAPI.getFilterPaging(this.paramGet);
            if(this.dataList && this.dataList.data && this.dataList.data.length > 0){
                this.dataList.data.forEach(el => {
                    el.OrderStatusText = ListOrderStatus.filter(ot => ot.value == el.OrderStatus)[0].text;
                })
            }
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

        /**
         * Mở form
         * Create: PTDuyen(09/09/2021)
         */
        openForm(data, action) {
            this.formShow = true;
            this.crud = action;
            if ((this.crud == CRUD.Read)||(this.crud == CRUD.Put)||this.crud == CRUD.Copy) {
                this.orderRead = Object.assign({}, data);
            }
            else {
                this.orderRead = {
                    Order: {
                        Address:"",
                        Phone: "",
                        OrderStatus: 0,
                    },
                    OrderDetails: [],
                };
            }
        },

        showPopup(action){
            this.$refs.CommonTable.showPopup(action);
        },

        btnXForm(){
            this.formShow = false;
        },

        /**
         * Thực hiện các hành động thêm sửa xóa
         * Create: PTDuyen(28/09/2021)
         */
        async crudObject(obj){
            try{
                this.keyword = "";
                // cập nhật trạng thái đơn hàng
                // thêm loader
                this.changeLoader(true);
                //Base.formatObjToSave(this.$refs.OrderDetail.order);
                let res = await OrderAPI.putStatusOrder(obj);
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
                    this.$store.dispatch('handleChangeOrder', obj);
                    console.log(this.$store.state.notiOrderStatus);
                    // tải lại dữ liệu
                    await this.getFilterPage();
                    if(this.post_save == true){
                        await this.postSave();
                    }
                    else {
                        this.formShow = false;
                    }
                } 
                // nếu cập nhật thất bại
                else{
                    console.log(res);
                    this.message = res.data.Data[0];
                    this.$refs.object_code.classList.add("is-invalid");
                    this.showPopup(PopupState.Duplicate);
                }
                this.changeLoader(false);
                return;
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
                        titleMgs: Base.stringFormat(ToastMgs.PostError,[obj.OrderName]),
                        iconToast: ToastMessageIcon.error
                    })
                }
                // nếu là cập nhật lỗi
                else if (this.crud == CRUD.Put){
                    this.listMgs.push({
                        showToast: true,
                        titleMgs: Base.stringFormat(ToastMgs.PutError,[obj.OrderName]),
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
            this.$nextTick(() => { this.$v.$reset() })
            // đổi hành động về thêm mới
            this.changeCrud(CRUD.Post);
            // provider chuyển về giá trị ban đầu
            this.$refs.OrderDetail.order = {};
            // lấy mã ncc mới
            await this.getNewCode();
            // focus vào ô input object_code
            this.$refs.OrderDetail.$refs.OrderCode.focus();
            // chuyển cất và thêm về false
            this.post_save = false;
        },

        /**
         * lấy ra phần tử được select
         * Create: PTDuyen(12/09)
         */
        getSelect(model, item){
            this[model] = item;
            switch (model){
                case 'user_model':
                    this.paramGet.ListUserName = item.map(user => user.UserName);
                    break;
                case 'country_model':
                    this.foodMerge.Food = item;
                    break;
                default:
                    break;
            }
        },
    },
}
</script>

<style>
.pop-filter p {
    font-family: GoogleSans-Bold;
}

.pop-filter {
    position: absolute;
    z-index: 5;
    background: #fff;
    /* border: 1px solid #babec5; */
    border-radius: 3px;
    transition: .2s;
    width: 538px;
    font-size: 12px;
    top: 35px;
    /* right: calc(-100% + 23px); */
    opacity: 0;
    height: 0;
}

.pop-filter .pop-init {
    overflow: hidden;
    transition: .2s;
    height: 0;
}

.pop-footer {
    font-size: inherit;
    position: relative;
    margin-top: 20px;
}

.show-filter {
    opacity: 1;
    /* height: 300px; */
}

.show-filter .pop-init {
    background-color: #fff;
    border: 1px solid var(--gray-color);
    padding: 20px 23px;
    height: fit-content;
}

</style>
