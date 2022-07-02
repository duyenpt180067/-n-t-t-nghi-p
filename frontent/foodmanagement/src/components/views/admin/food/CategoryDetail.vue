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
    <div class="add-item">
        <div class="add-entity">
            <form class="form-add" id="scroll-thumb" autocomplete="off">
                <div class="form-header">
                    <h2 class="form-title">Thông tin danh mục</h2>
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
                            <div class="general-row">
                                <div class="row1-input1 code">
                                    <p>Mã danh mục <span>*</span></p>
                                    <input :readonly="readonly" v-model="category.CategoryCode" ref="CategoryCode" id="CategoryCode"
                                           type="text" maxlength="20" tabindex="2" 
                                           class="w-full form-control" :class="{ 'is-invalid': $v.category.CategoryCode.$error }"
                                           @blur="$v.category.CategoryCode.$touch()">
                                    <div v-if="$v.category.CategoryCode.$error" class="invalid-feedback">
                                        <p v-if="!$v.category.CategoryCode.required">Mã danh mục không được để trống.</p>
                                        <p v-if="($v.category.CategoryCode.required) && (!$v.category.CategoryCode.startWithCM)">Mã danh mục phải bắt đầu bằng <i>'CM-'.</i></p>
                                    </div>
                                </div>
                            </div>
                             <div class="general-row">
                                <p>Tên danh mục <span>*</span></p>
                                <input v-model="category.CategoryName" id="CategoryName"
                                        ref="CategoryName" :readonly="readonly"
                                        type="text" maxlength="100" tabindex="2" 
                                        class="w-full form-control" :class="{ 'is-invalid': $v.category.CategoryName.$error }"
                                        @blur="$v.category.CategoryName.$touch()">
                                <div v-if="$v.category.CategoryName.$error" class="invalid-feedback">
                                    <p v-if="!$v.category.CategoryName.required">Tên danh mục không được để trống.</p>
                                </div>
                            </div>
                            <div class="general-row flex">
                                <div class="description w-2/5 m-r-12">
                                    <p>Mô tả</p>
                                    <textarea :readonly="readonly" class="w-full" rows="2" maxlength="255" tabindex="4"  v-model="category.Descriptions"
                                                placeholder="">
                                    </textarea>
                                </div>
                                <div class="upload-image w-3/5">
                                    <p>Hình ảnh</p>
                                    <div class="flex" style="min-height: 56px;">
                                        <div class="flex-center m-r-12" style="min-width: fit-content;flex-direction: column;justify-content: space-evenly;">
                                            <input :readonly="readonly" id="upload-file" @change="getFile('#img-category', $event)" ref="CategoryImage" type="file">
                                            <label class="pointer m-r-12" for="upload-file">Tải ảnh</label>
                                            <div class="pointer" id="delete-image" @click="deleteFile('#img-category')">Xóa ảnh</div>
                                        </div>
                                        <img :src="category.CategoryImage" alt="" style="width:157px;border-radius: 5px;" id="img-category">
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

<script>

import {
    required,
} 
from "vuelidate/lib/validators";
import CategoryAPI from '../../../../api/component/food/CategoryAPI'
import { PopupState } from '../../../../base/Resources.js';
import Base from '../../../../base/Base.js';
//import Base from '../../../../base/Base.js';
import VPopup from '@/components/base/VPopup';
export default {
    name:'CategoryDetail',
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
            loaderUrl: require('../../../../assets/lib/img/common/loading.svg'),
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
            category: Object.assign({},this.entityDetail),
        }
    },
    // rule của các input
    validations: {
        category: {
            CategoryCode: {
                required,
                startWithCM(){
                    if(this.category.CategoryCode.startsWith('CM-')) return true;
                    else return false;
                }
            },
            CategoryName: {
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
                // console.log(this.$v.category);
                for(let key in this.category){
                    if(this.$v.category[key]){
                        if(this.$v.category[key].$error == true){
                            this.idInvalid = key;
                            this.$emit('showPopup', PopupState.Invalid);
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
         * Đóng popup
         * Create: PTDuyen(17/08/2021)
         */
        closePop(action) {
            if(action == PopupState.Invalid){
                this.$refs[this.idInvalid].focus();
            }
            else if(action == PopupState.Duplicate){
                this.$refs.CategoryDetail.focus();
                this.$refs.CategoryDetail.classList.remove("is-invalid");
            }
            this.popShow = false;
        },
        /**
         * Đóng form bằng nút hủy hoặc x(khi có dữ liệu thay đổi)
         * Create: PTDuyen(11/09/2021)
         */
        btnXForm(){
            this.$emit('btnXForm',this.category, this.entityDetail, this.crud, 'CategoryCode');
        },
        /**
         * gọi đến hàm closeForm của categoryList
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
        async crudObject(){
            if(this.$refs.CategoryImage.files && this.$refs.CategoryImage.files[0]){
                let formData = new FormData();
                formData.append('file', this.$refs.CategoryImage.files[0])
                this.category.CategoryImage = await CategoryAPI.uploadFile(formData);
            }
            else {
                this.category.CategoryImage = '';
            }
            this.$emit('crudObject', this.category);
        },

        getFile(imgSelector, event){
            Base.getFile(imgSelector, event);
        },
        deleteFile(imgSelector){
            this.category.CategoryImage = '';
            this.$el.querySelector(imgSelector).setAttribute('src', '');
        }
    },
}
</script>

<style>

</style>