<template>
<div class="navbarweb" :class="{fadeleft:fadeleft}">
    <!-- logo -->
    <div class="logo-site">
        <div class="toggle-icon">
            <div>
                <span class="material-icons" v-if="!fadeleft" @click="fadeLeft()">
                apps
                </span>
            </div>
            <div class="icon-toggle" v-if="fadeleft" @click="fade()"></div>
        </div>
        <router-link tag="div" to="/app/home" class="pointer logo-home" :class="{logoleft:fadeleft}" v-tooltip.bottom-end="{content:'Love your life', classes:'tooltip'}" title="">
            ĐATT
        </router-link>
    </div>
    <!-- menu -->
    <div class="navbar-init scroll-nav" id="scroll-thumb">
        <router-link tag="div" to="/app/home" id="app-home" class="nav-item" v-tooltip.top-end="{content:'Trang chủ', classes:'tooltip'}" title="">
            <div class="nav-item-icon app-home flex-center">
                <i class="fa fa-home"></i>
            </div>
            <div class="nav-item-text" :class="{textleft:fadeleft}">
                Trang chủ
            </div>
        </router-link>
        <router-link tag="div" to="/app/category" id="app-category" class="nav-item" v-tooltip.top-end="{content:'Danh mục', classes:'tooltip'}" title="">
            <div class="nav-item-icon app-category flex-center"> 
                <i class="fa fa-list"></i>
            </div>
            <div class="nav-item-text" :class="{textleft:fadeleft}">
                Danh mục
            </div>
        </router-link>
        <router-link tag="div" to="/app/food" id="app-food" class="nav-item" v-tooltip.top-end="{content:'Thực đơn', classes:'tooltip'}" title="">
            <div class="nav-item-icon app-food flex-center">
                <i class="fa fa-hamburger"></i>
            </div>
            <div class="nav-item-text" :class="{textleft:fadeleft}">
                Thực đơn
            </div>
        </router-link>
        <router-link tag="div" to="/app/order" id="app-order" class="nav-item" v-tooltip.top-end="{content:'Đơn hàng', classes:'tooltip'}" title="">
            <div class="nav-item-icon app-order flex-center">
                <i class="fa fa-signal"></i>
            </div>
            <div class="nav-item-text" :class="{textleft:fadeleft}">
                Đơn hàng
            </div>
        </router-link>
        <div class="has-child" id="app-manage" title="">
            <div class="flex nav-item parent" @click="toggleChild('#app-manage')" v-tooltip.top-end="{content:'Quản lý', classes:'tooltip'}" >
                <div class="nav-item-icon app-system flex-center">
                    <i class="fa fa-cube"></i>
                </div>
                <div class="nav-item-text" :class="{textleft:fadeleft}">
                    Quản lý
                </div>
                <div class="flex-center arror-right" :class="{'w-0':fadeleft}">
                    <i class="fa fa-chevron-right"></i>
                </div>
            </div>
            <div class="child h-0" :class="{'h-0':fadeleft}" style="height:160px">
                <router-link tag="div" to="/app/blog" :class="{textleft:fadeleft}" class="child-item">
                    <div class="nav-item-text">Blog</div>
                </router-link>
                <router-link tag="div" to="/app/slider" :class="{textleft:fadeleft}" class="child-item">
                    <div class="nav-item-text">Slider</div>
                </router-link>
                <router-link tag="div" to="/app/faq" :class="{textleft:fadeleft}" class="child-item" v-tooltip.top-end="{content:'Câu hỏi thường gặp', classes:'tooltip'}">
                    <div class="nav-item-text">FAQ</div>
                </router-link>
                <router-link tag="div" to="/app/discount" :class="{textleft:fadeleft}" class="child-item">
                    <div class="nav-item-text">Mã giảm giá</div>
                </router-link>
                <!-- <router-link tag="div" to="/app/discount-condition" :class="{textleft:fadeleft}" class="child-item">
                    <div class="nav-item-text">Điều kiện giảm giá</div>
                </router-link> -->
            </div>
        </div>
        <router-link tag="div" to="/app/access-log" class="nav-item" id="app-system" v-tooltip.bottom-end="{content:'Nhật ký truy cập', classes:'tooltip'}" title="">
            <div class="nav-item-icon app-system flex-center">
                <i class="fa fa-cog"></i>
            </div>
            <div class="nav-item-text" :class="{textleft:fadeleft}">
                Nhật ký truy cập
            </div>
        </router-link>
    </div>
</div>
</template>
<style scoped>
@import url('../../assets/css/common/menu.css');

.fadeleft{
    width: 56px!important;
}
.textleft{
    font-size: 0 !important;
}
.logoleft{
    width: 0 !important;
    transition: 100ms;
    min-width: 0 !important;
    background-size: 0;
    font-size: 0 !important;
}

.logo-home {
    flex: 1;
    padding-left: 15px;
    font-size: 40px;
    text-shadow: 2px 2px var(--primary-admin-color);
    font-style: italic;
    font-family: 'GOOGLESANS-BOLD';
}

.nav-item.router-link-active,
.child-item.router-link-active{
    background-color: #5d5d61;
    font-weight: bold;
    border-left: 5px solid var(--primary-admin-color) !important;

}

.toggle-icon{
    cursor: pointer;
}

.toggle-icon .router-link-active{
    background-color: #393a3d;
    border-left: none !important;
}
</style>

<script>
    export default {
        name:'TheMenu',
        props:{
            fadeleft: Boolean
        },
        created(){
            this.$emit('removeNavTop');
        },
        methods: {
            fade(){
                this.$emit('fade');
            },
            fadeLeft(){
                this.$emit('fadeLeft');
            },
            toggleChild(selector){
                this.$el.querySelector(selector+">.child").classList.toggle("h-0");
                this.$el.querySelector(selector+">.parent .arror-right").classList.toggle("transform90");
                this.$emit('fade');
            },
        },
    }
</script>