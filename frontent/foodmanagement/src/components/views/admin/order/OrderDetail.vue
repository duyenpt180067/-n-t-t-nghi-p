<template>
<div>
    <div class="detail order-detail">
        <!-- loading -->
        <div v-if="loader" :class="{'background-loader':loader}">
            <div :class="{loader:loader}">
                <object :data="loaderUrl"></object>
            </div>
        </div>
        <div class="add-item">
            <div class="flex content-head">
                <div class="content-title">Chi tiết đơn hàng <span>{{orderMerge.Order.OrderName}}</span></div>
            </div>
            <div class="add-entity">
                <div class="flex">
                    <div class="w-3/5">
                        <div class="flex">
                            <div class="general-row w-1/2 m-r-12">
                                <p>Mã đơn hàng <span>*</span></p>
                                <input :readonly="readonly" v-model="orderMerge.Order.OrderCode" ref="OrderCode" id="OrderCode"
                                    type="text" maxlength="20" tabindex="2" 
                                    class="w-full form-control" :class="{ 'is-invalid': $v.orderMerge.Order.OrderCode.$error }"
                                    @blur="$v.orderMerge.Order.OrderCode.$touch()">
                                <div v-if="$v.orderMerge.Order.OrderCode.$error" class="invalid-feedback">
                                    <p v-if="!$v.orderMerge.Order.OrderCode.required">Mã đơn hàng không được để trống.</p>
                                    <p v-if="($v.orderMerge.Order.OrderCode.required) && (!$v.orderMerge.Order.OrderCode.startWithOC)">Mã đơn hàng phải bắt đầu bằng <i>'FC-'.</i></p>
                                </div>
                            </div>
                            <div class="general-row w-1/2">
                                <p>Tên đơn hàng <span>*</span></p>
                                <input :readonly="readonly" v-model="orderMerge.Order.OrderName" ref="OrderName" id="OrderName"
                                    type="text" maxlength="20" tabindex="2" 
                                    class="w-full form-control" :class="{ 'is-invalid': $v.orderMerge.Order.OrderName.$error }"
                                    @blur="$v.orderMerge.Order.OrderName.$touch()">
                                <div v-if="$v.orderMerge.Order.OrderName.$error" class="invalid-feedback">
                                    <p v-if="!$v.orderMerge.Order.OrderName.required">Tên đơn hàng không được để trống.</p>
                                </div>
                            </div>
                        </div>
                        <div class="general-row">
                            <p>Khách hàng <span>*</span></p>
                            <v-combobox class="user-data" 
                                        @getSelect="user_model=$event" 
                                        :combobox_valid="false" 
                                        :placeholder="''" 
                                        :item_text="['UserName', 'FullName', 'Phone', 'Address']" 
                                        :items="users"
                                        :vmodel="user_model"
                                        :multiple="false"
                                        :groupName="['UserName', 'Họ tên', 'Số điện thoại', 'Địa chỉ']"
                                        :display_item="'UserName'"
                                        :readonly="false">
                            </v-combobox>
                        </div>
                        <div class="flex">
                            <div class="general-row w-1/2 m-r-12">
                                <p>Địa chỉ nhận</p>
                                <textarea :readonly="readonly" class="w-full" rows="2" maxlength="255" v-model="orderMerge.Order.Address" placeholder="">
                                </textarea> 
                            </div>
                            <div class="general-row w-1/2">
                                <p>Số điện thoại nhận</p>
                                <input :readonly="readonly" :value="orderMerge.Order.Phone">
                            </div>
                        </div>
                    </div>
                    <div class="w-2/5 m-l-12">
                        <div class="general-row">
                            <p>Trạng thái đơn hàng</p>
                            <v-combobox @getSelect="orderStatusFilter=$event" 
                                        :combobox_valid="false" 
                                        :placeholder="''" 
                                        :item_text="'text'" 
                                        :items="listOrderStatus" 
                                        :vmodel="orderStatusFilter"
                                        :multiple="false">
                            </v-combobox>
                        </div>
                        <div class="general-row">
                            <p class="end" style="font-weight:bold;font-size:20px">Thành tiền</p>
                            <div>{{orderMerge.Order.TotalAmount}}</div>              
                        </div>
                    </div>
                </div>
            </div>
            <div class="order-table">
                <div class="grid-init" style="overflow: auto;">
                    <table>
                        <thead>
                            <tr>
                                <th class="center l-30" style="min-width:40px;">#</th>
                                <th style="min-width: 180px;">Sản phẩm</th>
                                <th style="min-width: 138px;">Kích thước</th>
                                <th style="min-width: 155px;">Topping</th>
                                <th style="min-width: 105px;" class="end">Số phần</th>
                                <th style="min-width: 105px;" class="end">Đơn giá</th>
                                <th style="min-width: 105px;" class="end">Giảm giá</th>
                                <th style="min-width: 105px;" class="end">Giá tiền</th>
                                <th style="min-width: 30px;">XÓA</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(itemDetail, index) in OrderDetails" :key="index" :class="{'list-input':crudDetail!='read', 'read-input':crudDetail=='read'}">
                                <td class="center l-30">{{index+1}}</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td class="end"><input :readonly="readonly" type="text" style="text-align: end;" v-model="itemDetail.Amount" @keyup="formatNumber(index)"></td>
                                <td class="r-30">
                                    <div class="delete-icon other-icon logo-icon pointer" @click="removeDetail(index)"></div>
                                </td>
                            </tr>
                            <!-- <tr v-for="(itemDetail, index) in OrderDetails" :key="index" :class="{'list-input':crudDetail!='read', 'read-input':crudDetail=='read'}">
                                <td class="center l-30">{{index+1}}</td>
                                <td>
                                    <v-combobox @getSelect="itemDetail.Food=$event;" 
                                                :combobox_valid="false" 
                                                :placeholder="''" 
                                                :item_text="['FoodCode','FoodName']" 
                                                :items="foodList"
                                                :display_item="'FoodName'" 
                                                :groupName="['Mã sản phẩm','Tên sản phẩm']"
                                                :vmodel="itemDetail.Food"
                                                :readonly="readonly" 
                                                :multiple="false">
                                    </v-combobox>
                                </td>
                                <td>
                                    <v-combobox @getSelect="sizeModel=$event;" 
                                                :combobox_valid="false" 
                                                :placeholder="''" 
                                                :item_text="['SizeCode','SizeName']" 
                                                :items="itemDetail.ListFoodDetail"
                                                :display_item="'SizeName'" 
                                                :groupName="['Mã kích thước','Kích thước']"
                                                :vmodel="sizeModel"
                                                :readonly="readonly" 
                                                :multiple="false">
                                    </v-combobox>
                                </td>
                                <td>
                                    <v-combobox @getSelect="getSelect('topping_model', $event)" 
                                                :combobox_valid="false" 
                                                :placeholder="''" 
                                                :item_text="['ToppingCode','ToppingName', 'ToppingAmount']" 
                                                :items="toppings" 
                                                :vmodel="topping_model"
                                                :multiple="true"
                                                :groupName="['Mã Topping','Tên Topping', 'Giá Topping']"
                                                :display_item="'ToppingName'"
                                                :readonly="readonly">
                                    </v-combobox>
                                </td>
                                <td class="end"><input :readonly="readonly" type="text" style="text-align: end;" v-model="itemDetail.Amount" @keyup="formatNumber(index)"></td>
                                <td class="r-30">
                                    <div class="delete-icon other-icon logo-icon pointer" @click="removeDetail(index)"></div>
                                </td>
                                <td class="w-30" style="right:-1px; min-width: 31px;"></td>
                            </tr>
                            <tr class="tr-input input-detail" v-if="showInputDetail">
                                <td class="center l-30">{{OrderDetails.length+1}}</td>
                                <td>
                                    <v-combobox @getSelect="orderDetail.Food=$event;" 
                                                :combobox_valid="false" 
                                                :placeholder="''" 
                                                :item_text="['FoodCode','FoodName']" 
                                                :items="foodList"
                                                :display_item="'FoodName'" 
                                                :groupName="['Mã sản phẩm','Tên sản phẩm']"
                                                :vmodel="orderDetail.Food"
                                                :readonly="readonly" 
                                                :multiple="false">
                                    </v-combobox>
                                </td>
                                <td>
                                    <v-combobox @getSelect="sizeModel=$event;" 
                                                :combobox_valid="false" 
                                                :placeholder="''" 
                                                :item_text="['SizeCode','SizeName']" 
                                                :items="orderDetail.ListFoodDetail"
                                                :display_item="'SizeName'" 
                                                :groupName="['Mã kích thước','Kích thước']"
                                                :vmodel="sizeModel"
                                                :readonly="readonly" 
                                                :multiple="false">
                                    </v-combobox>
                                </td>
                                <td>
                                    <v-combobox @getSelect="topping_model=$event;" 
                                                :combobox_valid="false" 
                                                :placeholder="''" 
                                                :item_text="['ToppingCode','ToppingName', 'ToppingAmount']" 
                                                :items="toppings"
                                                :display_item="'ToppingName'" 
                                                :groupName="['Mã Topping','Tên Topping', 'Giá']"
                                                :vmodel="topping_model"
                                                :readonly="readonly" 
                                                :multiple="true">
                                    </v-combobox>
                                </td>
                                <td class="end"><input :readonly="readonly" type="text" style="text-align: end;" v-model="orderDetail.Amount" @keyup="formatNumber(index)"></td>
                                <td class="r-30">
                                    <div class="delete-icon other-icon logo-icon pointer" @click="removeDetail(index)"></div>
                                </td>
                                <td class="w-30" style="right:-1px; min-width: 31px;"></td>
                            </tr> -->
                        </tbody>
                    </table>
                </div>
                <div class="table-actions flex">
                    <div class="table-action pointer" @click="addDetail();showInputDetail=true">Thêm dòng</div>
                    <div class="table-action pointer" @click="removeDetail('all')">Xóa hết dòng</div>
                </div>
            </div>
        </div>
        <div class="form-footer flex" :class="{'form-right':!fadeleft}">
            <div class="admin-btn-normal pointer btn-delete" tabindex="18" @click="btnXForm()" @keyup.enter="btnXForm()">Hủy</div>
            <div class="btn-save-all flex-center" v-if="crudDetail != 'read'">
                <div class="admin-btn-normal btn-save pointer" tabindex="16" @click="crudObject()" @keyup.enter="crudObject()">Cất</div>
                <!-- <div class="btn-footer btn-save-more" tabindex="17" @click="post_save = true;crudObject()" @keyup.enter="post_save = true,crudObject()">Cất và Thêm</div> -->
                <div class="admin-btn-normal admin-btn-primary pointer" v-if="!readonly" 
                        @click="post_save=true,crudObject()"
                        v-shortkey="['ctrl','shift','s']" @shortkey="post_save = true,crudObject()"
                        v-tooltip.top="{content:'Cất và thêm (Ctrl+ Shift + S)', classes:'tooltip', offset: '5px'}">Cất và thêm</div>
            </div>
            <div v-if="crudDetail == 'read'" class="btn-save-all">
                <div class="admin-btn-normal admin-btn-primary pointer" tabindex="16" @click="changeCrud('put')" @keyup.enter="changeCrud('put')">Sửa</div>
            </div>
        </div>
        <div>
            <div v-for="(mgs, index) in listMgs" :key="index">
                <toast-message :titleMgs="mgs.titleMgs" :iconToast="mgs.iconToast" v-if="mgs.showToast" @deleteToast="deleteToast(index)"></toast-message>
            </div>
        </div>
    </div>
    <add-order-detail v-if="showDetail" 
                    @closeForm="showDetail=false;" 
                    :crud="crudDetail"
                    @btnXForm="showDetail=false"
                    @changeCrud="changeCrud"
                    ref="CategoryDetail"></add-order-detail>
</div>
</template>

<style>
.order-detail{
    position: absolute;
    width: 100%;
    overflow: auto;
    height: calc(100% - 140px);
}

.order-detail>.add-item{
    display: block;
    padding: 0 35px;
    overflow-x: hidden;
}


.order-detail .add-item .content-head{
    padding-left: 0;
}

.order-detail .add-item .table-actions {
    margin-top: 10px;
    padding: 10px;
    height: 46px;
}

.order-detail .add-item .table-action {
    background: #fff;
    border: 1px solid #8d9096;
    border-radius: 3px!important;
    padding: 2px 20px!important;
    margin-right: 10px;
    font-weight: 700;
}

</style>

<script>

import {
    required,
} 
from "vuelidate/lib/validators";
import OrderAPI from '../../../../api/component/order/OrderAPI';
import UserAPI from '../../../../api/component/user/UserAPI';
// import ToppingAPI from '../../../../api/component/additional-element/ToppingAPI';   
// import FoodAPI from '../../../../api/component/food/FoodAPI';
import { PopupState } from '../../../../base/Resources.js';
import Base from '../../../../base/Base.js';
import { Actions, OrderStatus } from '../../../../base/vi/Resources.js';
import VCombobox from '@/components/base/VCombobox';
import ToastMessage from '@/components/base/ToastMessage';
import AddOrderDetail from './AddOrderDetail.vue';
export default {
    name:'OrderDetail',
    components:{
        VCombobox,
        ToastMessage,
        AddOrderDetail
    },
    props:{
        fadeleft: Boolean,
    },
    async created() {
        try {
            // hiển thị loader
            this.changeLoader(true);
            this.orderCode = this.$route.params.code;
            if(this.orderCode){
                this.orderMerge = await OrderAPI.getOrderByCode(this.orderCode);
            }
            else {
                this.orderCode = this.$route.params.copyCode;
                if(this.orderCode){
                    this.orderMerge = await OrderAPI.getOrderByCode(this.orderCode);
                }
            }
            let userData = await UserAPI.getFilterPaging({UserFilter:null,IsEmployee:false, PageNumber:null, PageSize:null});
            this.users = userData.data;
            // let foodData = await FoodAPI.getFilterPaging({FoodFilter:null,CategoryId:null,PageNumber:null,PageSize:null});
            // this.foodList = foodData.data;
            // // let sizeData = await SizeAPI.getFilterPaging({SizeFilter:null,SizeStatus:null});
            // // this.sizes = sizeData.data;
            // let toppingData = await ToppingAPI.getFilterPaging({ToppingFilter:null,ToppingAmountMin:null,ToppingAmountMax:null,PageNumber:null,PageSize:null});
            // this.toppings = toppingData.data;
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
            readonly: false,
            popShow: false,
            showInputDetail:true,
            // action post, put, copy
            action: "",
            // message trong popup
            message:"",
            // hiển thị loading
            loader:false,
            loaderUrl: require('../../../../assets/lib/img/common/loading.svg'),
            // toast
            mgs: {
                titleMgs: "",
                showToast: false,
                iconToast: "",
            },
            listMgs: [],
            post_save: false,
            // id của ô input invalid đầu tiên
            idInvalid:"",
            // foodList:[],
            orderMerge: {
                Order: {
                    OrderCode:'',
                    OrderName:'',
                },
                OrderDetails: []
            },
            // orderDetail:{
            //     OrderId: '',
            //     Size: {
            //         SizeId: '',
            //         SizeName: '',
            //     },
            //     Toppings:[]
            // },
            // Order: {
            //     OrderCode:'',
            //     OrderName:'',
            //     Toppings:[]
            // },
            OrderDetails: [],
            // orderCode: "",
            // dataDetailList: [],
            // objectDetailName: 'OrderDetail',
            // displayDetailName:'Chi tiết đơn hàng',
            // formShow: false,
            crudDetail: '',
            showDetail:false,
            actions: Actions,
            user_model:{
                UserName:'',
                UserCode:'',
                FullName:'',
                Phone:'',
                Address:''
            },
            listOrderStatus:OrderStatus,
            orderStatusFilter:OrderStatus[2],
            users:[],
            // sizeModel:{
            //     SizeName: '',
            //     SizeCode: ''
            // },
            // foodDetails:[],
            // toppings:[],
            // topping_model:[]
        }
    },
    // rule của các input
    validations: {
        orderMerge:{
            Order: {
                OrderCode: {
                    required,
                    startWithOC(){
                        if(this.orderMerge.Order.OrderCode.startsWith('OC-')) return true;
                        else return false;
                    }
                },
                OrderName: {
                    required,
                },
            }
        }
    },
    methods:{

        /**
         * validate khi nhấn click
         * Create: PTDuyen(18/08/2021)
         */
        validateForm() {
            this.idInvalid = "";
            this.$v.$touch();
            // nếu dữ liệu không hợp lệ
            if (this.$v.$invalid) {
                // console.log(this.$v.order);
                for(let key in this.order){
                    if(this.$v.order[key]){
                        if(this.$v.order[key].$error == true){
                            this.idInvalid = key;
                            this.$emit('showPopup', PopupState.Invalid);
                        }
                    }
                }
                return false;
            }
            // nếu dữ liệu hợp lệ 
            else {
                return true;
            }
        },

        /**
         * Đóng popup
         * Create: PTDuyen(17/08/2021)
         */
        closePop(action) {
            if(action == PopupState.Invalid){
                this.$refs[this.idInvalid].focus();
            }
            else if(action == PopupState.Duplicate){
                this.$refs.OrderDetail.focus();
                this.$refs.OrderDetail.classList.remove("is-invalid");
            }
            this.popShow = false;
        },
        /**
         * Đóng form bằng nút hủy hoặc x(khi có dữ liệu thay đổi)
         * Create: PTDuyen(11/09/2021)
         */
        btnXForm(){
            this.$router.go(-1);
        },
        /**
         * gọi đến hàm closeForm của orderList
         * Create: PTDuyen(11/09/2021)
         */
        closeForm() {
            this.$router.go(-1);   
        },

        /**
         * Đổi trạng thái crud
         * Create: PTDuyen(10/09/2021)
         */
        changeCrud(action){
            this.crudDetail = action;
        },
        /**
         * delete toast message
         * Create: PTDuyen(13/09/2021)
         */
        deleteToast(index) {
            this.listMgs.splice(index, 1);
        },
        async crudObject(){
            if(this.$refs.ImageURL.files && this.$refs.ImageURL.files[0]){
                let formData = new FormData();
                formData.append('file', this.$refs.ImageURL.files[0])
                this.orderMerge.Order.ImageURL = await OrderAPI.uploadFile(formData);
                this.$emit('crudObject', this.order);
            }
        },

        getFile(imgSelector, event){
            Base.getFile(imgSelector, event);
        },
        deleteFile(imgSelector){
            this.orderMerge.Order.ImageURL = '';
            this.$el.querySelector(imgSelector).setAttribute('src', '');
        },
        changeLoader(value){
            this.loader = value;
        },

        formatNumber(index){
            if(typeof(index)=='number'){
                this.orderMerge.OrderDetails[index].Amount = Base.formatNumber(this.orderMerge.OrderDetails[index].Amount);
            }
            else {
                // console.log(this.unitMoney_model);
                this.orderDetail.Amount = Base.formatNumber(this.orderDetail.Amount);
            }
        },

        /**
         * Xóa detail khoi list
         * Create:PTDuyen(19/10/2021)
         */
        removeDetail(index){
            if(typeof(index)=='number'){
                this.orderMerge.OrderDetails.splice(index,1);
            }
            else {
                if(index == 'all'){
                    this.orderMerge.OrderDetails = [];
                }
                else {
                    this.showInputDetail = false; 
                    this.orderDetail = {
                        Amount: '',
                        Size: {
                            SizeId: '',
                            SizeName: '',
                        }
                    }
                }
            }
            if(this.orderMerge.OrderDetails.length == 0){
                this.showInputDetail = true; 
                    this.orderDetail = {
                        Amount: '',
                        Size: {
                            SizeId: '',
                            SizeName: '',
                        }
                    }
            }
        },
        /**
         * Thêm detail vào list
         * Create:PTDuyen(19/10/2021)
         */
        addDetail(){
            this.showDetail = true;
            // console.log("ok");
            // this.orderMerge.OrderDetails = this.orderMerge.OrderDetails.filter(e => e != this.orderDetail);
            // this.orderMerge.OrderDetails.push(this.orderDetail);
            // this.orderDetail = Object.assign({}, this.orderMerge.OrderDetails[this.orderMerge.OrderDetails.length-1]);
            // console.log(this.orderMerge.OrderDetails);
        },
    },
}
</script>

<style>

</style>