<template>
<div class="category">
    <div class="btn-menu">
    <button>MENUS</button>
    </div>
    <div class="home-menu">
        <div class="catalog" v-for="(category) in listCategory" :key="category.CategoryId">
            <router-link tag="div" :to="'/user/shop/'+category.CategoryId" class="catalog-init pointer">
                <div class="category-img">
                    <div class="hover-category"></div>
                    <!--   -->
                    <img class="img-project" alt="" :src="category.CategoryImage">
                </div>
                <div class="category-name">{{category.CategoryName}}</div>
            </router-link>
        </div>
    </div>
</div>
</template>
<style scoped>
@import url('../../../../assets/css/user/home/category.css');
</style>
<script>
import CategoryAPI from '../../../../api/component/food/CategoryAPI'
export default {
    name: 'Category',
    data(){
        return {
            listCategory: []
        }
    },
    async created() {
        try {
            this.listCategory = (await CategoryAPI.getFilterPaging({CategoryFilter:null,CategoryStatus:null})).data;
        } catch (error) {
            console.log(error);
        }
    },
    methods: {

    }
}
</script>