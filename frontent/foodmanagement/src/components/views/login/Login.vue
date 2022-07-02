<template>
<div class="login-signup">
    <div class="form-login">
        <div class="flex btn-login">
            <div class="text-title" :class="{'text-active': !isSignUp}" @click="showPass=false;isSignUp=false;$nextTick(() => { $v.$reset() })">Đăng nhập</div>
            <div class="text-title" :class="{'text-active': isSignUp}" @click="showPass=false;isSignUp=true;$nextTick(() => { $v.$reset() })">Đăng ký</div>
        </div>
        <div class="form-sign sign-in"  :class="{ 'w-0': isSignUp }"  v-if="!isSignUp">
            <div class="form-input">
                <input type="text" name="UserName" id="UserName" placeholder="Tên tài khoản" 
                        v-model="user.UserName" ref="UserName"
                        :class="{ 'is-invalid': $v.user.UserName.$error }"
                        @blur="$v.user.UserName.$touch()">
                <div v-if="$v.user.UserName.$error" class="invalid-feedback">
                    <p v-if="!$v.user.UserName.required">Tên tài khoản không được để trống!</p>
                </div>
            </div>
            <div class="form-input" style="position:relative;">
                <input type="password" name="Pass" id="Pass" placeholder="Mật khẩu"
                        v-model="user.Pass" ref="Pass" 
                        :class="{ 'is-invalid': $v.user.Pass.$error }"
                        @blur="$v.user.Pass.$touch()">
                <div v-if="$v.user.Pass.$error" class="invalid-feedback">
                    <p v-if="!$v.user.Pass.required">Mật khẩu không được để trống!</p>
                    <p v-if="$v.user.Pass.required && !$v.user.Pass.checkPassword">Mật khẩu chứa ít nhất 1 chữ cái thường, 1 chữ cái viết hoa, 1 chữ số và 1 ký tự đặc biệt!</p>
                </div>
                <i class="fa fa-eye-slash show-hide-pase pointer" v-if="showPass" @click="showHidePass(false)"></i>
                <i class="fa fa-eye show-hide-pase pointer" v-else @click="showHidePass(true)"></i>
            </div>
        </div>
        <div class="form-sign sign-up"  :class="{ 'w-0': !isSignUp }" v-if="isSignUp">
            <div class="form-input">
                <input type="text" name="UserName" id="UserName" placeholder="Tên tài khoản"
                        v-model="user.UserName" ref="UserName"
                        :class="{ 'is-invalid': $v.user.UserName.$error }"
                        @blur="$v.user.UserName.$touch()">
                <div v-if="$v.user.UserName.$error" class="invalid-feedback">
                    <p v-if="!$v.user.UserName.required">Tên tài khoản không được để trống!</p>
                </div>
            </div>
            <div class="form-input"> 
                <input name="Phone" id="Phone" placeholder="Số điện thoại" 
                        v-model="user.Phone"  ref="Phone"
                        :class="{ 'is-invalid': $v.user.Phone.$error }"
                        @blur="$v.user.Phone.$touch()">
                <div v-if="$v.user.Phone.$error" class="invalid-feedback">
                    <p v-if="!$v.user.Phone.required">Số điện thoại không được để trống!</p>
                    <p v-if="$v.user.Phone.required && !$v.user.Phone.checkPhoneNumber">Số điện thoại không hợp lệ!</p>
                </div>
            </div>
            <div class="form-input">
                <input type="text" name="Address" ref="Address" id="Address" placeholder="Địa chỉ" v-model="user.Address">
            </div>
            <!-- <div class="form-input"> 
                <input type="text" name="Address" id="Address" v-model="user.Address"  ref="Address" placeholder="Địa chỉ">
            </div> -->
            <div class="form-input" style="position:relative;">
                <input type="password" name="Pass" id="Pass" placeholder="Mật khẩu" 
                        v-model="user.Pass"  ref="Pass"
                        :class="{ 'is-invalid': $v.user.Pass.$error }"
                        @blur="$v.user.Pass.$touch()">
                <div v-if="$v.user.Pass.$error" class="invalid-feedback">
                    <p v-if="!$v.user.Pass.required">Mật khẩu không được để trống!</p>
                    <p v-if="$v.user.Pass.required && !$v.user.Pass.checkPassword">Mật khẩu chứa ít nhất 1 chữ cái thường, 1 chữ cái viết hoa, 1 chữ số và 1 ký tự đặc biệt!</p>
                </div>
                <i class="fa fa-eye-slash show-hide-pase pointer" v-if="showPass" @click="showHidePass(false)"></i>
                <i class="fa fa-eye show-hide-pase pointer" v-else @click="showHidePass(true)"></i>
            </div>
        </div>
        <div class="btn-login center">
            <button class="btn-submit" @click="login()">Next</button>
        </div>
    </div>
    <div>
        <v-popup v-if="popShow" :popShow="popShow" :action="action" :idInvalid="idInvalid" @closeForm="closeForm" :message="message" @closePop="closePop" @crudObject="login()"></v-popup>
        <div v-for="(mgs, index) in listMgs" :key="index">
            <toast-message :titleMgs="mgs.titleMgs" :iconToast="mgs.iconToast" v-if="mgs.showToast" @deleteToast="deleteToast(index)"></toast-message>
        </div>
    </div>
</div>
</template>
<style>
@import url('../../../assets/css/login/login.css');
.show-hide-pase{
    position: absolute;
    top: 35%;
    right: 10px;
}
</style>
<script>
import {
    required,
} 
from "vuelidate/lib/validators";
import VPopup from '@/components/base/VPopup';
import UserAPI from '../../../api/component/user/UserAPI';
import CartDetailAPI from '../../../api/component/user/CartDetailAPI';

import { PopupState } from '../../../base/Resources';
import Base from '../../../base/Base';
import ToastMessage from '../../base/ToastMessage.vue';
const regexPhone =/^[+]?[(]?[0-9]{2,3}[)]?[-\s.]?[2-9]{1}\d{2}[-\s.]?[0-9]{4,6}$/,
      regexPass = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?^()_#&])[A-Za-z\d@$!%*?^()_#]{6,}$/;
export default {
    components:{
        VPopup,
        ToastMessage,
    },
    watch:{
        
    },
    name:'Login',
    data(){
        return{
            user: {
                UserName: "",
                Pass: "",
                Phone: "",
                Addess: "",
            },
            isSignUp: false,
            idInvalid: "",
            // action post, put, copy
            action: "",
            // hiển thị popup
            popShow: false,
            // message trong popup
            message:"",
            // toast
            mgs: {
                titleMgs: "",
                showToast: false,
                iconToast: "",
            },
            listMgs: [],
            prevRoute: null,
            showPass: false
        }
    },
    beforeRouteEnter(to, from, next) {
        next(vm => {
            vm.prevRoute = from
        })
    },
    // rule của các input
    validations: {
        user: {
            UserName: {
                required,
            },
            Phone: {
                checkNull(){
                    if(this.isSignUp == false || this.user.Phone){
                        return true;
                    }
                    else return false;
                },
                checkPhoneNumber(){
                    if(this.isSignUp == false || regexPhone.test(this.user.Phone.trim()) == true){
                        return true;
                    }
                    else return false;
                }
            },
            Pass:{
                checkPassword(){
                    if(regexPass.test(this.user.Pass.trim()) == false){
                        return false;
                    }
                    else return true;
                }
            }
        }
    },
    methods:{
        showHidePass(val){
            this.showPass = val;
            document.getElementById("Pass").setAttribute("type", val?"text":"password");
        },
        /**
         * Mở popup
         * Create: PTDuyen(11/09/2021)
         */
        showPopup(action) {
            this.popShow = true;
            this.action = action;
        },

        /**
         * gọi đến hàm closeForm của ProviderList
         * Create: PTDuyen(11/09/2021)
         */
        closeForm() {
            this.$emit('closeForm');    
        },
        /**
         * delete toast message
         * Create: PTDuyen(13/09/2021)
         */
        deleteToast(index) {
            this.listMgs.splice(index, 1);
        },

        /**
         * Đóng popup
         * Create: PTDuyen(17/08/2021)
         */
        closePop() {
            // if(action == PopupState.Invalid){
            //     this.$refs[this.idInvalid].focus();
            // }
            // else if(action == PopupState.Duplicate){
            //     this.$refs[this.idInvalid].focus();
            //     this.$refs[this.idInvalid].classList.remove("is-invalid");
            // }
            this.popShow = false;
        },

        /**
         * validate khi nhấn click
         * Create: PTDuyen(18/08/2021)
         */
        validateForm() {
            this.idInvalid = "";
            this.$v.$touch();
            // nếu dữ liệu không hợp lệ
            if (this.$v.$invalid) {
                for(let key in this.user){
                    if(this.$v.user[key]){
                        if(this.$v.user[key].$error == true){
                            this.idInvalid = key;
                            this.showPopup("invalid");
                            return false;
                        }
                    }
                }
                return false;
            }
            // nếu dữ liệu hợp lệ 
            else {
                return true;
            }
        },
        async login(){
            try {
                console.log(this.prevRoute ? this.prevRoute.path : '/');
                if(this.validateForm() == true){
                    if(this.isSignUp == false){
                        let res = await UserAPI.login(this.user);
                        if(res.data.resultCode == "FM-Success"){
                            localStorage.setItem('user', JSON.stringify(res.data.data));
                            setTimeout(function(){
                                Base.logout();
                            }, 86400000)
                            // console.log(time);
                            if(res.data.data.isEmployee){
                                this.$emit('changeUser', res.data.data.isEmployee)
                                this.$router.push({path: '/app/home', name: 'HomeAdmin'});
                            }
                            else if(!res.data.data.isEmployee){
                                this.$emit('changeUser', res.data.data.isEmployee)
                                if(localStorage.getItem('addCart')){
                                    let addCart = JSON.parse(localStorage.getItem('addCart'));
                                    addCart.UserName = res.data.data.userName;
                                    let result = CartDetailAPI.postObj(addCart);
                                    console.log(result);
                                    //this.$router.push({path: this.prevRoute.path});
                                }
                                this.$router.push({path: this.prevRoute ? this.prevRoute.path : '/'});
                            }
                        }
                        else {
                            this.listMgs.push({
                                showToast: true,
                                titleMgs: "Tên tài khoản hoặc mật khẩu không đúng! Vui lòng kiểm tra lại!",
                                iconToast: 'error'
                            });
                        }
                        //console.log(res)
                    }
                    else{
                        this.user.isEmployee = false;
                        this.user.IsEmployee = false;
                        this.user.IsAdmin = false;
                        this.user.UserStatus = 0;
                        let res = await UserAPI.register(this.user);
                        //console.log(res);
                        if(res.data.resultCode == "FM-Success"){
                            localStorage.setItem('user', JSON.stringify(res.data.data));
                            setTimeout(function(){
                                Base.logout();
                            }, 86400000);
                            this.$router.push({path: this.prevRoute ? this.prevRoute.path : '/'});
                        }
                        else if(res.data.success==false && res.data.resultCode != "FM-Exception"){
                            this.message = res.data.data[0];
                            this.showPopup(PopupState.Duplicate)
                        }
                        else {
                            this.listMgs.push({
                                showToast: true,
                                titleMgs: "Thao tác thất bại! Vui lòng kiểm tra lại kết nối mạng hoặc liên hệ để được trợ giúp",
                                iconToast: 'error'
                            });
                        }
                    }
                }
            } catch (error) {
                this.listMgs.push({
                    showToast: true,
                    titleMgs: "Thao tác thất bại! Vui lòng kiểm tra lại kết nối mạng hoặc liên hệ để được trợ giúp",
                    iconToast: 'error'
                });
            }
        },

    },
}
</script>