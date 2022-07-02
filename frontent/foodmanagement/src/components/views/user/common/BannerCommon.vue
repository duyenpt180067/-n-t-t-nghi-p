<template>
<div class="banner-common">
    <div class="banner-icon">
        <div class="banner-title">{{displayTitle}}</div>
        <div class="flex-center banner-init">
            <router-link tag="div" to="/user/home" class="to-home pointer m-r-12"><b>Home</b></router-link>
            <i class="fa fa-angle-right"></i>
            <div class="active-tab m-l-12" style="color:var(--primary-color)"><b>{{title}}</b></div>
        </div>
    </div>
</div>
</template>
<style>
.banner-common{
    background-color: #f7f2e2;
    padding: 35px 0;
}
.banner-common .banner-title{
    text-align: center;
    font-size: 50px;
    font-weight: 600;
}
.banner-common .banner-init{
    font-size: 16px;
}
</style>
<script>
export default {
    name:"BannerCommon",
    props:{
        title: String,
        displayTitle: String,
    },
    data(){
        return{}
    }
}
</script>