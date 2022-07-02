<template>
<div class="nav-top flex align-items">
    <router-link tag="div" to="/user/home" class="logo-home pointer">Project</router-link>
    <div class="side-menu align-items">
        <router-link tag="div" to="/user/home" class="side d-inline-block">Home</router-link>
        <!-- <router-link tag="div" to="/user/menu" class="side d-inline-block">Menu</router-link> -->
        <router-link tag="div" to="/user/about" class="side d-inline-block">About</router-link>
        <router-link tag="div" to="/user/shop" class="side d-inline-block">Shop</router-link>
        <router-link tag="div" to="/user/blog" class="side d-inline-block">Blog</router-link>
        <router-link tag="div" to="/user/faq" class="side d-inline-block" v-tooltip.bottom="{content:'Câu hỏi thường gặp', classes:['tooltip'], offset: '5px'}">FAQ</router-link>
    </div>
    <div class="call-and-order flex align-items">
        <div class="icon-ship"></div>
        <div>
            <div class="call-and-order-text">Call and Order in</div>
            <div class="call-and-order-phone">+1 718-904-4450</div>
        </div>
    </div>
    <div class="head-other flex align-items">
        <div style="position:relative" class="search">
            <div class="head-other-item flex-center pointer" @click="clickSearch= !clickSearch" v-tooltip.bottom="{content:'Tìm kiếm', classes:['tooltip'], offset: '5px'}">
                <i class="fa fa-search"></i>
            </div>
            <div class="search-food flex-center" v-click-outside="outSearch" v-if="clickSearch">
                <div class="arrow-top-right"></div>
                <input type="text" placeholder="Nhập tên sản phẩm để tìm kiếm" v-model="searchFood" @keyup.enter="goToShop">
                <i class="fa fa-search" @click="goToShop"></i>
            </div>
        </div>
        <!-- router-link tag="div" to="/login" v-tooltip.bottom="{content:'Đăng nhập', classes:['tooltip'], offset: '5px'}" -->
        <div class="head-other-item flex-center user pointer" @click="goToUser">
            <i class="fa fa-user"></i>
        </div>
        <div class="head-other-item cart flex-center pointer" @click="goToCart" v-tooltip.bottom="{content:'Giỏ hàng', classes:['tooltip'], offset: '5px'}" ><i class="fa fa-shopping-cart"></i></div>
    </div>
</div>
</template>
<style>
.logo-home {
    font-size: 30px;
    text-align: center;
    font-weight: bold;
    font-style: italic;
    color: var(--primary-color);
    text-transform: uppercase;
    font-family: sans-serif;
} 
</style>
<script>
import {Tabs} from '../../base/Resources'
export default {
//   components: { TheSelect },
    props:{
        tabActive: String,
        isEmployee: Boolean
    },
    data(){
        return {
            tabs: Tabs,
            clickSearch: false,
            searchFood: "",
        }
    },
    mounted(){
    },
    name:'TheHeader',
    methods:{
        outSearch(){
            this.clickSearch = false;
        },
        goToCart(){
            let user = JSON.parse(localStorage.getItem('user'));
            if(user){
                this.$router.push({
                    path: '/user/cart'
                });
            }else{
                this.$router.push({
                    path: '/login'
                });
            }
        },
        goToUser(){
            let user = JSON.parse(localStorage.getItem('user'));
            if(user){
                this.$router.push({
                    path: '/user/account'
                });
            }else{
                this.$router.push({
                    path: '/login'
                });
            }
        },
        goToShop(){
            this.$router.push({
                path: '/user/shop', 
                params: {
                    param1: 'value1'
                }
            });
        }
    }
}
</script>