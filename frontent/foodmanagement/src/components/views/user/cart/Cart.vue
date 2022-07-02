<template>
<div class="cart m-t-20">
    <div class="flex" style="align-items: baseline;">
        <div class="w-1/2 order-info address">
            Địa chỉ giao hàng:
            <textarea class="w-full" rows="2" ref="OrderAddress" v-model="address" placeholder="">
            </textarea>  
        </div>
        <div class="w-1/2 order-info phone">
            Số điện thoại:
            <input  ref="OrderPhone" v-model="phone"
                    :class="{ 'is-invalid': !testPhone }"
                    type="text" maxlength="20" class="w-full form-control" @change="phoneChange()">
            <div v-if="!testPhone" class="invalid-feedback">
                <p>Số điện thoại không hợp lệ!</p>
            </div>
        </div>
    </div>
    <div class="food-item w-full" v-for="(data, index) in listCart" :key="data.CartDetailId">
        <div class="flex food-init" style="align-items:center;">
            <router-link tag="div" :to="'/user/menu/'+data.FoodCode" style="width:150px;" class="m-r-12 pointer food-img">
                <div class="img-food"><img :src="data.ImageURL" alt=""></div>
                <span class="sale" v-if="data.HasDiscount">Sale!</span>
            </router-link>
            <div class="w-1/4 m-l-12">
                <router-link tag="div" :to="'/user/menu/'+data.FoodCode" class="food-name pointer"><b>{{data.FoodName}}</b></router-link>
                <div class="food-size flex-center">
                    <div class="w-1/2 flex-center">
                        <div class="w-1/3 m-r-12">Size: </div> 
                        <v-combobox @getSelect="changeSize($event, data, index)" v-if="!data.HasDiscount"
                                    :combobox_valid="false" 
                                    :placeholder="''" 
                                    class="w-2/3 m-r-12 food-size"
                                    :item_text="['SizeName', 'NumberAmount']" 
                                    :items="data.ListFoodDetailSame"
                                    :display_item="'SizeName'" 
                                    :groupName="['Kích thước', 'Đơn giá']"
                                    :vmodel="data.sizeModel"
                                    :multiple="false">
                        </v-combobox>
                        <v-combobox @getSelect="changeSize($event, data, index)" v-else
                                    :combobox_valid="false" 
                                    :placeholder="''" 
                                    class="w-2/3 m-r-12 food-size"
                                    :item_text="['SizeName', 'NumberAmount', 'RealAmountNumber']" 
                                    :items="data.ListFoodDetailSame"
                                    :display_item="'SizeName'" 
                                    :groupName="['Kích thước', 'Đơn giá', 'Giá sau giảm']"
                                    :vmodel="data.sizeModel"
                                    :multiple="false">
                        </v-combobox>
                    </div>
                    <div class="w-1/2">
                        <div class="unit-price">
                            Giá: <b :id="'numberAmount'+index" :class="{'line-through': data.HasDiscount}">{{data.NumberAmount}}</b> 
                            <span v-if="!data.HasDiscount">VND</span>
                        </div>
                        <div v-if="data.HasDiscount">
                            <b>{{formatNumber(data.RealAmount)}} VND</b>
                        </div>
                    </div>
                </div>
            </div>
            <div class="w-1/6 m-l-12 flex-center food-to-cart">
                <div class="food-quantity flex-center">
                    <button class="m-l-12" @click="updateQuantity('subtract', index, data)">-</button>
                    <div class="w-1/3 center"><input class="center w-full" type="number" v-model="data.Quantity" @change="caculateTopping(data, 'updateQuantity', index)" id=quantity></div>
                    <button class="m-r-12" @click="updateQuantity('add', index, data)">+</button>
                </div>
            </div>
            <div class="m-l-12 w-1/3">
                <div v-if="data.ListOrgTopping && data.ListOrgTopping.length > 0" class="toppings flex ">
                    <div class="">
                        <div class="topping-quantity">
                            <input type="checkbox" class="checkbox pointer flex-center" :ref="'ChooseAll'+index" :id="'choose-all' + index" @change="caculateTopping(data, 'all', index, $event)"/>
                        </div>
                        <div class="topping-name">Topping</div> 
                        <div class="topping-price">Giá (VND)</div> 
                    </div>
                    <div class="topping-item" v-for="(topping) in data.ListOrgTopping" :key="topping.ToppingCode">
                        <div class="topping-quantity">
                            <input type="checkbox" class="checkbox pointer flex-center" :ref="'CheckChoose'+index"
                                    @change="caculateTopping(data, topping, index, $event)"
                                    :value="topping.ToppingId" name="topping" v-model="data.checkedToppings"/>
                        </div>
                        <div class="topping-name">{{topping.ToppingName}}</div> 
                        <div class="topping-price">{{topping.ToppingAmount}}</div> 
                    </div>
                    <div class="topping-last">
                        <div class="topping-name flex-center" style="padding: 0;">Tổng giá</div> 
                        <div class="topping-price flex-center" style="height: 66.67%" :id="'amountToppingModel'+index">{{data.amountToppingModel}}</div> 
                    </div>
                </div>
            </div>
            <div class="w-1/8 m-l-12">
                Thành tiền: <b style="color:var(--primary-color);" :id="'amountFood'+index">{{data.amountToppingModel}}</b>
            </div>
            <div class="w-1/8 m-l-12 cart-action center">
                <div><button disabled @click="crud='put';crudObject(data, index)" :id="'update'+index">Cập nhật</button></div>
                <div><button @click="crud='delete';crudObject(data.CartDetailId)">Xóa</button></div>
            </div>
        </div>
    </div>
    <div class="flex-center m-t-20 m-b-20 to-order">
        <div class="w-1/2" style="font-size: 20px;">Tổng tiền: <b style="color:var(--primary-color);">{{allAmount}}</b></div>
        <div class="home">
            <button @click="order('post')">Đặt hàng</button>
        </div>
    </div>
    <div>
        <div v-for="(mgs, index) in listMgs" :key="index">
            <toast-message :titleMgs="mgs.titleMgs" :iconToast="mgs.iconToast" v-if="mgs.showToast" @deleteToast="deleteToast(index)"></toast-message>
        </div>
        <v-popup v-if="popShow" :popShow="popShow" :message="message" @closePop="closePop" :action="actionPopup"></v-popup>
    </div>
</div>
</template>

<script>
import ToppingAPI from '../../../../api/component/additional-element/ToppingAPI';
import OrderAPI from '../../../../api/component/order/OrderAPI';
import CartDetailAPI from '../../../../api/component/user/CartDetailAPI';
import Base from '../../../../base/Base';
import { CRUD, PopupState } from '../../../../base/Resources';
import ToastMessage from '../../../base/ToastMessage.vue';
import VCombobox from '../../../base/VCombobox.vue';
import VPopup from '../../../base/VPopup.vue';
export default {
    name:'Cart',
    components:{ VCombobox, VPopup, ToastMessage },
    data(){
        return {
            listCart:[],
            listTopping: [],
            crud: '',
            user: null,
            allAmount: 0,
            address:"",
            popShow:false,
            listMgs: [],
            message: "",
            actionPopup: "",
            phone: "",
            testPhone: true,
        }
    },
    async created(){
        this.user = JSON.parse(localStorage.getItem('user'));
        if(this.user){
            this.address = this.user.address;
            this.phone = this.user.phone;
            this.listTopping = await ToppingAPI.getAll();
            await this.getListCart();
            this.listCart.forEach((ele, index) => {
                this.caculateTopping(ele, "load", index);
            });
        }
        else {
            this.$router.push({ path: '/login' });
        }
    },
    methods:{
        formatNumber(val){
            return Base.formatNumber(val);
        },
        /**
         * delete toast message
         * Create: PTDuyen(13/09/2021)
         */
        deleteToast(index) {
            this.listMgs.splice(index, 1);
        },
        phoneChange(){
            let regexPhone = /^[+]?[(]?[0-9]{2,3}[)]?[-\s.]?[2-9]{1}\d{2}[-\s.]?[0-9]{4,6}$/;
            this.phone = this.phone.trim();
            if(regexPhone.test(this.phone.trim())){
                this.testPhone = true;
                return true;
            }else {
                this.testPhone = false;
                return false;
            }
        },
        async getListCart() {
            this.listCart = (await CartDetailAPI.getFilterPaging({UserName: this.user.userName})).Data;
            if(this.listCart && this.listCart.length > 0){
                this.listCart.forEach((ele) => {  
                    if(ele.DiscountCode&&(new Date(ele.DiscountStartDate) <= new Date())&&(new Date(ele.DiscountEndDate) >= new Date())){
                        ele.HasDiscount = true;
                        if(ele.UnitPrice*ele.DiscountAmount/100 > ele.DiscountMaxAmount){
                            ele.RealAmount = ele.UnitPrice - ele.DiscountMaxAmount;
                        }
                        else {
                            ele.RealAmount = ele.UnitPrice - ele.UnitPrice*ele.DiscountAmount/100;
                        }
                    }
                    else {
                        ele.HasDiscount = false;
                        ele.RealAmount = ele.UnitPrice;
                    }
                    ele.sizeModel = {
                        FoodDetailId: ele.FoodDetailId,
                        SizeName: ele.SizeName,
                        SizeCode: ele.SizeCode,
                        Amount: ele.UnitPrice,
                        RealAmount: ele.RealAmount,
                    }
                    ele.NumberAmount = Base.formatNumber(ele.sizeModel.Amount);
                    ele.amountToppingModel = 0;
                    ele.checkedToppings = ele.Toppings && ele.Toppings.length > 0 ? ele.Toppings.map(x => x.ToppingId):[];
                    for (let index = 0; index < ele.ListFoodDetailSame.length; index++) {
                        const element = ele.ListFoodDetailSame[index];
                        element.NumberAmount = Base.formatNumber(element.Amount);
                        element.RealAmount = !ele.HasDiscount ? 
                                            element.Amount : 
                                            (element.Amount*ele.DiscountAmount/100 > ele.DiscountMaxAmount ? 
                                                element.Amount - ele.DiscountMaxAmount : 
                                                element.Amount - element.Amount*ele.DiscountAmount/100)
                        element.RealAmountNumber = Base.formatNumber(element.RealAmount);
                    }
                });
            }
        },
        updateQuantity(operation, index, data){
            if(operation == 'add'){
                this.listCart[index].Quantity = parseInt(this.listCart[index].Quantity) + 1;
            }
            else{
                this.listCart[index].Quantity = parseInt(this.listCart[index].Quantity) > 1 ? parseInt(this.listCart[index].Quantity)-1 : 1;
            }
            this.caculateTopping(data, 'updateQuantity', index);
        },
        caculateTopping(data, item, indexCart, e){
            data.amountFoodDetail = Base.formatNumber(data.sizeModel.RealAmount*data.Quantity);
            if (item == "all") {
                // nếu đang chọn tất cả
                if(this.$refs['ChooseAll'+indexCart][0].checked == true){
                    // tất cả input type = checkbox trong bảng gán bằng checked
                    this.$refs['CheckChoose'+indexCart].forEach(i =>{
                        i.checked = true;
                    })
                    data.amountToppingModel = Base.formatNumber(this.listCart[indexCart].ListOrgTopping.reduce((a, b) => a.ToppingAmount + b.ToppingAmount));
                }
                // ngược lại
                else {
                    this.$refs['CheckChoose'+indexCart].forEach(i =>{
                        i.checked = false;
                    })
                    data.amountToppingModel = 0;
                    data.checkedToppings = [];
                }
            } 
            else if(item == "load"){
                if(data.checkedToppings && data.checkedToppings.length == this.listCart[indexCart].ListOrgTopping.length){
                    if(document.getElementById('choose-all'+indexCart))
                        document.getElementById('choose-all'+indexCart).checked = true;
                }
                else { 
                    if(document.getElementById('choose-all'+indexCart))
                        document.getElementById('choose-all'+indexCart).checked = false;
                }
                if(this.listCart[indexCart].Toppings && this.listCart[indexCart].Toppings.length > 1){
                    data.amountToppingModel = Base.formatNumber(this.listCart[indexCart].Toppings.reduce((a, b) => a.ToppingAmount + b.ToppingAmount));
                }
                else if(this.listCart[indexCart].Toppings && this.listCart[indexCart].Toppings.length == 1){
                    data.amountToppingModel = Base.formatNumber(this.listCart[indexCart].Toppings[0].ToppingAmount);
                }
                else{
                    data.amountToppingModel = 0;
                }
                if(document.getElementById('amountToppingModel'+indexCart)){
                    document.getElementById('amountToppingModel'+indexCart).innerText = data.amountToppingModel;
                }
                data.amountOrgFood = Base.formatNumber(Base.formatToNumber(data.amountToppingModel) + Base.formatToNumber(data.amountFoodDetail));
            }
            else if (item == "updateQuantity"){
                data.amountFoodDetail = Base.formatNumber(data.sizeModel.RealAmount*data.Quantity);
            }
            else {
                if(data.checkedToppings && data.checkedToppings.length == this.listCart[indexCart].ListOrgTopping.length){
                    this.$refs['ChooseAll'+indexCart][0].checked = true;
                }
                else {
                    this.$refs['ChooseAll'+indexCart][0].checked = false;
                }
                data.ListTopping = data.checkedToppings.toString();
                if(e.target.checked==true){
                    data.amountToppingModel = Base.formatNumber(Base.formatToNumber(data.amountToppingModel) + parseInt(item.ToppingAmount));
                }
                else {
                    data.amountToppingModel = Base.formatNumber(Base.formatToNumber(data.amountToppingModel) - parseInt(item.ToppingAmount));
                }
            }
            data.amountNumberFood = Base.formatToNumber(data.amountToppingModel) + Base.formatToNumber(data.amountFoodDetail);
            data.amountFood = Base.formatNumber(data.amountNumberFood);
            this.allAmount = 0;
            this.listCart.forEach(item =>{
                this.allAmount += item.amountNumberFood;
            })
            this.allAmount = Base.formatNumber(this.allAmount);
            if(document.getElementById('amountFood'+indexCart)){
                document.getElementById('amountFood'+indexCart).innerText = data.amountFood;
            }
            if(data.amountFood != data.amountOrgFood){
                document.getElementById('update'+indexCart).disabled = false;
            }
            else {
                document.getElementById('update'+indexCart).disabled = true;
            }
        },
        changeSize(item, data, index){
            data.sizeModel = item;
            data.FoodDetailId = item.FoodDetailId;
            data.NumberAmount = Base.formatNumber(data.sizeModel.Amount);
            document.getElementById('numberAmount'+index).innerText = data.NumberAmount;
            // data.amountFoodDetail = Base.formatNumber(item.Amount*data.quantity);
            // data.amountFood = Base.formatNumber(Base.formatToNumber(data.amountToppingModel) + Base.formatToNumber(data.amountFoodDetail));
            this.caculateTopping(data, "updateQuantity", index);
        },
        async crudObject(obj, index){
            try {
                switch (this.crud) {
                    case CRUD.Delete:
                        var { data } = await CartDetailAPI.deleteMultiObj([obj]);
                        console.log(data);
                        await this.getListCart();
                        this.listCart.forEach((ele, index) => {
                            this.caculateTopping(ele, "load", index);
                        });
                        break;
                    case CRUD.Put:
                        var res = await CartDetailAPI.putObj(obj);
                        if(res.status == 202){
                            //await this.getListCart();
                            document.getElementById("update"+index).disabled = true;
                        }
                        break;
                    default:
                        break;
                }
            } catch (error) {
                console.log(error)
            }
        },
        async order(action){
            try {
                switch (action) {
                    case CRUD.Post:
                        if(!this.address){
                            this.popShow = true;
                            this.message = "Bạn vui lòng điền địa chỉ nhận hàng trước!";
                            this.actionPopup = PopupState.Duplicate;
                        }
                        else if(!this.phone){
                            this.popShow = true;
                            this.message = "Bạn vui lòng điền số điện thoại nhận hàng trước!";
                            this.actionPopup = PopupState.Duplicate;
                        }
                        else if(this.listCart.length == 0){
                            this.popShow = true;
                            this.message = "Bạn vui lòng chọn sản phẩm để đặt hàng!";
                            this.actionPopup = PopupState.Duplicate;
                        }
                        else {
                            let orderDetails = [];
                            this.listCart.forEach(ele =>{
                                orderDetails.push({
                                    FoodDetailId: ele.FoodDetailId,
                                    ListTopping: ele.ListTopping,
                                    DiscountId: ele.DiscountId,
                                    Quantity: ele.Quantity,
                                    Amount: ele.amountNumberFood,
                                    UnitPrice: ele.UnitPrice,
                                    CartDetailId: ele.CartDetailId,
                                })
                            })
                            let obj = {
                                Order: {
                                    TotalAmount: Base.formatToNumber(this.allAmount),
                                    Phone: this.phone,
                                    Address: this.address,
                                    OrderStatus: 0, // 0 - Processing,
                                    UserName: this.user.userName,
                                    DiscountId: null,
                                },
                                OrderDetails: orderDetails
                            }
                            console.log(obj);
                            var res = await OrderAPI.postOrder(obj);
                            if(res.data.success && res.data.resultCode == "FM-Success"){
                                this.$router.push({path: '/user/account', name: 'Account'});
                            }
                        }
                        break;
                
                    default:
                        break;
                }
            } catch (error) {
                console.log(error)
            }
        },
        closePop(){
            this.popShow=false;
            if(!this.address){
                this.$refs.OrderAddress.focus();
            }
            else if(!this.phone){
                this.$refs.OrderPhone.focus();
            }
            else if(this.listCart.length == 0){
                this.$router.push({ path: '/user/shop' })
            }
        }
    },
}
</script>

<style>
.cart .order-info{
    padding: 20px;
    position: sticky;
    top: 57px;
    background: #fff;
    z-index: 1;
}
.cart .order-info.phone input {
    height: 36px;
    padding: 10px;
    border-radius: 3px;
}
.cart .food-item .food-init:hover{
    background: #fff;
}
.cart .food-item .food-init:hover img{
    transform: scale(1);
}
.cart .food-item .food-init:hover .food-name{
    font-size: 18px;
}
.cart .food-item .food-init .food-img:hover img{
    transform: scale(1.1);
}
.cart .food-size .v-select__slot input {
    padding-left: 10px;
}
.cart .food-item .food-init .food-size{
    margin-top: 0;
}
button[disabled]{
    color: var(--gray-color);
}
.cart .cart-action button[disabled]:hover{
    text-decoration: none;
    color: var(--gray-color);
}
.cart .cart-action button:hover{
    text-decoration: underline;
    color: #f2765e;
}
.cart .to-order{
    position: sticky;
    padding: 10px 0;
    bottom: 0;
    background: #fff;
}
</style>