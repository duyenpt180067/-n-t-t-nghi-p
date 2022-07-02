<template>
    <div class="order-user-detail">
        <div class="cover-background"></div>
        <div class="detail-item add-item">
            <div class="add-entity">
                <form class="form-add" id="scroll-thumb" autocomplete="off">
                    <div class="form-header">
                        <h2 class="form-title">Thông tin đơn hàng</h2>
                        <div class="header-btn-content">
                            <div v-tooltip.bottom="{content:'Đóng (Esc)', classes:'tooltip', offset: '5px'}" 
                                class="form-header-btn btn-close" id="btn-close" tabindex="0" 
                                @click="btnXForm()"
                                v-shortkey="['esc']" @shortkey="btnXForm()">
                            </div>
                        </div>
                    </div>
                    <div class="flex" style="padding-bottom: 20px;border-bottom: 1px solid var(--gray-color);">
                        <div class="w-1/2 flex m-r-12">
                            <div class="w-2/3 order-info address m-r-12">
                                Địa chỉ giao hàng: <div>{{orderMerge.Order.Address}}</div>
                            </div>
                            <div class="w-1/3 order-info phone m-r-12">
                                Số điện thoại: <div>{{orderMerge.Order.Phone}}</div>
                            </div>
                        </div>
                        <div class="w-1/3">Trạng thái đơn hàng<span v-if="!isEmployee">:</span> <b v-if="!isEmployee"><p>{{orderStatus.text}}</p></b>
                            <div class="page-size" v-if="isEmployee">
                                <v-select :item_text="'text'" :items="listOrderStatus" :vmodel="orderStatus" @getSelect="orderStatus=$event"></v-select>
                            </div>
                            <!-- <div>{{orderStatus}}</div> -->
                        </div>
                        <div class="w-1/4 to-order end">
                            <div style="font-size: 20px;">Tổng tiền: <b style="color:var(--primary-color);">{{allAmount}}</b></div>
                        </div>
                    </div>
                    <div class="order-init" style="padding: 15px 0;">
                        <div class="food-item w-full" v-for="(data, index) in orderMerge.OrderDetails" :key="data.OrderDetailId">
                            <div class="flex food-init" style="align-items:center;">
                                <div tag="div" @click="goToFood(data.FoodCode)" style="width:150px;padding: 10px;" class="m-r-12 pointer food-img">
                                    <div class="img-food"><img :src="data.ImageURL" alt=""></div>
                                    <span class="sale" v-if="data.HasDiscount">Sale!</span>
                                </div>
                                <div class="w-1/4 m-l-12">
                                    <div @click="goToFood(data.FoodCode)" class="food-name pointer"><b>{{data.FoodName}}</b></div>
                                    <div class="food-size flex-center">
                                        <div class="w-1/3 flex-center">Size: {{data.SizeName}}</div> 
                                        <div class="w-2/3">
                                            <div class="unit-price">
                                                Giá: <b :id="'numberAmount'+index" :class="{'line-through': data.HasDiscount}">{{formatNumber(data.UnitPrice)}}</b> 
                                                <span v-if="!data.HasDiscount">VND</span>
                                            </div>
                                            <div v-if="data.HasDiscount">
                                                <b>{{formatNumber(data.RealAmount)}} VND</b>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="w-1/8 m-l-12 flex-center food-to-cart">
                                    <div class="food-quantity">Số lượng: <b> {{formatNumber(data.Quantity)}}</b></div>
                                </div>
                                <div class="m-l-12 w-1/3">
                                    <div v-if="data.Toppings && data.Toppings.length > 0" class="toppings flex ">
                                        <div class="">
                                            <div class="topping-name">Topping</div> 
                                            <div class="topping-price">Giá (VND)</div> 
                                        </div>
                                        <div class="topping-item" v-for="(topping) in data.Toppings" :key="topping.ToppingCode">
                                            <div class="topping-name">{{topping.ToppingName}}</div> 
                                            <div class="topping-price">{{formatNumber(topping.ToppingAmount)}}</div> 
                                        </div>
                                        <div class="topping-last">
                                            <div class="topping-name flex-center" style="padding: 0;">Tổng giá</div> 
                                            <div class="topping-price flex-center" :id="'amountToppingModel'+index">{{data.AmountToppingModel}}</div> 
                                        </div>
                                    </div>
                                </div>
                                <div class="w-1/8 m-l-12">
                                    Thành tiền: <b style="color:var(--primary-color);" :id="'amountFood'+index">{{data.NumberAmount}}</b>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-footer flex" v-if="isEmployee">
                        <div class="admin-btn-normal btn-delete" tabindex="18" @click="btnXForm()" @keyup.enter="btnXForm()">Hủy</div>
                        <div style="flex:1;" class="flex-center m-r-12" v-if="orderStatus.value==1||orderStatus.value==6">
                            <p class="m-r-12" style="width:110px;">Lý do {{orderStatus.text}}</p>
                            <textarea name="OrderReason" style="height: 39px;" @blur="checkOrderReason" v-model="order.OrderReason" class="form-control w-full" id="OrderReason" ref="OrderReason"></textarea>
                            <div class="invalid-feedback" style="margin-top: -65px;margin-left: 50%;">
                                <p>Lý do {{orderStatus.text}} không được để trống.</p>
                            </div>
                        </div>
                        <div class="btn-save-all flex">
                            <!-- :disable="readonly" -->
                            <div class="admin-btn-normal btn-save" tabindex="16"
                                @click="crudObject()"
                                @keyup.enter="crudObject()"
                                v-shortkey="['ctrl','s']" @shortkey="crudObject()"
                                v-tooltip.top="{content:'Cất (Ctrl + S)', classes:'tooltip', offset: '5px'}">Cất</div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
import OrderAPI from '../../../../api/component/order/OrderAPI'
import Base from '../../../../base/Base';
import { ListOrderStatus } from '../../../../base/vi/Resources';
import VSelect from '../../../base/VSelect.vue';
export default {
  components: { VSelect },
    name:'OrderUserDetail',
    props:{
        orderRead: Object,
    },
    data(){
        return {
            order: Object.assign({}, this.orderRead),
            orderMerge: {
                Order: {
                    Address:"",
                    Phone: "",
                    OrderStatus: 0,
                    OrderReason: "",
                },
                OrderDetails: [],
            },
            orderStatus: ListOrderStatus[0],
            allAmount: 0,
            listOrderStatus: ListOrderStatus,
            isEmployee: false,
        }
    },
    async created() {
        let user = JSON.parse(localStorage.getItem("user"));
        if(user && user.isEmployee){
            this.isEmployee = true;
        }
        this.orderMerge = await OrderAPI.getOrderByCode(this.order.OrderCode);
        this.allAmount = 0;
        this.orderStatus = ListOrderStatus.filter(el => el.value == this.orderMerge.Order.OrderStatus)[0];
        switch (this.orderStatus.value) {
            case 0:
                this.listOrderStatus = ListOrderStatus.filter(el => el.value <= 2 && el.value >= 0);
                break;
            case 1: 
            case 6:
                this.listOrderStatus = ListOrderStatus.filter(el => el.value == this.orderStatus.value);
                break;
            case 3:
            case 2:
                this.listOrderStatus = ListOrderStatus.filter(el => el.value <= 4 && el.value >= this.orderStatus.value);
                break;
            case 4:
            case 5:
                this.listOrderStatus = ListOrderStatus.filter(el => el.value <= 5 && el.value >= this.orderStatus.value);
                break;
            default:
                this.listOrderStatus = ListOrderStatus.filter(el => el.value <= 2 && el.value >= 0);
                break;
        }
        this.orderMerge.OrderDetails.forEach(element => {
            element.NumberAmount = Base.formatNumber(element.Amount);
            if(element.FoodDiscountCode&&(new Date(element.FoodDiscountStartDate) <= new Date())&&(new Date(element.FoodDiscountEndDate) >= new Date())){
                element.HasDiscount = true;
                if(element.UnitPrice*element.FoodDiscountAmount/100 > element.FoodDiscountMaxAmount){
                    element.RealAmount = element.UnitPrice - element.FoodDiscountMaxAmount;
                }
                else {
                    element.RealAmount = element.UnitPrice - element.UnitPrice*element.FoodDiscountAmount/100;
                }
            }
            else {
                element.HasDiscount = false;
                element.RealAmount = element.Amount;
            }
            this.allAmount += element.Amount;
            if(element.Toppings){
                if(element.Toppings.length > 1){
                    element.AmountToppingModel = Base.formatNumber(element.Toppings.reduce((a, b) => a.ToppingAmount + b.ToppingAmount));
                }
                else if(element.Toppings.length == 1){
                    element.AmountToppingModel = Base.formatNumber(element.Toppings[0].ToppingAmount);
                }
                else {
                    element.AmountToppingModel = 0;
                }
            }
            
        });
        this.allAmount = Base.formatNumber(this.allAmount);
    },
    methods:{
        btnXForm(){
            this.$emit('btnXForm');
        },

        goToFood(foodCode){
            if(!this.isEmployee){
                this.$router.push({path: '/user/menu/'+foodCode})
            }
            else{
                this.$router.push({path: '/app/food-detail', name: 'FoodDetail', params:{code: foodCode}});
            }
        },

        formatNumber(val){
            return Base.formatNumber(val);
        },

        crudObject(){
            //this.orderMerge.Order.OrderReason = this.order.OrderReason;
            if(this.orderStatus.value==1||this.orderStatus.value==6){
                if(this.checkOrderReason()){
                    if(this.orderMerge.Order.OrderStatus == this.orderStatus.value&&this.orderMerge.Order.OrderReason == this.order.OrderReason){
                        this.btnXForm();
                    }
                    else{
                        this.orderMerge.Order.OrderStatus = this.orderStatus.value;
                        this.orderMerge.Order.OrderReason = this.order.OrderReason
                        this.$emit('crudObject', this.orderMerge.Order); 
                    }
                }
                else {
                    document.getElementsByClassName("invalid-feedback")[0].style.visibility="visible";
                    return;
                }
            }
            else {
                if(this.orderMerge.Order.OrderStatus == this.orderStatus.value&&this.orderMerge.Order.OrderReason == this.order.OrderReason){
                    this.btnXForm();
                }
                else{
                    this.orderMerge.Order.OrderStatus = this.orderStatus.value;
                    this.orderMerge.Order.OrderReason = this.order.OrderReason
                    this.$emit('crudObject', this.orderMerge.Order); 
                }
            }
        },

        checkOrderReason(){
            if(!this.order.OrderReason){
                this.$refs.OrderReason.classList.add("is-invalid");
                return false;
            }
            else {
                this.$refs.OrderReason.classList.remove("is-invalid");
                document.getElementsByClassName("invalid-feedback")[0].style.visibility="hidden";
                return true;
            }
        }
    }
}
</script>

<style>
.order-user-detail {
    position: fixed;
    top: 0;
    left: 0;
    height: 100%;
    width: 100%;
    z-index: 2;
}
.order-user-detail .form-add{
    font-size: 14px;
    width: calc(100% - 40px);
    border-radius: 5px;
    /* padding-bottom: 20px; */
}
.order-user-detail .order-init .food-item .food-init{
    margin: 10px 0;
    padding: 0;
}
</style>
