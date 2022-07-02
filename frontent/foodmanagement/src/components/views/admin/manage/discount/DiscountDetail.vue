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
    <div class="add-item discount-item">
        <div class="add-entity">
             <form class="form-add" id="scroll-thumb" autocomplete="off">
                <div class="form-header">
                    <h2 class="form-title">Chi tiết mã giảm giá</h2>
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
                                    <p>Mã giảm giá <span>*</span></p>
                                    <input :readonly="readonly" v-model="discount.DiscountCode" ref="DiscountCode" id="DiscountCode"
                                        type="text" maxlength="20" tabindex="2" 
                                        class="w-full form-control" :class="{ 'is-invalid': $v.discount.DiscountCode.$error }"
                                        @blur="$v.discount.DiscountCode.$touch()">
                                    <div v-if="$v.discount.DiscountCode.$error" class="invalid-feedback">
                                        <p v-if="!$v.discount.DiscountCode.required">Mã giảm giá không được để trống.</p>
                                    </div>
                                </div>
                                <div class="w-1/2">
                                    <p>Tiêu đề</p>
                                    <input v-model="discount.DiscountTitle" id="DiscountName"
                                            ref="DiscountName" :readonly="readonly"
                                            type="text" maxlength="255" tabindex="2" 
                                            class="w-full form-control">
                                </div>
                            </div>
                            <div class="general-row flex">
                                <div class="w-1/2 m-r-12 page-size">
                                    <p>Loại áp dụng</p>
                                    <v-select :item_text="'text'" :items="listDiscountType" :vmodel="discountType_model" @getSelect="discountType_model=$event;discount.DiscountType=$event.value;"></v-select>
                                </div>
                                <div class="w-1/2 flex">
                                    <div class="w-1/2 m-r-12" style="position:relative;">
                                        <p>Giảm giá <span>*</span></p>
                                        <input :readonly="readonly" v-model="discount.DiscountAmount" ref="DiscountAmount" id="DiscountAmount"
                                            type="number" maxlength="20" tabindex="2" max="100" min="0"
                                            class="form-control end" :class="{ 'is-invalid': $v.discount.DiscountAmount.$error }"
                                            @blur="$v.discount.DiscountAmount.$touch()"> 
                                        <div v-if="$v.discount.DiscountAmount.$error" class="invalid-feedback">
                                            <p v-if="!$v.discount.DiscountAmount.required">Giảm giá không được để trống.</p>
                                        </div>
                                        <p style="width: fit-content;position: absolute;top: 53%;right: 6px;">%</p>
                                    </div>
                                    <div class="w-1/2" style="position:relative;">
                                        <p>Giảm tối đa</p>
                                        <input :readonly="readonly" v-model="discount.DiscountMaxNumberAmount" ref="DiscountMaxAmount" id="DiscountMaxAmount"
                                            type="text" maxlength="20" tabindex="2" @keyup="formatNumber"
                                            class="w-full form-control end">
                                        <p style="width: fit-content;position: absolute;top: 53%;right: 6px;">VND</p>
                                    </div>
                                </div>
                            </div>
                            <div class="general-row flex">
                                <div class="w-1/2 m-r-12">
                                    <p>Ngày bắt đầu</p>
                                    <v-datepicker :dataDate="discount.DiscountStartDate" 
                                                    @changeDate="changeDate($event, 'DiscountStartDate')" 
                                                    :checkDateValid="true">
                                    </v-datepicker>
                                </div>
                                <div class="w-1/2">
                                    <p>Ngày kết thúc</p>
                                    <v-datepicker :dataDate="discount.DiscountEndDate" 
                                                @changeDate="changeDate($event, 'DiscountEndDate')" 
                                                :checkDateValid="true">
                                    </v-datepicker>
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
import DiscountAPI from '../../../../../api/component/bussiness-item/DiscountAPI'
import { CRUD, PopupState } from '../../../../../base/Resources.js';
import VPopup from '@/components/base/VPopup';
import VDatepicker from '@/components/base/VDatepicker';
import Base from '../../../../../base/Base';
import { DiscountType } from '../../../../../base/vi/Resources';
import VSelect from '../../../../base/VSelect.vue';
export default {
    name:'DiscountDetail',
    components:{
        VPopup,
        VDatepicker,
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
            this.$refs.DiscountCode.focus();
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
            discount: Object.assign({},this.entityDetail),
            listDiscountType: DiscountType,
            discountType_model:DiscountType[1],
        }
    },
    // rule của các input
    validations: {
        discount: {
            DiscountCode: {
                required
            },
            DiscountAmount:{
                required,
            },
        }
    },
    created(){
        this.listDiscountType = DiscountType.filter(e=>e.value!=null);
        this.discountType_model = this.listDiscountType[0];
        if(this.discount){
            this.discount.DiscountMaxNumberAmount = this.discount.DiscountMaxAmount ? Base.formatNumber(this.discount.DiscountMaxAmount) : null;
            this.discountType_model = DiscountType.filter(e=>e.value==this.discount.DiscountType)[0];
        }
    },
    methods:{

        /**
         * validate khi nhấn click
         * Create: PTDuyen(18/08/2021)
         */
        validateForm() {
            console.log("oke");
            this.idInvalid = "";
            this.$v.$touch();
            // nếu dữ liệu không hợp lệ
            if (this.$v.$invalid) {
                // console.log(this.$v.discount);
                for(let key in this.discount){
                    if(this.$v.discount[key]){
                        if(this.$v.discount[key].$error == true){
                            this.idInvalid = key;
                            console.log(key);
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
         * Lay ngày của dateFilter
         * Create:PTDuyen(17/10/2021)
         */
        changeDate(date, model){
            if(date){
                this.discount[model] = date;
            }
            else {
                this.discount[model] = null;
            }
        },

        /**
         * lấy mã code mới
         * Create: PTDuyen(18/08/2021)
         */
        async getNewCode(){
            try{
                // lấy mã nhân viên mới
                var res = await DiscountAPI.getNewCode();
                this.discount.DiscountCode = res;
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

        formatNumber(){
            this.discount.DiscountMaxAmount = Base.formatToNumber(this.discount.DiscountMaxNumberAmount);
            this.discount.DiscountMaxNumberAmount = Base.formatNumber(this.discount.DiscountMaxAmount);
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
                this.$refs.DiscountCode.focus();
                this.$refs.DiscountCode.classList.remove("is-invalid");
            }
            this.popShow = false;
        },
        /**
         * Đóng form bằng nút hủy hoặc x(khi có dữ liệu thay đổi)
         * Create: PTDuyen(11/09/2021)
         */
        btnXForm(){
            this.$emit('btnXForm',this.discount, this.entityDetail, this.crud, 'DiscountCode');
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
                this.$emit('crudObject', this.discount);
            }
            catch(ex){
                console.log(ex)
            }
        },
    },
}
</script>

<style>
.general-row input.end{
    padding: 0 35px 0 10px !important;
}
</style>