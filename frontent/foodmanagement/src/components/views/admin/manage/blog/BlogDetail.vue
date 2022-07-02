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
    <div class="add-item blog-item">
        <div class="add-entity">
             <form class="form-add" id="scroll-thumb" autocomplete="off">
                <div class="form-header">
                    <h2 class="form-title">Thông tin bài viết</h2>
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
                                <div class="row1-input1 w-1/3 m-r-12">
                                    <p>Mã bài viết <span>*</span></p>
                                    <input :readonly="readonly" v-model="blog.BlogCode" ref="BlogCode" id="BlogCode"
                                           type="text" maxlength="20" tabindex="2" 
                                           class="w-full form-control" :class="{ 'is-invalid': $v.blog.BlogCode.$error }"
                                           @blur="$v.blog.BlogCode.$touch()">
                                    <div v-if="$v.blog.BlogCode.$error" class="invalid-feedback">
                                        <p v-if="!$v.blog.BlogCode.required">Mã bài viết không được để trống.</p>
                                        <p v-if="($v.blog.BlogCode.required) && (!$v.blog.BlogCode.startWithBC)">Mã bài viết phải bắt đầu bằng <i>'BC-'.</i></p>
                                    </div>
                                </div>
                                <div class="w-1/3 m-r-12">
                                    <p>Chủ đề <span>*</span></p>
                                    <input v-model="blog.BlogTopic" id="BlogTopic"
                                            ref="BlogTopic" :readonly="readonly"
                                            type="text" maxlength="100" tabindex="2" 
                                            class="w-full form-control" :class="{ 'is-invalid': $v.blog.BlogTopic.$error }"
                                            @blur="$v.blog.BlogTopic.$touch()">
                                    <div v-if="$v.blog.BlogTopic.$error" class="invalid-feedback">
                                        <p v-if="!$v.blog.BlogTopic.required">Tên bài viết không được để trống.</p>
                                    </div>
                                </div>
                                <div class="w-1/3">
                                    <p>Tiêu đề</p>
                                    <input :readonly="readonly" class="w-full" rows="2" tabindex="4"  v-model="blog.BlogTitle"
                                                placeholder="">
                                </div>
                            </div>
                            <div class="general-row flex">
                                <div class="w-1/2 m-r-12">
                                    <p>Mở đầu</p>
                                    <textarea :readonly="readonly" class="w-full" rows="2" tabindex="4"  v-model="blog.BlogIntro"
                                                placeholder="">
                                    </textarea>
                                </div>
                                <div class="w-1/2">
                                    <p>Nội dung khác</p>
                                    <textarea :readonly="readonly" class="w-full" rows="2" tabindex="4"  v-model="blog.BlogOther"
                                                placeholder="">
                                    </textarea>
                                </div>
                            </div>
                            <div class="general-row flex">
                                <div class="w-1/2 m-r-12">
                                    <p>Trích dẫn</p>
                                    <textarea :readonly="readonly" class="w-full" rows="2" maxlength="255" tabindex="4"  v-model="blog.BlogQuote"
                                                placeholder="">
                                    </textarea>
                                </div>
                                <div class="w-1/2">
                                    <p>Nội dung nhấn mạnh</p>
                                    <textarea :readonly="readonly" class="w-full" rows="2" maxlength="255" tabindex="4"  v-model="blog.BlogHighlight"
                                                placeholder="">
                                    </textarea>
                                </div>
                            </div>
                            <div class="general-row">
                                <p>Nội dung <span>*</span></p>
                                <textarea :readonly="readonly" rows="2" tabindex="4"  v-model="blog.BlogContent"
                                          placeholder="" ref="BlogContent" id="BlogContent"
                                          class="w-full form-control" :class="{ 'is-invalid': $v.blog.BlogContent.$error }"
                                          @blur="$v.blog.BlogContent.$touch()">
                                </textarea>
                                <div v-if="$v.blog.BlogContent.$error" class="invalid-feedback">
                                    <p v-if="!$v.blog.BlogContent.required">Nội dung bài viết không được để trống.</p>
                                </div>
                            </div>
                            <div class="general-row flex">
                                <div class="summary w-3/5 m-r-12">
                                    <p>Kết luận</p>
                                    <textarea :readonly="readonly" class="w-full" rows="2" maxlength="255" tabindex="4"  v-model="blog.BlogOther"
                                                placeholder="">
                                    </textarea>
                                </div>
                                <div class="upload-image w-2/5">
                                    <p>Hình ảnh</p>
                                    <div class="flex" style="min-height: 56px;">
                                        <div class="flex-center m-r-12" style="min-width: fit-content;flex-direction: column;justify-content: space-evenly;">
                                            <input :readonly="readonly" id="upload-file" @change="getFile('#img-blog', $event)" ref="BlogImage" type="file">
                                            <label class="pointer m-r-12" for="upload-file">Tải ảnh</label>
                                            <div class="pointer" id="delete-image" @click="deleteFile('#img-blog')">Xóa ảnh</div>
                                        </div>
                                        <img :src="blog.BlogImage" alt="" style="width:157px;border-radius: 5px;" id="img-blog">
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
.blog-item .add-entity{
    width: 900px;
}

.blog-item textarea::-webkit-scrollbar {
    width: 0px;
}
</style>

<script>

import {
    required,
} 
from "vuelidate/lib/validators";
import BlogAPI from '../../../../../api/component/bussiness-item/BlogAPI'
import { PopupState } from '../../../../../base/Resources.js';
import Base from '../../../../../base/Base.js';
//import Base from '../../../../base/Base.js';
import VPopup from '@/components/base/VPopup';
export default {
    name:'BlogDetail',
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
            blog: Object.assign({},this.entityDetail),
        }
    },
    // rule của các input
    validations: {
        blog: {
            BlogCode: {
                required,
                startWithBC(){
                    if(this.blog.BlogCode.startsWith('BC-')) return true;
                    else return false;
                }
            },
            BlogContent: {
                required,
            },
            BlogTopic: {
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
                // console.log(this.$v.blog);
                for(let key in this.blog){
                    if(this.$v.blog[key]){
                        if(this.$v.blog[key].$error == true){
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
                this.$refs.BlogDetail.focus();
                this.$refs.BlogDetail.classList.remove("is-invalid");
            }
            this.popShow = false;
        },
        /**
         * Đóng form bằng nút hủy hoặc x(khi có dữ liệu thay đổi)
         * Create: PTDuyen(11/09/2021)
         */
        btnXForm(){
            this.$emit('btnXForm',this.blog, this.entityDetail, this.crud, 'BlogCode');
        },
        /**
         * gọi đến hàm closeForm của blogList
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
            if(this.$refs.BlogImage.files && this.$refs.BlogImage.files[0]){
                let formData = new FormData();
                formData.append('file', this.$refs.BlogImage.files[0])
                this.blog.BlogImage = await BlogAPI.uploadFile(formData);
            }
            else {
                this.blog.BlogImage = '';
            }
            this.$emit('crudObject', this.blog);
        },

        getFile(imgSelector, event){
            Base.getFile(imgSelector, event);
        },
        deleteFile(imgSelector){
            this.blog.BlogImage = '';
            this.$el.querySelector(imgSelector).setAttribute('src', '');
        },
    },
}
</script>

<style>

</style>