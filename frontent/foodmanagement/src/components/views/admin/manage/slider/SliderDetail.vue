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
    <div class="add-item slider-item">
        <div class="add-entity">
             <form class="form-add" id="scroll-thumb" autocomplete="off">
                <div class="form-header">
                    <h2 class="form-title">Thông tin Slider</h2>
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
                                <div class="row1-input1 w-1/2 m-r-12">
                                    <p>Mã Slider <span>*</span></p>
                                    <input :readonly="readonly" v-model="slider.SliderCode" ref="SliderCode" id="SliderCode"
                                           type="text" maxlength="20" tabindex="2" 
                                           class="w-full form-control" :class="{ 'is-invalid': $v.slider.SliderCode.$error }"
                                           @blur="$v.slider.SliderCode.$touch()">
                                    <div v-if="$v.slider.SliderCode.$error" class="invalid-feedback">
                                        <p v-if="!$v.slider.SliderCode.required">Mã Slider không được để trống.</p>
                                        <p v-if="($v.slider.SliderCode.required) && (!$v.slider.SliderCode.startWithSC)">Mã Slider phải bắt đầu bằng <i>'SC-'.</i></p>
                                    </div>
                                </div>
                                <div class="w-1/2">
                                    <p>Tên Slider <span>*</span></p>
                                    <input v-model="slider.SliderName" id="SliderName"
                                            ref="SliderName" :readonly="readonly"
                                            type="text" maxlength="100" tabindex="2" 
                                            class="w-full form-control" :class="{ 'is-invalid': $v.slider.SliderName.$error }"
                                            @blur="$v.slider.SliderName.$touch()">
                                    <div v-if="$v.slider.SliderName.$error" class="invalid-feedback">
                                        <p v-if="!$v.slider.SliderName.required">Tên Slider không được để trống.</p>
                                    </div>
                                </div>
                            </div>
                            <div class="general-row flex">
                                <div class="w-1/2 m-r-12">
                                    <p>Tiêu đề</p>
                                    <input :readonly="readonly" class="w-full" rows="2" tabindex="4"  v-model="slider.SliderTitle"
                                                placeholder="">
                                </div>
                                <div class="w-1/2">
                                    <p>Link</p>
                                    <input :readonly="readonly" class="w-full" rows="2" tabindex="4"  v-model="slider.SliderLink"
                                                placeholder="">
                                </div>
                            </div>
                            <div class="general-row flex">
                                <div class="summary w-3/5 m-r-12">
                                    <p>Nội dung <span>*</span></p>
                                    <textarea :readonly="readonly" rows="2" tabindex="4"  v-model="slider.SliderContent"
                                            placeholder="" ref="SliderContent" id="SliderContent"
                                            class="w-full form-control" :class="{ 'is-invalid': $v.slider.SliderContent.$error }"
                                            @blur="$v.slider.SliderContent.$touch()">
                                    </textarea>
                                    <div v-if="$v.slider.SliderContent.$error" class="invalid-feedback">
                                        <p v-if="!$v.slider.SliderContent.required">Nội dung Slider không được để trống.</p>
                                    </div>
                                </div>
                                <div class="upload-image w-2/5">
                                    <p>Hình ảnh</p>
                                    <div class="flex" style="min-height: 56px;">
                                        <div class="flex-center m-r-12" style="min-width: fit-content;flex-direction: column;justify-content: space-evenly;">
                                            <input :readonly="readonly" id="upload-file" @change="getFile('#img-slider', $event)" ref="SliderImage" type="file">
                                            <label class="pointer m-r-12" for="upload-file">Tải ảnh</label>
                                            <div class="pointer" id="delete-image" @click="deleteFile('#img-slider')">Xóa ảnh</div>
                                        </div>
                                        <img :src="slider.SliderImage" alt="" style="width:157px;border-radius: 5px;" id="img-slider">
                                    </div>
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
import SliderAPI from '../../../../../api/component/bussiness-item/SliderAPI'
import { CRUD, PopupState } from '../../../../../base/Resources.js';
import Base from '../../../../../base/Base.js';
//import Base from '../../../../base/Base.js';
import VPopup from '@/components/base/VPopup';
export default {
    name:'SliderDetail',
    components:{
        VPopup
    },
    props:{
        entityDetail: Object,
        crud: String,
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
            slider: Object.assign({},this.entityDetail),
        }
    },
    // rule của các input
    validations: {
        slider: {
            SliderCode: {
                required,
                startWithSC(){
                    if(this.slider.SliderCode.startsWith('SC-')) return true;
                    else return false;
                }
            },
            SliderContent: {
                required,
            },
            SliderName: {
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
                // console.log(this.$v.slider);
                for(let key in this.slider){
                    if(this.$v.slider[key]){
                        if(this.$v.slider[key].$error == true){
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
                this.$refs.SliderCode.focus();
                this.$refs.SliderCode.classList.remove("is-invalid");
            }
            this.popShow = false;
        },
        /**
         * Đóng form bằng nút hủy hoặc x(khi có dữ liệu thay đổi)
         * Create: PTDuyen(11/09/2021)
         */
        btnXForm(){
            this.$emit('btnXForm',this.slider, this.entityDetail, this.crud, 'SliderCode');
        },
        /**
         * gọi đến hàm closeForm của sliderList
         * Create: PTDuyen(11/09/2021)
         */
        closeForm() {
            console.log("ok")
            this.$emit('closeForm');    
        },
        /**
         * delete toast message
         * Create: PTDuyen(13/09/2021)
         */
        deleteToast(index) {
            this.listMgs.splice(index, 1);
        },
        async crudObject(){
            try{
                if(this.$refs.SliderImage.files && this.$refs.SliderImage.files[0]){
                    let formData = new FormData();
                    formData.append('file', this.$refs.SliderImage.files[0])
                    this.slider.SliderImage = await SliderAPI.uploadFile(formData);
                }
                else {
                    if(this.crud == CRUD.Post)
                        this.slider.SliderImage = null;
                }
                this.$emit('crudObject', this.slider);
            }
            catch(ex){
                console.log(ex)
            }
        },

        getFile(imgSelector, event){
            Base.getFile(imgSelector, event);
        },
        deleteFile(imgSelector){
            this.slider.SliderImage = '';
            this.$el.querySelector(imgSelector).setAttribute('src', '');
        },
    },
}
</script>

<style>

</style>