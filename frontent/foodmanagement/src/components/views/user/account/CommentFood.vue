<template>
    <div class="order-user-detail">
        <div class="cover-background"></div>
        <div class="detail-item add-item">
            <div class="add-entity">
                <form class="form-add" id="scroll-thumb" autocomplete="off">
                    <div class="form-header">
                        <h2 class="form-title">Đánh giá</h2>
                        <div class="header-btn-content">
                            <div v-tooltip.bottom="{content:'Đóng (Esc)', classes:'tooltip', offset: '5px'}" 
                                class="form-header-btn btn-close" id="btn-close" tabindex="0" 
                                @click="btnXForm()"
                                v-shortkey="['esc']" @shortkey="btnXForm()">
                            </div>
                        </div>
                    </div>
                    <div class="flex" style="align-items: center;padding-bottom: 20px;border-bottom: 1px solid var(--gray-color);">
                        <div class="w-1/2 flex m-r-12">
                            <div class="w-2/3 order-info address m-r-12">
                                Địa chỉ giao hàng: <div>{{orderMerge.Order.Address}}</div>
                            </div>
                            <div class="w-1/3 order-info phone m-r-12">
                                Số điện thoại: <div>{{orderMerge.Order.Phone}}</div>
                            </div>
                        </div>
                        <div class="w-1/3">Trạng thái đơn hàng: <b><p>{{orderStatus.text}}</p></b>
                            <!-- <div>{{orderStatus}}</div> -->
                        </div>
                        <div class="w-1/4 to-order end">
                            <div style="font-size: 20px;">Tổng tiền: <b style="color:var(--primary-color);">{{allAmount}}</b></div>
                        </div>
                    </div>
                    <div class="order-init" style="padding: 15px 0;">
                        <div class="food-item w-full" v-for="(data, index) in orderDetails" :key="data.FoodCode">
                            <div class="flex food-init" style="align-items:center;">
                                <div tag="div" @click="goToFood(data.FoodCode)" style="width:150px;padding: 10px;" class="m-r-12 pointer food-img">
                                    <div class="img-food"><img :src="data.ImageURL" alt=""></div>
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
                                <div class="rate w-1/3" :class="'rate-'+index">
                                    <div class="star" v-if="readOnly">
                                        <div class="rating flex" style="align-items:center;">
                                            <span>Đánh giá</span>
                                            <div class="center" style="flex:1;">
                                                <span class="stars-container" :class="'stars-'+data.Comment.CommentStar*20">★★★★★</span>
                                            </div>
                                        </div>
                                        <div class="comment-content">
                                            <p>Bình luận</p>
                                            <textarea class="form-control w-full" :readonly="readOnly" :ref="'CommentContent'+index" :id="'CommentContent'+index" rows="3" placeholder="Bình luận của bạn" v-model="data.Comment.CommentContent"></textarea>
                                        </div>
                                    </div>
                                    <div class="star" v-else>
                                        <div class="rating">
                                            <!-- <span style="color: red;">*</span> -->
                                            <p>Đánh giá</p>
                                            <fieldset class="rate flex-center" style="border: none;">
                                                <input class="form-control d-none" @click="send('1', index)" type="radio" :checked="checked_1" id="star1" name="rating" value="1" />
                                                <label class="star-lable m-r-12 pointer" for="star1" title="Tệ - 1 sao"><i class="fas fa-star"></i></label>
                                                <input class="form-control d-none" @click="send('2', index)" type="radio" :checked="checked_2" id="star2" name="rating" value="2" />
                                                <label class="star-lable m-r-12 pointer" for="star2" title="Hơi tệ - 2 sao"><i class="fas fa-star"></i></label>
                                                <input class="form-control d-none" @click="send('3', index)" type="radio" :checked="checked_3" id="star3" name="rating" value="3" />
                                                <label class="star-lable m-r-12 pointer" for="star3" title="Tốt - 3 sao"><i class="fas fa-star"></i></label>
                                                <input class="form-control d-none" @click="send('4', index)" type="radio" :checked="checked_4" id="star4" name="rating" value="4" />
                                                <label class="star-lable m-r-12 pointer" for="star4" title="Hài lòng - 4 sao"><i class="fas fa-star"></i></label>
                                                <input class="form-control d-none" @click="send('5', index)" type="radio" :checked="checked_5" id="star5" name="rating" value="5" />
                                                <label class="star-lable m-r-12 pointer" for="star5" title="Rất hài lòng - 5 sao"><i class="fas fa-star"></i></label>
                                            </fieldset>
                                        </div>
                                        <div class="comment-content">
                                            <p>Bình luận</p>
                                            <textarea class="form-control w-full" :ref="'CommentContent'+index" :id="'CommentContent'+index" rows="3" placeholder="Bình luận của bạn" v-model="data.Comment.CommentContent"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-footer flex" v-if="readOnly">
                        <div class="admin-btn-normal btn-delete" tabindex="18" @click="btnXForm()" @keyup.enter="btnXForm()">Hủy</div>
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
        <v-popup v-if="popShow" :popShow="popShow" :action="actionPop" @closeForm="btnXForm" @closePop="popShow=false;" :message="message"></v-popup>
    </div>
</template>

<script>
import OrderAPI from '../../../../api/component/order/OrderAPI'
import CommentAPI from '../../../../api/component/bussiness-item/CommentAPI'
import Base from '../../../../base/Base';
import { ListOrderStatus } from '../../../../base/vi/Resources';
import VPopup from '../../../base/VPopup.vue';
import { PopupState } from '../../../../base/Resources';
export default {
  components: { VPopup },
    name:'CommentFood',
    props:{
        orderRead: Object,
    },
    data(){
        return {
            popShow: false,
            message: false,
            actionPop:"",
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
            orderDetails:[],
            orderStatus: ListOrderStatus[0],
            allAmount: 0,
            readOnly: false,
            checked_5: false,
            checked_4: false,
            checked_3: false,
            checked_2: false,
            checked_1: false,
        }
    },
    async created() {
        this.readOnly = this.order.IsRated;
        this.orderMerge = await OrderAPI.getOrderByCode(this.order.OrderCode);
        this.allAmount = 0;
        this.orderStatus = ListOrderStatus.filter(el => el.value == this.orderMerge.Order.OrderStatus)[0];
        const key = 'FoodCode';
        this.orderDetails = [...new Map(this.orderMerge.OrderDetails.map(item => [item[key], item])).values()];
        this.orderDetails.forEach(element => {
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
            if(!element.Comment){
                element.Comment = {
                    FoodCode: element.FoodCode,
                    CommentContent: "",
                    CommentStar: 0,
                    OrderId: element.OrderId,
                    UserName:""
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
            this.$router.push({path: '/user/menu/'+foodCode})
        },

        formatNumber(val){
            return Base.formatNumber(val);
        },

        send(item, index){
            this.orderDetails[index].Comment.CommentStar = item;
            for (let i = 1; i <= 5; i++) {
                if(i<=item){
                    
                    document.querySelector(".rate-"+index+" label[for='star"+i+"']").style.color = "#ffc222";
                }
                else {
                    document.querySelector(".rate-"+index+" label[for='star"+i+"']").style.color = "var(--gray-color)";
                }
            }
        },

        async crudObject(){
            try {
                let user = JSON.parse(localStorage.getItem("user"));
                if(user){
                    let lstComment = [];
                    for (let index = 0; index < this.orderDetails.length; index++) {
                        const ele = this.orderDetails[index];
                        if(ele.Comment.CommentStar != 0 || ele.Comment.CommentContent){
                            lstComment.push({
                                FoodCode: ele.FoodCode,
                                CommentContent: ele.Comment.CommentContent,
                                CommentStar: ele.Comment.CommentStar,
                                OrderId: ele.OrderId,
                                UserName:user.userName,
                                UserId: null
                            })
                        }
                        
                    }
                    if(lstComment.length > 0){
                        let res = await CommentAPI.postListComment(lstComment);
                        if(res.data.success){
                            this.btnXForm();
                        }
                        else {
                            this.popShow = true;
                            this.actionPop = PopupState.Duplicate;
                            this.message = "Có lỗi xảy ra. Vui lòng thử lại hoặc liên hệ để trợ giúp!";
                        }
                    }
                    else {
                        this.popShow = true;
                        this.actionPop = PopupState.Duplicate;
                        this.message = "Bạn chưa đánh giá!";
                    }
                }
                else {
                    this.$router.push({path: '/login'});
                }
            } catch (error) {
                console.log(error);
            }
        },
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
.star-lable{
    color: var(--gray-color);
}
</style>