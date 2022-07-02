<template>
<div class="popular-dish">
    <h1>Popular dishes</h1>
    <div class="menu-dishes">
        <div class="popular-category" v-for="(category, index) in lstPopularCategory" :key="category.CategoryId" :ref="category.CategoryCode">
            <button class="popular-category-btn" :class="{'active-dish': index==activeCategory}" @click="getFoodPopular(category.CategoryId, index)">{{category.CategoryName}}</button>
        </div>
    </div>
    <div class="content-shop" style="text-align: center;">
        <div class="food-item pointer w-1/3" v-for="(data) in lstFoodPopular" :key="data.FoodId">
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
                        <div class="w-3/4">Giá: <b :class="{'line-through': data.HasDiscount}">{{data.Amount}}</b> <b v-if="data.HasDiscount">{{formatNumber(data.RealAmount)}}</b> VND</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</template>
<style scoped>

.popular-dish {
    font-family: 'GoogleSans-Bold';
    color: #1E1D23;
    text-align: center;
    margin-bottom: 50px;
}

.popular-dish h1{
    font-size: 42px;
    line-height: 45px;
    margin-bottom: 40px;
}

.popular-dish .menu-dishes .popular-category{
    display: inline-block;
    margin: 5px 10px;
    border-radius: 28px;
}

.popular-dish .menu-dishes .popular-category .popular-category-btn{
    font-size: 14px;
    font-weight: 700;
    text-transform: uppercase;
    padding: 15px 45px;
    border-radius: 28px;
    border: 1px solid var(--gray-color2);
    background-color: #fff;
    box-shadow: none;
}

.popular-dish .menu-dishes .popular-category .popular-category-btn:hover{
    border: 1px solid var(--primary-color);
    color: black;
}

.popular-dish .menu-dishes .popular-category .popular-category-btn.active-dish{
    background-color: var(--primary-color);
}
</style>
<script>
import CategoryAPI from '../../../../api/component/food/CategoryAPI'
import FoodAPI from '../../../../api/component/food/FoodAPI';
import Base from '../../../../base/Base';
export default {
    name: 'PopularDish',
    data(){
        return {
            lstPopularCategory: [],
            lstFoodPopular:[],
            activeCategory: 0,
        }
    },
    async created() {
        try {
            this.lstPopularCategory = await CategoryAPI.getPopularCategory();
            this.getFoodPopular(this.lstPopularCategory[0].CategoryId, 0);
        } catch (error) {
            console.log(error);
        }
    },
    mounted(){
        // this.$nextTick(() => { 
        //     this.$el.querySelector('.popular-category button.popular-category-btn').classList.add("active-dish");
        // })
    },
    methods: {
        async getFoodPopular(categoryId, index){
            this.activeCategory = index;
            this.lstFoodPopular = await FoodAPI.getFoodPopular(categoryId);
            if(this.lstFoodPopular.length > 0){
                this.lstFoodPopular.forEach(ele => {  
                    console.log(ele);
                    if(ele.DiscountId&&(new Date(ele.DiscountStartDate) <= new Date())&&(new Date(ele.DiscountEndDate) >= new Date())){
                        ele.HasDiscount = true;
                        if(ele.Amount*ele.DiscountAmount/100 > ele.DiscountMaxAmount){
                            ele.RealAmount = ele.Amount - ele.DiscountMaxAmount;
                        }
                        else {
                            ele.RealAmount = ele.Amount - ele.Amount*ele.DiscountAmount/100;
                        }
                    }
                    else {
                        ele.HasDiscount = false;
                        ele.RealAmount = ele.Amount;
                    }
                    ele.Amount = Base.formatNumber(ele.Amount);
                });
            }
        },
        formatNumber(val){
            return Base.formatNumber(val);
        }
    }
}
</script>