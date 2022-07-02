<template>
<div class="content page-admin" :class="{'fade-left': fadeleft}">
    <!-- loading -->
    <div :class="{ 'd-none': !loader, 'background-loader':loader}">
        <div :class="{loader:loader}" class="bg-load">
            <object :data="loaderUrl"></object>
        </div>
    </div>
    <div class="flex content-header">
        <div class="welcome">
            Welcome to Project
        </div>
        <div class="user-account pointer flex" @click="openUser=!openUser">
            <i class="fa fa-user"></i>
            <i class="fa fa-caret-down"></i>
        </div>
        <div class="user-data" v-if="user && openUser" v-click-outside="closeUser">
            <div class="center m-b-12" style="font-size:16px"><b>Thông tin tài khoản</b></div>
            <div class="center user-name m-b-12"><b>{{user.userName}}</b></div>
            <div class="user-info m-b-20">
                <p class="m-b-2">Họ tên: <b>{{user.fullName}}</b></p>
                <p class="m-b-2">Mã nhân viên: <b>{{user.userCode}}</b></p>
                <p class="m-b-2">Email: <b>{{user.email}}</b></p>
                <p class="m-b-2">Số điện thoại: <b>{{user.phone}}</b></p>
            </div>
            <div class="pointer btn-account m-b-12">Đổi mật khẩu</div>
            <div class="pointer btn-account" @click="logout">Đăng xuất</div>
        </div>
    </div>
    <router-view :fadeleft="fadeleft" :loader="loader" @changeLoader="changeLoader($event)"></router-view>
</div>
</template>

<script>
import Base from '../../../base/Base';
export default {
    name:"PageAdmin",
    props:{
        fadeleft:Boolean
    },
    data(){
        return {
            // hiển thị loader
            loader: false,
            loaderUrl: require('../../../assets/lib/img/common/loading.svg'),
            user: null,
            openUser: false
        }
    },
    created(){
        this.user = JSON.parse(localStorage.getItem('user'));
        if(!this.user){
            this.$router.push({ path: '/login' });
        }
    },
    methods:{
        changeLoader(value){
            this.loader = value;
        },
        logout(){
            Base.logout();
            this.$emit('changeUser', false);
            this.$router.push({ path: '/login' });
        },
        closeUser(){
            this.openUser = false;
        }
    },
}
</script>

<style>
@import url(../../../assets/css/admin/common/form-detail.css);

.content-head{
    padding: 15px 20px;
}

.content .content-title{
    font-family: 'GoogleSans-Bold';
    font-size: 24px;
    flex: 1;
}

.page-admin .content-header{
    padding: 10px 20px;
    background-color: #fff;
}

.page-admin .welcome{
    font-size: 18px;
    font-weight: bold;
    flex: 1;
    color: var(--primary-color);
    font-style: italic;
}

.page-admin .user-account{
    padding: 0.5rem;
    justify-content: space-between;
    width: 50px;
    color: #404040;
    transition: .2s;
    border-radius: 4px;
    font-size: 14px;
}
.page-admin .user-account:hover{
    background-color: #f0f2f5;
}

.page-admin .user-data{
    position: absolute;
    background: #fff;
    padding: 14px 60px;
    top: 55px;
    right: 5px;
    font-size: 14px;
    border-radius: 3px;
    box-shadow: 0px 0px 5px rgb(0 0 0 / 40%);
    z-index: 5;
}
.page-admin .user-data .user-name{
    font-size: 17px;
    color: var(--primary-color);
    text-transform: uppercase;
}
.page-admin .btn-account{
    transition: .2s;
    border-radius: 3px;
    border: 1px solid var(--gray-color);
    padding: 5px 10px;
    width: 105px;
    text-align: center;
    margin: auto;
}
.page-admin .btn-account:hover{
    color: #f2765e;
}
</style>
