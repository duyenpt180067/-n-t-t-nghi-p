<template>
<div class="detail discount-condition-detail">
    <!-- loading -->
    <div v-if="loader" :class="{'background-loader':loader}">
        <div :class="{loader:loader}">
            <object :data="loaderUrl"></object>
        </div>
    </div>
    <!-- background -->
    <div class="cover-background"></div>
    <div class="add-item discount-item">
        <div class="add-entity">
             <form class="form-add" id="scroll-thumb" autocomplete="off">
                <div class="form-header">
                    <h2 class="form-title">Chi tiết điều kiện giảm giá</h2>
                    <div class="header-btn-content">
                        <div v-tooltip.bottom="{content:'Đóng (Esc)', classes:'tooltip', offset: '5px'}" 
                             class="form-header-btn btn-close" id="btn-close" tabindex="0" 
                             @click="btnXForm()"
                             v-shortkey="['esc']" @shortkey="btnXForm()">
                        </div>
                    </div>
                </div>
                <div class="form-init">
                    <div class="form-init-general">
                        <div class="general-init general-left">
                            <div class="general-row flex">
                                <div class="w-1/2 m-r-12">
                                    <p>Mã điều kiện <span>*</span></p>
                                    <input :readonly="readonly" v-model="discountCondition.DiscountConditionCode" ref="DiscountConditionCode" id="DiscountConditionCode"
                                        type="text" maxlength="20" tabindex="2" 
                                        class="w-full form-control" :class="{ 'is-invalid': $v.discountCondition.DiscountConditionCode.$error }"
                                        @blur="$v.discountCondition.DiscountConditionCode.$touch()">
                                    <div v-if="$v.discountCondition.DiscountConditionCode.$error" class="invalid-feedback">
                                        <p v-if="!$v.discountCondition.DiscountConditionCode.required">Mã điều kiện giảm giá không được để trống.</p>
                                    </div>
                                </div>
                                <div class="w-1/2 discount-object page-size">
                                    <p>Đối tượng áp dụng</p>
                                    <v-select :items="['Người dùng', 'Đơn hàng']" :tabindex="2" :vmodel="discountCondition.DiscountConditionFor" @getSelect="discountCondition.DiscountConditionFor = $event"></v-select>
                                </div>
                            </div>
                            <div class="general-row flex">
                                <div class="w-1/2 m-r-12">
                                    <p>Tiêu đề <span>*</span></p>
                                    <input :readonly="readonly" v-model="discountCondition.DiscountConditionName" ref="DiscountConditionName" id="DiscountConditionName"
                                        type="text" tabindex="2" 
                                        class="w-full form-control" :class="{ 'is-invalid': $v.discountCondition.DiscountConditionName.$error }"
                                        @blur="$v.discountCondition.DiscountConditionName.$touch()">
                                    <div v-if="$v.discountCondition.DiscountConditionName.$error" class="invalid-feedback">
                                        <p v-if="!$v.discountCondition.DiscountConditionName.required">Tiêu đề không được để trống.</p>
                                    </div>
                                </div>
                                <div class="w-1/2">
                                    <p>Lý do giảm giá</p>
                                    <textarea :readonly="readonly" tabindex="2" class="w-full" rows="2" v-model="discountCondition.DiscountConditionReason" placeholder="">
                                    </textarea>  
                                </div>
                            </div>
                            <div class="general-row flex">
                                <div class="w-1/2 m-r-12" style="position:relative;">
                                    <p>Giá trị tối thiểu</p>
                                    <input :readonly="readonly" v-model="discountCondition.DiscountConditionMinNumber" ref="DiscountConditionMin" id="DiscountConditionMin"
                                        type="text" maxlength="20" tabindex="2" @keyup="formatNumber('DiscountConditionMin')"
                                        class="w-full form-control end">
                                    <p style="width: fit-content;position: absolute;top: 53%;right: 6px;">VND</p>
                                </div>
                                <div class="w-1/2" style="position:relative;">
                                    <p>Giá trị tối đa</p>
                                    <input :readonly="readonly" v-model="discountCondition.DiscountConditionMaxNumber" ref="DiscountConditionMax" id="DiscountConditionMax"
                                        type="text" maxlength="20" tabindex="2" @keyup="formatNumber('DiscountConditionMax')"
                                        class="w-full form-control end">
                                    <p style="width: fit-content;position: absolute;top: 53%;right: 6px;">VND</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="line"></div>
                <div class="form-footer flex">
                    <div class="admin-btn-normal btn-delete" tabindex="18" @click="btnXForm()" @keyup.enter="btnXForm()">Hủy</div>
                    <div class="btn-save-all flex">
                        <!-- :disable="readonly" -->
                        <div class="admin-btn-normal btn-save" tabindex="16" v-if="!readonly"
                             @click="crudObject()"
                             @keyup.enter="crudObject()"
                             v-shortkey="['ctrl','s']" @shortkey="crudObject()"
                             v-tooltip.top="{content:'Cất (Ctrl + S)', classes:'tooltip', offset: '5px'}">Cất</div>
                        <div class="admin-btn-normal admin-btn-primary" v-if="!readonly" 
                             tabindex="17" :disable="readonly"
                             @click="post_save=true,crudObject()" 
                             @keyup.enter="post_save = true,crudObject()"
                             v-shortkey="['ctrl','shift','s']" @shortkey="post_save = true,crudObject()"
                             v-tooltip.top="{content:'Cất và thêm (Ctrl+ Shift + S)', classes:'tooltip', offset: '5px'}">Cất và Thêm</div>
                        <div class="admin-btn-normal admin-btn-primary" v-if="readonly" @click="changeCrud('put')">Sửa</div>
                    </div>
                </div>
            </form>
        </div>
    </div>
    <div>
        <div v-for="(mgs, index) in listMgs" :key="index">
            <toast-message :titleMgs="mgs.titleMgs" :iconToast="mgs.iconToast" v-if="mgs.showToast" @deleteToast="deleteToast(index)"></toast-message>
        </div>
        <v-popup v-if="popShow" :popShow="popShow" :action="action" :idInvalid="idInvalid" @closeForm="closeForm" :message="message" :crud="crud" @closePop="closePop" @crudObject="crudObject()"></v-popup>
    </div>
</div>
</template>

<style>
</style>

<script>

import {
    required,
} 
from "vuelidate/lib/validators";
import DiscountConditionAPI from '../../../../../api/component/bussiness-item/DiscountConditionAPI'
import { CRUD, PopupState } from '../../../../../base/Resources.js';
import VPopup from '@/components/base/VPopup';
import VSelect from '../../../../base/VSelect.vue';
import Base from '../../../../../base/Base';
export default {
    name:'DiscountConditionDetail',
    components:{
        VPopup,
        VSelect
    },
    props:{
        entityDetail: Object,
        crud: String,
    },

    async mounted() {
        if((this.crud == CRUD.Post)||(this.crud == CRUD.Copy)){
            await this.getNewCode();
        }
        this.$nextTick(() => {
            this.$refs.DiscountConditionCode.focus();
        })
    },

    created(){
        if(this.discountCondition){
            this.discountCondition.DiscountConditionMinNumber = Base.formatNumber(this.discountCondition.DiscountConditionMin);
            this.discountCondition.DiscountConditionMaxNumber = Base.formatNumber(this.discountCondition.DiscountConditionMax);
        }
        if(this.crud == CRUD.Post){
            this.discountCondition.DiscountConditionFor = 'Người dùng';
        }
    },

    data(){
        return {
            readonly: false,
            popShow: false,
            // action post, put, copy
            action: "",
            // message trong popup
            message:"",
            // hiển thị loading
            loader:false,
            loaderUrl: require('../../../../../assets/lib/img/common/loading.svg'),
            // toast
            mgs: {
                titleMgs: "",
                showToast: false,
                iconToast: "",
            },
            listMgs: [],
            post_save: false,
            // id của ô input invalid đầu tiên
            idInvalid:"",
            discountCondition: Object.assign({},this.entityDetail),
        }
    },
    // rule của các input
    validations: {
        discountCondition: {
            DiscountConditionCode: {
                required
            },
            DiscountConditionName:{
                required,
            },
        }
    },
    methods:{

        /**
         * validate khi nhấn click
         * Create: PTDuyen(18/08/2021)
         */
        validateForm() {
            this.idInvalid = "";
            this.$v.$touch();
            // nếu dữ liệu không hợp lệ
            if (this.$v.$invalid) {
                // console.log(this.$v.discount);
                for(let key in this.discountCondition){
                    if(this.$v.discountCondition[key]){
                        if(this.$v.discountCondition[key].$error == true){
                            this.idInvalid = key;
                            this.showPopup(PopupState.Invalid);
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

        /**
         * lấy mã code mới
         * Create: PTDuyen(18/08/2021)
         */
        async getNewCode(){
            try{
                // lấy mã nhân viên mới
                var res = await DiscountConditionAPI.getNewCode();
                this.discountCondition.DiscountConditionCode = res;
            }
            // nếu lấy mã mới thất bại
            catch(res){
                console.log(res);
            }
        },

        /**
         * Mở popup
         * Create: PTDuyen(11/09/2021)
         */
        showPopup(action) {
            this.popShow = true;
            this.action = action;
        },

        formatNumber(item){
            this.discountCondition[item] = Base.formatToNumber(this.discountCondition[item+'Number']);
            this.discountCondition[item+'Number'] = Base.formatNumber(this.discountCondition[item]);
            document.getElementById(item).value = this.discountCondition[item+'Number'];
        },

        /**
         * Đóng popup
         * Create: PTDuyen(17/08/2021)
         */
        closePop(action) {
            if(action == PopupState.Invalid){
                this.$refs[this.idInvalid].focus();
            }
            else if(action == PopupState.Duplicate){
                this.$refs.DiscountConditionCode.focus();
                this.$refs.DiscountConditionCode.classList.remove("is-invalid");
            }
            this.popShow = false;
        },
        /**
         * Đóng form bằng nút hủy hoặc x(khi có dữ liệu thay đổi)
         * Create: PTDuyen(11/09/2021)
         */
        btnXForm(){
            this.$emit('btnXForm',this.discountCondition, this.entityDetail, this.crud, 'DiscountConditionCode');
        },
        /**
         * gọi đến hàm closeForm của discountList
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
        crudObject(){
            try{
                this.$emit('crudObject', this.discountCondition);
            }
            catch(ex){
                console.log(ex)
            }
        },
    },
}
</script>

<style>
.discount-condition-detail .add-entity,
.discount-condition-detail .form-init{
    overflow: hidden;
}
.discount-object .v-input.v-select{
    padding: 0 !important;
}
/* .discount-condition-detail .general-row input.end{
    padding: 0 10px !important;
} */
</style>