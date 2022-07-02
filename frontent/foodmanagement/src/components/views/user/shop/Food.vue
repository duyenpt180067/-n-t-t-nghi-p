<template>
<div class="shop-food">
    <div class="food">
        <div class="food-content flex">
            <div class="img-food w-1/3 m-r-12 pointer flex-center">
                <img :src="FoodMerge.Food.ImageURL" class="w-full" alt="">
                <span class="sale" v-if="FoodMerge.Food.HasDiscount">Sale!</span>
            </div>
            <div class="init-food w-2/3">
                <div class="food-name m-b-20">{{FoodMerge.Food.FoodName}}</div>
                <div class="category flex">
                    <span style="color:gray;" class="w-1/5">Danh mục: </span> 
                    <router-link tag="b" :to="'/user/shop/'+ FoodMerge.Food.CategoryId" class="w-4/5 pointer" style="color:var(--primary-color)">{{FoodMerge.Food.CategoryName}}</router-link>
                </div>
                <div class="food-title">{{FoodMerge.Food.Title}}</div>
                <div class="food-amount flex m-b-20">
                    <span style="color:gray;" class="w-1/5">Đơn giá: </span>
                    <div class="m-r-12" :class="{'line-through': FoodMerge.Food.HasDiscount}">
                        <b>{{FoodMerge.FoodDetails[0].NumberAmount}}</b>
                        <b v-if="FoodMerge.FoodDetails.length > 1"> - {{FoodMerge.FoodDetails[FoodMerge.FoodDetails.length-1].NumberAmount}}</b>
                        <span v-if="!FoodMerge.Food.HasDiscount">VND</span>
                    </div>
                    <div class="w-2/5" v-if="FoodMerge.Food.HasDiscount">
                        <b>{{formatNumber(FoodMerge.FoodDetails[0].RealAmount)}}</b>
                        <b v-if="FoodMerge.FoodDetails.length > 1"> - {{formatNumber(FoodMerge.FoodDetails[FoodMerge.FoodDetails.length-1].RealAmount)}}</b> VND
                    </div>
                </div>
                <div class="flex-center food-to-cart m-b-20">
                    <div class="w-1/5 food-quantity flex-center">
                        <button @click="updateQuantity('subtract')">-</button>
                        <div class="w-1/3 center"><input class="center w-full" type="number" v-model="quantity" id=quantity></div>
                        <button @click="updateQuantity('add')">+</button>
                    </div>
                    <div class="w-3/7 food-size flex-center">
                        <span class="w-1/4 m-r-12">Size: </span>
                        <v-combobox @getSelect="changeSize($event)" v-if="!FoodMerge.Food.HasDiscount"
                                    :combobox_valid="false" 
                                    :placeholder="''" 
                                    class="w-1/3 m-r-12"
                                    :item_text="['SizeName', 'NumberAmount']" 
                                    :items="FoodMerge.FoodDetails"
                                    :display_item="'SizeName'" 
                                    :groupName="['Kích thước', 'Đơn giá']"
                                    :vmodel="sizeModel"
                                    :multiple="false">
                        </v-combobox>
                        <v-combobox @getSelect="changeSize($event)" v-else
                                    :combobox_valid="false" 
                                    :placeholder="''" 
                                    class="w-1/3 m-r-12"
                                    :item_text="['SizeName', 'NumberAmount', 'RealAmount']" 
                                    :items="FoodMerge.FoodDetails"
                                    :display_item="'SizeName'" 
                                    :groupName="['Kích thước', 'Đơn giá', 'Giá sau giảm']"
                                    :vmodel="sizeModel"
                                    :multiple="false"></v-combobox>
                        <div class="food-detail-price w-1/3">Giá: {{amountFoodDetail}}</div> 
                    </div>
                    <div class="w-1/3 add-to-cart">
                        <button class="flex-center pointer" @click="addToCart"><i class="fa fa-shopping-cart m-r-12"></i> Thêm vào giỏ hàng</button>
                    </div>
                    <div class="w-1/8" style="text-align: left;"><i class="fa fa-heart add-heart"></i></div>
                </div>
                <div class="toppings flex m-b-20" v-if="FoodMerge.Toppings && FoodMerge.Toppings.length > 0">
                    <div class="w-1/6">
                        <div class="topping-quantity">
                            <input type="checkbox" class="checkbox pointer flex-center" ref="ChooseAll" id="choose-all" @change="caculateTopping('all', $event)"/>
                        </div>
                        <div class="topping-name">Topping</div> 
                        <div class="topping-price">Giá (VND)</div> 
                    </div>
                    <div class="topping-item w-1/6" v-for="(topping) in FoodMerge.Toppings" :key="topping.ToppingCode">
                        <div class="topping-quantity">
                            <input type="checkbox" class="checkbox pointer flex-center" ref="CheckChoose"
                                    @change="caculateTopping(topping, $event)"
                                    :value="topping.ToppingId" name="topping" v-model="checkedToppings"/>
                        </div>
                        <div class="topping-name">{{topping.ToppingName}}</div> 
                        <div class="topping-price">{{topping.ToppingNumberAmount}}</div> 
                    </div>
                    <div class="w-1/6 topping-last">
                        <div class="topping-name flex-center" style="padding: 0;">Tổng giá</div> 
                        <div class="topping-price flex-center" style="height: 66.67%">{{amountToppingModel}}</div> 
                    </div>
                </div>
                <div class="all-amount" style="font-size: 18px;">
                    Thành tiền: <b style="color: var(--primary-color);">{{amountFood}}</b> VND
                </div>
            </div>
        </div> 
        <div class="description-comment">
            <div class="btn-des-com">
                <div class="flex-center" style="margin:auto;width:fit-content;">
                    <div class="btn-init btn-des m-r-12" :class="{'btn-active': isDescription}" @click="isDescription=true">Mô tả</div>
                    <div class="btn-init btn-com" :class="{'btn-active': !isDescription}" @click="isDescription=false">Bình luận ({{listComment.length}})</div>
                </div>
            </div>
            <div class="des-com-content">
                <div class="description" :class="{'d-none': !isDescription}">{{FoodMerge.Food.Descriptions}}</div>
                <div class="comments" :class="{'d-none': isDescription}">
                    <div class="comment flex">
                        <div class="w-1/5 center pointer m-r-12 comment-item" :class="{'active':star==null}"><div class="status" @click="loadComments(null)">Tất cả <div class="line-active"></div></div></div>
                        <div class="w-1/5 center pointer m-r-12 comment-item" :class="{'active':star==1}"><div class="status" @click="loadComments('1')">1 sao <div class="line-active"></div></div></div>
                        <div class="w-1/5 center pointer m-r-12 comment-item" :class="{'active':star==2}"><div class="status" @click="loadComments('2')">2 sao <div class="line-active"></div></div></div>
                        <div class="w-1/5 center pointer m-r-12 comment-item" :class="{'active':star==3}"><div class="status" @click="loadComments('3')">3 sao <div class="line-active"></div></div></div>
                        <div class="w-1/5 center pointer m-r-12 comment-item" :class="{'active':star==4}"><div class="status" @click="loadComments('4')">4 sao <div class="line-active"></div></div></div>
                        <div class="w-1/5 center pointer comment-item" :class="{'active':star==5}" @click="loadComments('5')"><div class="status">5 sao <div class="line-active"></div></div></div>
                    </div>
                    <div class="m-t-20 center">
                        <div v-if="listComment.length > 0">
                            <div v-for="(data) in listComment" :key="data.CommentId" class="m-b-12 comment-init">
                                <div class="rate">
                                    <span class="stars-container" :class="'stars-'+data.CommentStar*20">★★★★★</span>
                                    <div class="flex" style="align-items:center">
                                        <span class="m-r-12"><b>{{data.UserName}}</b></span>
                                        <i class="fas fa-clock m-r-12" style="color:var(--gray-color)"></i>
                                        <span>{{formatDate(data.CreatedDate)}}</span>
                                    </div>
                                    <div class="content-comment">{{data.CommentContent}}</div>
                                </div>
                            </div>
                        </div>
                        <div v-else class="w-full">
                            <img src="../../../../assets/lib/img/common/bg_report_nodata.76e50bd8.svg" alt=""><br>
                            <span class="no-data-text">Không có đánh giá</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>  
        <div class="related-food center" v-if="relatedFood.length > 0">
            <div class="related-title">Menu liên quan</div>
            <div @click="reloadPage(data.FoodCode)" class="food-item w-1/3 pointer" v-for="(data) in relatedFood" :key="data.FoodId">
                <div class="flex-center food-init">
                    <div class="w-2/5 m-r-12">
                        <div class="img-food"><img :src="data.ImageURL" alt=""></div>
                        <span class="sale" v-if="data.HasDiscount">Sale!</span>
                    </div>
                    <div class="w-3/5 m-l-12">
                        <div class="food-name"><b>{{data.FoodName}}</b></div>
                        <div class="food-title">{{data.Title}}</div>
                        <div class="food-size flex">
                            <div class="w-1/4">Size: <b>{{data.SizeName}}</b></div>
                            <div class="w-3/4">Giá: <b :class="{'line-through': data.HasDiscount}">{{data.NumberAmount}}</b> <b v-if="data.HasDiscount">{{formatNumber(data.RealAmount)}}</b> VND</div>
                        </div>
                    </div>
                </div>
            </div>
        </div> 
    </div>
</div>
</template>

<script>
import CommentAPI from '../../../../api/component/bussiness-item/CommentAPI';
import FoodAPI from '../../../../api/component/food/FoodAPI';
import CartDetailAPI from '../../../../api/component/user/CartDetailAPI';

import Base from '../../../../base/Base';
import VCombobox from '../../../base/VCombobox.vue';
// import VCombobox from '@/components/base/VCombobox';
//import VSelect from '../../../base/VSelect.vue';
export default {
    name:'Food',
    components:{ VCombobox },
    data(){
        return{
            FoodMerge:{
                Food:{
                    ImageURL:'',
                    FoodView: 0,
                },
                FoodDetails:[{
                    Amount:0
                }],
                Toppings:[],
                Comments:[],
            },
            quantity:1,
            sizeModel:{
                SizeName:'',
                SizeCode:''
            },
            isDescription: true, 
            relatedFood:[],
            amountToppingModel:0,
            amountFoodDetail: 0,
            amountFood:0,
            checkedToppings:[],
            listComment: [],
            star: null,
        }
    },
    watch:{
        quantity(){
            if(!this.quantity || this.quantity < 0){
                this.quantity = 1;
            }
            this.amountFoodDetail = Base.formatNumber(this.sizeModel.RealAmount*this.quantity);
            this.amountFood = Base.formatNumber(Base.formatToNumber(this.amountToppingModel) + Base.formatToNumber(this.amountFoodDetail));
        },
    },
    async created() {
        try {
            this.FoodMerge = await FoodAPI.getFoodByCode(this.$route.params.foodCode);
            this.FoodMerge.Food.FoodView += 1;
            this.listComment = this.FoodMerge.Comments;
            if(this.FoodMerge.Food.DiscountId&&(new Date(this.FoodMerge.Food.DiscountStartDate) <= new Date())&&(new Date(this.FoodMerge.Food.DiscountEndDate) >= new Date())){
                this.FoodMerge.Food.HasDiscount = true;
                if(this.FoodMerge.FoodDetails.length > 0){
                    this.FoodMerge.FoodDetails.forEach(ele => {  
                        ele.NumberAmount = Base.formatNumber(ele.Amount);
                        if(ele.Amount*this.FoodMerge.Food.DiscountAmount/100 > this.FoodMerge.Food.DiscountMaxAmount){
                            ele.RealAmount = ele.Amount - this.FoodMerge.Food.DiscountMaxAmount;
                        }
                        else {
                            ele.RealAmount = ele.Amount - ele.Amount*this.FoodMerge.Food.DiscountAmount/100;
                        }
                    });
                }
            }
            else {
                this.FoodMerge.Food.HasDiscount = false;
                if(this.FoodMerge.FoodDetails.length > 0){
                    this.FoodMerge.FoodDetails.forEach(ele => {  
                        ele.RealAmount = ele.Amount
                        ele.NumberAmount = Base.formatNumber(ele.Amount);
                    });
                }
            }
            if(this.FoodMerge.Toppings && this.FoodMerge.Toppings.length > 0){
                this.FoodMerge.Toppings.forEach(topping => {
                    topping.ToppingNumberAmount = Base.formatNumber(topping.ToppingAmount);
                    topping.ToppingQuantity = 0;
                });
            }
            this.sizeModel = this.FoodMerge.FoodDetails[0];
            this.relatedFood = (await FoodAPI.getFilterPaging({FoodFilter:null, CategoryId:this.FoodMerge.Food.CategoryId, PageNumber:1, PageSize:100})).data;
            if(this.relatedFood.length <= 15){
                let moreRelatedFood = [];
                moreRelatedFood = (await FoodAPI.getFilterPaging({FoodFilter:this.FoodMerge.Food.CategoryName, CategoryId:null, PageNumber:1, PageSize:10})).data
                let keyUnique = 'FoodCode';
                this.relatedFood = [...new Map(this.relatedFood.concat(moreRelatedFood).map(item => [item[keyUnique], item])).values()];
                //Base.uniqueArray(this.relatedFood.concat(moreRelatedFood));
            }
            //this.relatedFood = this.relatedFood.filter(e => e.FoodId != this.FoodMerge.Food.FoodId);
            for (let index = 0; index < this.relatedFood.length; index++) {
                const el = this.relatedFood[index];
                if(el.FoodId == this.FoodMerge.Food.FoodId){
                    this.relatedFood.splice(index, 1);
                    index -= 1;
                }
                else {
                    if(el.DiscountId&&(new Date(el.DiscountStartDate) <= new Date())&&(new Date(el.DiscountEndDate) >= new Date())){
                        el.HasDiscount = true;
                        if(el.Amount*el.DiscountAmount/100 > el.DiscountMaxAmount){
                            el.RealAmount = el.Amount - el.DiscountMaxAmount;
                        }
                        else {
                            el.RealAmount = el.Amount - el.Amount*el.DiscountAmount/100;
                        }
                    }
                    else {
                        el.HasDiscount = false;
                        el.RealAmount = el.Amount;
                    }
                    el.NumberAmount = Base.formatNumber(el.Amount);
                }
                
            }
        } catch (error) {
            console.log(error);
        }
    },
    methods:{
        async loadComments(item){
            this.star = parseInt(item);
            this.listComment = (await CommentAPI.getFilterPaging({
                CommentStar: this.star,
                CommentStatus: null,
                FoodCode:this.FoodMerge.Food.FoodCode,
                ListUserId: null,
                OrderId: null,
            })).data;
        },
        updateQuantity(operation){
            if(operation == 'add'){
                this.quantity = parseInt(this.quantity) + 1;
            }
            else{
                this.quantity = parseInt(this.quantity) > 1 ? parseInt(this.quantity)-1 : 1;
            }
        },
        formatNumber(val){
            return Base.formatNumber(val);
        },
        caculateTopping(item, e){
            if (item == "all") {
                // nếu đang chọn tất cả
                if(this.$refs.ChooseAll.checked == true){
                    // tất cả input type = checkbox trong bảng gán bằng checked
                    this.$refs.CheckChoose.forEach(i =>{
                        i.checked = true;
                    })
                    this.amountToppingModel = Base.formatNumber(this.FoodMerge.Toppings.reduce((a, b) => a.ToppingAmount + b.ToppingAmount));
                }
                // ngược lại
                else {
                    this.$refs.CheckChoose.forEach(i =>{
                        i.checked = false;
                    })
                    this.amountToppingModel = 0;
                }
            } else {
                if(this.checkedToppings.length == this.FoodMerge.Toppings.length){
                    this.$refs.ChooseAll.checked = true;
                }
                else {
                    this.$refs.ChooseAll.checked = false;
                }
                if(e.target.checked==true){
                    this.amountToppingModel = Base.formatNumber(Base.formatToNumber(this.amountToppingModel) + parseInt(item.ToppingAmount));
                }
                else {
                    this.amountToppingModel = Base.formatNumber(Base.formatToNumber(this.amountToppingModel) - parseInt(item.ToppingAmount));
                }
            }
            this.amountFood = Base.formatNumber(Base.formatToNumber(this.amountToppingModel) + Base.formatToNumber(this.amountFoodDetail));
        },
        changeSize(item){
            this.sizeModel = item,
            this.amountFoodDetail = Base.formatNumber(item.RealAmount*this.quantity);
            this.amountFood = Base.formatNumber(Base.formatToNumber(this.amountToppingModel) + Base.formatToNumber(this.amountFoodDetail));
        },
        reloadPage(code){
            this.$router.push({
                path: '/user/menu/'+code, 
                params: {
                    foodCode: code,
                }
            });
            location.reload();
        },
        formatDate(val){
            return Base.formatDateToString(val);
        },
        async addToCart(){
            try {
                let user = JSON.parse(localStorage.getItem('user'));
                let bodyCartDetail = {
                    FoodDetailId: this.sizeModel.FoodDetailId,
                    Quantity: this.quantity,
                    ListTopping: this.checkedToppings.toString(),
                }
                console.log(bodyCartDetail);
                if(user){
                    bodyCartDetail.UserName = user.userName; 
                    localStorage.setItem('addCart', JSON.stringify(bodyCartDetail));
                    let result = await CartDetailAPI.postObj(bodyCartDetail);
                    if(result.status == 201){
                        this.$router.push({
                            path: '/user/cart'
                        })
                    }
                }
                else {
                    localStorage.setItem('addCart', JSON.stringify(bodyCartDetail));
                    this.$router.push({
                        path: '/login'
                    })
                }
            } catch (error) {
                console.log(error);
            }
        },
        justNumber(event){
            return Base.justNumberInput(event);
        }
    },
}
</script>

<style>
.food .comment-init {
    display: inline-block;
    min-width: 400px;
    text-align: center;
}
.food .rate{
    margin: auto;
    text-align: left;
}
.food {
    padding: 20px;
    font-size: 14px;
}
.food .food-content .img-food{
    border: 1px solid var(--gray-color);
    border-radius: 15px;
    transition: .2s;
    padding: 15px;
    position: relative;
}
.food .food-content .img-food:hover{
    background: #fff9e9;
}
.food .food-size .v-select__slot input {
    padding-left: 10px;
}
.food .food-content .img-food:hover img{
    transform: scale(1.1);
}
.food .food-content .img-food img{
    transition: .2s;
    border-radius: 15px;
}
.food .init-food {
    padding: 10px 20px;
}
.food .init-food .food-name,
.food .related-title{
    font-size: 25px;
    font-weight: bold;
    height: 40px;
    color: var(--primary-color);
}
.food .init-food .food-title{
    margin: 10px 0;
    color: gray;
}
.food-quantity button{
    background-color: #e0e0e0;
    border-radius: 50%;
    height: 35px;
    width: 35px;
}
.food-quantity input:focus,
.food-quantity input:active{
    border: 0 !important;
}
.food .add-to-cart button{
    margin: auto;
    transition: .2s;
    padding: 6px 10px;
    font-weight: bold;
    border-radius: 3px;
    background-color: var(--primary-color);
}
.food .add-to-cart button:hover{
    color: ghostwhite;
}
.food-to-cart{
    justify-content: space-between;
    text-align: center;
}
.food .add-heart{
    padding: 10px;
    background-color: #e0e0e0;
    border-radius: 5px;
    cursor: pointer;
}
.description-comment{
    padding: 20px 50px;
}
.description-comment .des-com-content{
    margin: 25px;
}
.description-comment .des-com-content .description{
    white-space: pre-wrap;
}
.description-comment .btn-init {
    min-width: fit-content;
    padding: 8px 20px;
    border-radius: 5px;
    transition: .2s;
    cursor: pointer;
    font-size: 15px;
}
.description-comment .btn-init.btn-active{
    background-color: var(--primary-color);
    font-weight: bold;
}
.description-comment .btn-init.btn-active:hover{
    background-color: var(--primary-color);
    font-weight: bold;
}
.description-comment .btn-init:hover{
    background: #e0e0e0;
}
.toppings .topping-name,
.toppings .topping-price,
.toppings .topping-quantity{
    border-right: 1px dotted #c7c7c7;
    border-bottom: 1px solid #c7c7c7;
    padding: 0 10px;
    height: 25px;
    display: flex;
    align-items: center;
}
.toppings .topping-quantity{
    padding: 0;
}
.toppings .topping-quantity input {
    margin: auto;
}
.toppings .topping-last .topping-name,
.toppings .topping-last .topping-price{
    border-right: 0;
    border-bottom: 1px solid #c7c7c7;
}
.description-comment .line{
    padding: 30px 0;
    border-top: 1px solid var(--gray-color);
}
.description-comment .comment .comment-item > .status{
    width: fit-content;
    margin: auto;
    transition: .2s;
}
.description-comment .comment .line-active{
    width: 0;
    height: 3.5px;
    background-color: var(--primary-color);
    transition: .2s;
}
.description-comment .comment .comment-item > .status:hover .line-active,
.description-comment .comment .active .line-active{
    width: 100%;
}
</style>