<template>
<div class="account">
    <div class="account-detail flex m-b-20">
        <div class="w-1/2 m-r-12 account-detail-init">
            <div class="form-input">
                <p>Tên tài khoản</p>
                <input type="text" name="UserName" id="UserName" placeholder="Tên tài khoản" 
                        class="w-full" :readonly="readonly"
                        v-model="user.userName" ref="UserName"
                        :class="{ 'is-invalid': $v.user.userName.$error }"
                        @blur="$v.user.userName.$touch()">
                <div v-if="$v.user.userName.$error" class="invalid-feedback">
                    <p v-if="!$v.user.userName.required">Tên tài khoản không được để trống!</p>
                </div>
            </div>
            <div class="btn-account flex">
                <div class="m-r-12 pointer" v-if="readonly" @click="readonly=false">Cập nhật tài khoản</div>
                <div class="pointer m-r-12" v-if="!readonly" @click="updateUser">Lưu</div>
                <div class="pointer" @click="logout">Đăng xuất</div>
            </div>
            <!-- <div class="form-input">
                <input type="password" name="Pass" id="Pass" placeholder="Mật khẩu"
                        v-model="user.pass" ref="Pass" 
                        :class="{ 'is-invalid': $v.user.pass.$error }"
                        @blur="$v.user.pass.$touch()">
                <div v-if="$v.user.pass.$error" class="invalid-feedback">
                    <p v-if="!$v.user.pass.required">Mật khẩu không được để trống!</p>
                    <p v-if="$v.user.pass.required && !$v.user.pass.checkPassword">Mật khẩu chứa ít nhất 1 chữ cái thường, 1 chữ cái viết hoa, 1 chữ số và 1 ký tự đặc biệt!</p>
                </div>
            </div> -->
        </div>
        <div class="w-1/2 account-detail-init">
            <div class="form-input m-b-12"> 
                <p>Số điện thoại</p>
                <input name="Phone" id="Phone" placeholder="Số điện thoại" 
                        class="w-full" :readonly="readonly"
                        v-model="user.phone"  ref="Phone"
                        :class="{ 'is-invalid': $v.user.phone.$error }"
                        @blur="$v.user.phone.$touch()">
                <div v-if="$v.user.phone.$error" class="invalid-feedback">
                    <p v-if="!$v.user.phone.required">Số điện thoại không được để trống!</p>
                    <p v-if="$v.user.phone.required && !$v.user.phone.checkPhoneNumber">Số điện thoại không hợp lệ!</p>
                </div>
            </div>
            <div class="form-input">
                <p>Địa chỉ</p>
                <textarea type="text" :readonly="readonly" class="w-full" name="Address" ref="Address" id="Address" placeholder="Địa chỉ" v-model="user.address" rows="2"></textarea>
            </div>
        </div>
    </div>  
    <div class="line"></div>
    <div class="orders">
        <div class="order-status flex-center">
            <div class="w-1/5 center pointer m-r-12 order-status-item" :class="{'active':orderStatus=='Processing'}"><div class="status" @click="loadOrders('Processing')">Chờ xử lý <div class="line-active"></div></div></div>
            <div class="w-1/5 center pointer m-r-12 order-status-item" :class="{'active':orderStatus=='Accept'}"><div class="status" @click="loadOrders('Accept')">Quán đang chuẩn bị <div class="line-active"></div></div></div>
            <div class="w-1/5 center pointer m-r-12 order-status-item" :class="{'active':orderStatus=='WaitingToTake'}"><div class="status" @click="loadOrders('WaitingToTake')">Tài xế đang đến quán <div class="line-active"></div></div></div>
            <div class="w-1/5 center pointer m-r-12 order-status-item" :class="{'active':orderStatus=='Delivering'}"><div class="status" @click="loadOrders('Delivering')">Đang giao hàng <div class="line-active"></div></div></div>
            <div class="w-1/5 center pointer m-r-12 order-status-item" :class="{'active':orderStatus=='Complete'}"><div class="status" @click="loadOrders('Complete')">Hoàn thành <div class="line-active"></div></div></div>
            <div class="w-1/5 center pointer order-status-item" :class="{'active':orderStatus.toString().includes('Delete') || orderStatus.toString().includes('Reject')}" @click="loadOrders('Delete')"><div class="status">Đã hủy <div class="line-active"></div></div></div>
        </div>
        <div class="list-order m-t-20">
            <common-table :oneAction="true" :layoutConfig="layoutConfig" :dataList="orders" :objectName="'Order'"
                          :actions="actions" :checkPaging="false" :displayName="'Đơn hàng'" @actionOne="actionOne"
                          @crudObject="crudOrder" ref="CommonTable">
            </common-table>
        </div>
    </div>
    <order-user-detail v-if="showDetail && orderRead && orderRead.OrderId" @btnXForm="btnXForm" :orderRead="orderRead"></order-user-detail>
    <comment-food v-if="showComment && orderRead && orderRead.OrderId" @btnXForm="btnXForm" :orderRead="orderRead"></comment-food>
</div>
</template>

<script>
import {
    required,
} 
from "vuelidate/lib/validators";
import OrderAPI from '../../../../api/component/order/OrderAPI';
import UserAPI from '../../../../api/component/user/UserAPI';
import Base from '../../../../base/Base';
import { CRUD, PopupState } from '../../../../base/Resources';
import { OrderActions, OrderStatus } from '../../../../base/vi/Resources';
import CommonTable from '../../../base/CommonTable.vue';
import CommentFood from './CommentFood.vue';
import OrderUserDetail from './OrderUserDetail.vue';
import { mapState } from 'vuex'
const regexPhone =/^[+]?[(]?[0-9]{2,3}[)]?[-\s.]?[2-9]{1}\d{2}[-\s.]?[0-9]{4,6}$/;
export default {
  components: { CommonTable, OrderUserDetail, CommentFood },
    name:'Account',
    // rule của các input
    validations: {
        user: {
            userName: {
                required,
            },
            phone: {
                required,
                checkPhoneNumber(){
                    if(regexPhone.test(this.user.phone.trim()) == true){
                        return true;
                    }
                    else return false;
                }
            },
        }
    },
    computed: mapState(['notiOrderStatus', 'orderChange']),
    watch:{
        notiOrderStatus(newValue, oldValue){
            console.log(newValue, oldValue);
        },
        orderChange(){
            console.log(this.orderChange);
        }
    },
    data(){
        return {
            user: null,
            orderStatus: OrderStatus.Processing.textEnglish,
            orders: [],
            readonly: true,
            actions:OrderActions,
            layoutConfig: [],
            showDetail: false,
            orderRead: null,
            showComment: false,
            popMessage:"",
        }
    },
    async created() {
        this.user = JSON.parse(localStorage.getItem("user"));
        if(!this.user){
            this.$router.push({path: '/login'});
        }
        this.layoutConfig = await OrderAPI.getLayoutConfig();
        let layoutValue = JSON.parse(this.layoutConfig.LayoutValue);
        layoutValue.forEach(element => {
            if(element.DataBind=="OrderName"||element.DataBind=="OrderReason"||element.DataBind=="OrderStatusText"){
                element.State = false;
            }
        });
        this.layoutConfig.LayoutValue = JSON.stringify(layoutValue);
        await this.loadOrders(this.orderStatus);
    },
    methods:{
        async loadOrders(item){
            let statusFilter = null;
            if(item == OrderStatus.Complete.textEnglish){
                this.orderStatus = OrderStatus[item].textEnglish;
                statusFilter = OrderStatus[item].value;
                this.actions = [
                    { text: 'Chi tiết', value: CRUD.Read },
                    { text: 'Đánh giá', value: CRUD.Rate },
                ];
            }
            else if(item != OrderStatus.Delete.textEnglish){
                this.orderStatus = OrderStatus[item].textEnglish;
                statusFilter = OrderStatus[item].value;
                if(item != OrderStatus.Processing.textEnglish){
                    this.actions = [OrderActions[0]];
                }
                else {
                    this.actions = OrderActions;
                }
            }
            else {
                this.orderStatus = [OrderStatus.Reject.textEnglish, OrderStatus.Delete.textEnglish];
                statusFilter = OrderStatus.Reject.value + "," + OrderStatus.Delete.value;
                this.actions = [OrderActions[0]];
            }
            this.orders = await OrderAPI.getFilterPaging({
                ListUserName: this.user.userName,
                OrderFilter: null,
                OrderStatus: statusFilter,
                CreatedDateMin: null,
                CreatedDateMax: null,
                PageNumber: null,
                PageSize: null,
            })
        },
        actionOne(){

        },
        async updateUser(){
            let orgUser = JSON.parse(localStorage.getItem("user"));
            if(orgUser){
                let orUser = {};
                for (const key in this.user) {
                    let pasKey = key[0].toUpperCase() + key.substring(1, key.length);
                    orUser[pasKey] = this.user[key];
                }
                let res = await UserAPI.updateUser(orUser, orgUser.userName);
                if(res.data&&res.data.resultCode=="FM-Success"){
                    this.readonly = true;
                    localStorage.setItem('user', JSON.stringify(this.user));
                    setTimeout(function(){
                        Base.logout();
                    }, 86400000)
                }
                else {
                    this.$refs.CommonTable.message = res.data.data[0];
                    this.$refs.CommonTable.showPopup(PopupState.Duplicate);
                }
            }
            else {
                this.$router.push({path: '/login'})
            }
        },
        logout(){
            Base.logout();
            this.$router.push({ path: '/login' });
        },
        btnXForm(){
            this.showDetail = false;
            this.showComment = false;
        },
        crudOrder(action, obj){
            if(action == CRUD.Read){
                this.orderRead = obj;
                this.showDetail = true;
            }
            else if(action == CRUD.Rate){
                this.orderRead = obj;
                this.showComment = true;
            }
            else {
                console.log("delete");
            }
        },
        unwatch(){
            return this.$store.watch(
                (state, getters) => getters.notiOrderStatus,
                (newValue, oldValue) => {
                    console.log(`Updating from ${oldValue} to ${newValue}`);
                }
            )
        }
    },
    beforeDestroy() {
        this.unwatch();
    },
}
</script>

<style>
.account{
    padding: 50px 20px;
}
.account .account-detail input:focus{
    border-style: none none dotted none !important;
}
.account .account-detail input:read-only{
    background: #fff;
}
.account .account-detail input,
.account .account-detail textarea{
    border-radius: 3px;
    border: 1px solid var(--gray-color);
    border-style: none none dotted none;
}
.account .account-detail input{
    padding: 0 10px;
    height: 36px;
}
.account .account-detail-init{
    position: relative;
}
.account .btn-account{
    position: absolute;
    bottom: 12px;
    font-size: 15px;
}
.account .btn-account>div{
    transition: .2s;
    border-radius: 3px;
    border: 1px solid var(--gray-color);
    padding: 5px 10px;
}
.account .btn-account>div:hover{
    color: #f2765e;
}
.account .line{
    padding: 30px 0;
    border-top: 1px solid var(--gray-color);
}
.account .orders .order-status .order-status-item > .status{
    width: fit-content;
    margin: auto;
    transition: .2s;
}
.account .orders .order-status .line-active{
    width: 0;
    height: 3.5px;
    background-color: var(--primary-color);
    transition: .2s;
}
.account .orders .order-status .order-status-item > .status:hover .line-active,
.account .orders .order-status .active .line-active{
    width: 100%;
}
.account .scroll-body{
    height: 100%;
    margin-bottom: 20px;
}
.account .scroll-body .no-data{
    padding: 15px 0;
}
</style>