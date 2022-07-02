<template>
<div class="detail">
    <!-- loading -->
    <div v-if="loader" :class="{'background-loader':loader}">
        <div :class="{loader:loader}">
            <object :data="loaderUrl"></object>
        </div>
    </div>
    <!-- background -->
    <div class="cover-background"></div>
    <div class="add-item faq-item">
        <div class="add-entity">
             <form class="form-add" id="scroll-thumb" autocomplete="off">
                <div class="form-header">
                    <h2 class="form-title">Chi tiết câu hỏi</h2>
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
                                <div class="row1-input1 w-1/2 m-r-12 flex">
                                    <div class="w-1/2 m-r-12">
                                        <p>Mã câu hỏi <span>*</span></p>
                                        <input :readonly="readonly" v-model="faq.FaqCode" ref="FaqCode" id="FaqCode"
                                            type="text" maxlength="20" tabindex="2" 
                                            class="w-full form-control" :class="{ 'is-invalid': $v.faq.FaqCode.$error }"
                                            @blur="$v.faq.FaqCode.$touch()">
                                        <div v-if="$v.faq.FaqCode.$error" class="invalid-feedback">
                                            <p v-if="!$v.faq.FaqCode.required">Mã câu hỏi không được để trống.</p>
                                            <p v-if="($v.faq.FaqCode.required) && (!$v.faq.FaqCode.startWithFAC)">Mã Faq phải bắt đầu bằng <i>'FAC-'.</i></p>
                                        </div>
                                    </div>
                                    <div class="w-1/2">
                                        <p>Thứ tự</p>
                                        <input v-model="faq.Priority" id="Priority"
                                                ref="Priority" :readonly="readonly"
                                                type="text" maxlength="255" tabindex="2" 
                                                class="w-full form-control">
                                    </div>
                                </div>
                                <div class="w-1/2">
                                    <p>Tiêu đề</p>
                                    <input v-model="faq.FaqName" id="FaqName"
                                            ref="FaqName" :readonly="readonly"
                                            type="text" maxlength="255" tabindex="2" 
                                            class="w-full form-control">
                                </div>
                            </div>
                            <div class="general-row">
                                <p>Nội dung câu hỏi <span>*</span></p>
                                <textarea :readonly="readonly" rows="2" tabindex="4"  v-model="faq.FaqQuestion"
                                        placeholder="" ref="FaqQuestion" id="FaqQuestion"
                                        class="w-full form-control" :class="{ 'is-invalid': $v.faq.FaqQuestion.$error }"
                                        @blur="$v.faq.FaqQuestion.$touch()">
                                </textarea>
                                <div v-if="$v.faq.FaqQuestion.$error" class="invalid-feedback">
                                    <p v-if="!$v.faq.FaqQuestion.required">Nội dung câu hỏi không được để trống.</p>
                                </div>
                            </div>
                            <div class="general-row">
                                <p>Trả lời <span>*</span></p>
                                <textarea :readonly="readonly" rows="2" tabindex="4"  v-model="faq.FaqAnswer"
                                        placeholder="" ref="FaqAnswer" id="FaqAnswer"
                                        class="w-full form-control" :class="{ 'is-invalid': $v.faq.FaqAnswer.$error }"
                                        @blur="$v.faq.FaqAnswer.$touch()">
                                </textarea>
                                <div v-if="$v.faq.FaqAnswer.$error" class="invalid-feedback">
                                    <p v-if="!$v.faq.FaqAnswer.required">Nội dung trả lời câu hỏi không được để trống.</p>
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
import FaqAPI from '../../../../../api/component/bussiness-item/FaqAPI'
import { CRUD, PopupState } from '../../../../../base/Resources.js';
import Base from '../../../../../base/Base.js';
//import Base from '../../../../base/Base.js';
import VPopup from '@/components/base/VPopup';
export default {
    name:'FaqDetail',
    components:{
        VPopup
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
            this.$refs.FaqCode.focus();
        })
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
            faq: Object.assign({},this.entityDetail),
        }
    },
    // rule của các input
    validations: {
        faq: {
            FaqCode: {
                required,
                startWithFAC(){
                    if(this.faq.FaqCode.startsWith('FAC-')) return true;
                    else return false;
                }
            },
            FaqQuestion: {
                required,
            },
            FaqAnswer: {
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
                // console.log(this.$v.faq);
                for(let key in this.faq){
                    if(this.$v.faq[key]){
                        if(this.$v.faq[key].$error == true){
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
                var res = await FaqAPI.getNewCode();
                this.faq.FaqCode = res;
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

        /**
         * Đóng popup
         * Create: PTDuyen(17/08/2021)
         */
        closePop(action) {
            if(action == PopupState.Invalid){
                this.$refs[this.idInvalid].focus();
            }
            else if(action == PopupState.Duplicate){
                this.$refs.FaqCode.focus();
                this.$refs.FaqCode.classList.remove("is-invalid");
            }
            this.popShow = false;
        },
        /**
         * Đóng form bằng nút hủy hoặc x(khi có dữ liệu thay đổi)
         * Create: PTDuyen(11/09/2021)
         */
        btnXForm(){
            this.$emit('btnXForm',this.faq, this.entityDetail, this.crud, 'FaqCode');
        },
        /**
         * gọi đến hàm closeForm của faqList
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
                this.$emit('crudObject', this.faq);
            }
            catch(ex){
                console.log(ex)
            }
        },

        getFile(imgSelector, event){
            Base.getFile(imgSelector, event);
        },
        deleteFile(imgSelector){
            this.faq.FaqImage = '';
            this.$el.querySelector(imgSelector).setAttribute('src', '');
        },
    },
}
</script>

<style>

</style>