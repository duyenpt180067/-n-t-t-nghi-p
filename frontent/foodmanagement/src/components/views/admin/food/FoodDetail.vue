<template>
<div class="detail food-detail">
    <!-- loading -->
    <div v-if="loader" :class="{'background-loader':loader}">
        <div :class="{loader:loader}">
            <object :data="loaderUrl"></object>
        </div>
    </div>
    <div class="add-item">
        <div class="flex content-head">
            <div class="content-title">Chi tiết sản phẩm</div>
        </div>
        <div class="add-entity">
            <div class="flex">
                <div class="w-3/5">
                    <div class="flex">
                        <div class="general-row w-1/2 m-r-12">
                            <p>Mã sản phẩm <span>*</span></p>
                            <input :readonly="readonly" v-model="Food.FoodCode" ref="FoodCode" id="FoodCode"
                                type="text" maxlength="20" tabindex="2" 
                                class="w-full form-control" :class="{ 'is-invalid': $v.Food.FoodCode.$error }"
                                @blur="$v.Food.FoodCode.$touch()">
                            <div v-if="$v.Food.FoodCode.$error" class="invalid-feedback">
                                <p v-if="!$v.Food.FoodCode.required">Mã sản phẩm không được để trống.</p>
                                <p v-if="($v.Food.FoodCode.required) && (!$v.Food.FoodCode.startWithFC)">Mã sản phẩm phải bắt đầu bằng <i>'FC-'.</i></p>
                            </div>
                        </div>
                        <div class="general-row w-1/2">
                            <p>Tên sản phẩm <span>*</span></p>
                            <input :readonly="readonly" v-model="Food.FoodName" ref="FoodName" id="FoodName"
                                type="text" maxlength="20" tabindex="2" 
                                class="w-full form-control" :class="{ 'is-invalid': $v.Food.FoodName.$error }"
                                @blur="$v.Food.FoodName.$touch()">
                            <div v-if="$v.Food.FoodName.$error" class="invalid-feedback">
                                <p v-if="!$v.Food.FoodName.required">Tên sản phẩm không được để trống.</p>
                            </div>
                        </div>
                    </div>
                    <div class="general-row">
                        <p>Giới thiệu</p>
                        <input :readonly="readonly" tabindex="2" v-model="Food.Title">
                    </div>
                    <div class="general-row">
                        <p>Danh mục <span>*</span></p>
                        <v-combobox @getSelect="categoryModel=$event;" 
                                    :combobox_valid="false" 
                                    :placeholder="''" 
                                    :readonly="readonly" 
                                    :item_text="['CategoryCode','CategoryName']" 
                                    :display_item="'CategoryName'"
                                    :items="categoryList" 
                                    :groupName="['Mã danh mục','Tên danh mục']"
                                    :vmodel="categoryModel"
                                    :multiple="false">
                        </v-combobox>
                    </div>
                    <div class="general-row">
                        <div class="row2-input" style="position: relative;">
                            <p>Danh sách Topping</p>
                            <!-- <input class="w-full" type="text" maxlength="255"> -->
                            <v-combobox @getSelect="getSelect('topping_model', $event)" 
                                        :combobox_valid="false" 
                                        :placeholder="''" 
                                        :item_text="['ToppingCode','ToppingName', 'ToppingAmount']" 
                                        :items="toppings" 
                                        :vmodel="topping_model"
                                        :multiple="true"
                                        :groupName="['Mã Topping','Tên Topping', 'Giá Topping']"
                                        :display_item="'ToppingName'"
                                        :readonly="readonly">
                            </v-combobox>
                        </div>
                    </div>
                    <div class="general-row">
                        <p>Mô tả</p>
                        <textarea :readonly="readonly" class="w-full" rows="2" v-model="Food.Descriptions" placeholder="">
                        </textarea>                    
                    </div>
                </div>
                <div class="w-2/5 m-l-12">
                    <div class="flex">
                        <!-- <div class="general-row w-1/2 m-r-12">
                            <p>Lượt xem</p>
                            <input readonly="readonly" :value="Food.FoodView">
                        </div> -->
                        <div class="general-row w-1/2" style="position: relative;">
                            <p>Danh sách mã gảm giá</p>
                            <!-- <input class="w-full" type="text" maxlength="255"> -->
                            <v-combobox @getSelect="getSelect('discountModel', $event)" 
                                        :combobox_valid="false" 
                                        :placeholder="''" 
                                        :item_text="['DiscountCode','DiscountAmount', 'DiscountMaxAmount', 'DiscountStartDate', 'DiscountEndDate']" 
                                        :items="discounts" 
                                        :vmodel="discountModel"
                                        :multiple="false"
                                        :groupName="['Mã giảm giá','% giảm giá', 'Giảm tối đa', 'Ngày bắt đầu', 'Ngày kết thúc']"
                                        :display_item="'DiscountCode'"
                                        :readonly="readonly">
                            </v-combobox>
                        </div>
                        <div class="general-row w-1/2 center">
                            <p>Đánh giá</p>
                            <div class="rating">
                                <span class="stars-container" :class="'stars-'+star*20">★★★★★</span>
                            </div>
                        </div>
                    </div>
                    <div class="general-row">
                        <p>Hình ảnh</p>
                        <div class="center" style="">
                            <div class="flex-center m-r-12" style="min-width: fit-content;">
                                <input :readonly="readonly" id="upload-file" @change="getFile('#img-Food', $event)" ref="ImageURL" type="file">
                                <label class="pointer m-r-12" for="upload-file">Tải ảnh</label>
                                <div class="pointer" id="delete-image" @click="deleteFile('#img-Food')">Xóa ảnh</div>
                            </div>
                            <img alt="" style="border-radius: 5px;width:220px;margin-top:10px;" id="img-Food">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="food-table">
            <table>
                <thead>
                    <tr>
                        <th class="center l-30" style="min-width:40px;">#</th>
                        <th style="min-width: 327px;">KÍCH THƯỚC</th>
                        <th style="min-width: 138px;" class="end">ĐƠN GIÁ</th>
                        <th style="min-width: 105px;">XÓA</th>
                        <th class="w-30" style="right:-1px; min-width: 31px;"></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(itemDetail, index) in FoodDetails" :key="index" :class="{'list-input':crudDetail!='read', 'read-input':crudDetail=='read'}">
                        <td class="center l-30">{{index+1}}</td>
                        <td>
                            <v-combobox @getSelect="itemDetail=$event;" 
                                        :combobox_valid="false" 
                                        :placeholder="''" 
                                        :item_text="['SizeCode','SizeName']" 
                                        :items="sizes"
                                        :display_item="'SizeName'" 
                                        :groupName="['Mã kích thước','Tên kích thước']"
                                        :vmodel="itemDetail"
                                        :readonly="readonly" 
                                        :multiple="false">
                            </v-combobox>
                        </td>
                        <td class="end"><input :id="'amountDetail'+index" :readonly="readonly" type="text" style="text-align: end;" v-model="itemDetail.NumberAmount" @keyup="formatNumber(index, itemDetail)"></td>
                        <td class="r-30">
                            <div class="delete-icon other-icon logo-icon pointer" @click="removeDetail(index)"></div>
                        </td>
                        <td class="w-30" style="right:-1px; min-width: 31px;"></td>
                    </tr>
                    <tr class="tr-input input-detail" v-if="showInputDetail">
                        <td class="center l-30">{{FoodDetails.length+1}}</td>
                        <td>
                            <v-combobox @getSelect="FoodDetail=$event;" 
                                        :combobox_valid="false" 
                                        :placeholder="''" 
                                        :item_text="['SizeCode','SizeName']" 
                                        :items="sizes"
                                        :display_item="'SizeName'" 
                                        :groupName="['Mã kích thước','Tên kích thước']"
                                        :vmodel="FoodDetail"
                                        :readonly="readonly" 
                                        :multiple="false">
                            </v-combobox>
                        </td>
                        <td class="end"><input id="amountDetail" :readonly="readonly" type="text" style="text-align: end;" v-model="FoodDetail.NumberAmount" @keyup="formatNumber('detail')"></td>
                        <td class="r-30">
                            <div class="delete-icon other-icon logo-icon pointer" @click="removeDetail('item_detail')"></div>
                        </td>
                        <td class="w-30" style="right:-1px; min-width: 31px;"></td>
                    </tr>
                </tbody>
            </table>
            <div class="table-actions flex">
                <div class="table-action pointer" @click="addDetail();showInputDetail=true">Thêm dòng</div>
                <div class="table-action pointer" @click="removeDetail('all')">Xóa hết dòng</div>
            </div>
        </div>
    </div>
    <div class="form-footer flex" :class="{'form-right':!fadeleft}">
        <div class="admin-btn-normal pointer btn-delete" tabindex="18" @click="btnXForm()" @keyup.enter="btnXForm()">Hủy</div>
        <div class="btn-save-all flex-center" v-if="crudDetail != 'read'">
            <div class="admin-btn-normal btn-save pointer" tabindex="16" @click="crudObject()" @keyup.enter="crudObject()">Cất</div>
            <!-- <div class="btn-footer btn-save-more" tabindex="17" @click="post_save = true;crudObject()" @keyup.enter="post_save = true,crudObject()">Cất và Thêm</div> -->
            <div class="admin-btn-normal admin-btn-primary pointer" v-if="!readonly" 
                    @click="post_save=true,crudObject()"
                    v-shortkey="['ctrl','shift','s']" @shortkey="post_save = true,crudObject()"
                    v-tooltip.top="{content:'Cất và thêm (Ctrl+ Shift + S)', classes:'tooltip', offset: '5px'}">Cất và thêm</div>
        </div>
        <div v-if="crudDetail == 'read'" class="btn-save-all">
            <div class="admin-btn-normal admin-btn-primary pointer" tabindex="16" @click="changeCrud('put')" @keyup.enter="changeCrud('put')">Sửa</div>
        </div>
    </div>
    <div>
        <div v-for="(mgs, index) in listMgs" :key="index">
            <toast-message :titleMgs="mgs.titleMgs" :iconToast="mgs.iconToast" v-if="mgs.showToast" @deleteToast="deleteToast(index)"></toast-message>
        </div>
        <v-popup v-if="popShow" :popShow="popShow" :action="action" :idInvalid="idInvalid" @closeForm="closeForm" :message="message" :crud="crudDetail" @closePop="closePop" @crudObject="crudObject()"></v-popup>
    </div>
</div>
</template>

<script>

import {
    required,
} 
from "vuelidate/lib/validators";
import FoodAPI from '../../../../api/component/food/FoodAPI';
import SizeAPI from '../../../../api/component/additional-element/SizeAPI';
import ToppingAPI from '../../../../api/component/additional-element/ToppingAPI';
import CategoryAPI from '../../../../api/component/food/CategoryAPI';
import { PopupState, CRUD, ToastMessageIcon } from '../../../../base/Resources.js';
import { ToastMgs } from '../../../../base/vi/Resources.js';
import Base from '../../../../base/Base.js';
import VCombobox from '@/components/base/VCombobox';
import VPopup from '@/components/base/VPopup';
import ToastMessage from '@/components/base/ToastMessage';
import DiscountAPI from '../../../../api/component/bussiness-item/DiscountAPI';
export default {
    name:'FoodDetail',
    components:{
        VCombobox,
        ToastMessage,
        VPopup
    },
    props:{
        fadeleft: Boolean,
    },
    async created() {
        try {
            // hiển thị loader
            this.changeLoader(true);
            this.FoodCode = this.$route.params.code;
            if(this.FoodCode){
                this.crudDetail = CRUD.Put;
                this.FoodMerge = await FoodAPI.getFoodByCode(this.FoodCode);
            }
            else {
                this.FoodCode = this.$route.params.copyCode;
                if(this.FoodCode){
                    this.crudDetail = CRUD.Copy;
                    this.FoodMerge = await FoodAPI.getFoodByCode(this.FoodCode);
                    this.FoodMerge.Food.FoodCode = await FoodAPI.getNewCode();
                }
                this.crudDetail = CRUD.Post;
            }
            for (let ind = 0; ind < this.FoodMerge.Comments.length; ind++) {
                this.star += parseInt(this.FoodMerge.Comments[ind].CommentStar);
                
            }
            this.star = this.FoodMerge.Comments.length > 0 ? Math.floor(this.star/this.FoodMerge.Comments.length) : 0;
            if(this.FoodMerge.Food.ImageURL){
                this.$el.querySelector("#img-Food").setAttribute('src', this.FoodMerge.Food.ImageURL);
            }
            this.Food = this.FoodMerge.Food;
            this.categoryModel = {
                CategoryId: this.Food.CategoryId?this.Food.CategoryId:"",
                CategoryName: this.Food.CategoryName?this.Food.CategoryName:"",
                CategoryCode: this.Food.CategoryCode?this.Food.CategoryCode:""
            }
            this.discountModel = {
                DiscountId: this.Food.DiscountId?this.Food.DiscountId:"",
                DiscountCode: this.Food.DiscountCode?this.Food.DiscountCode:"",
                DiscountMaxAmount: this.Food.DiscountMaxAmount?this.Food.DiscountMaxAmount:"",
                DiscountStartDate: this.Food.DiscountStartDate?this.Food.DiscountStartDate:"",
                DiscountEndDate: this.Food.DiscountEndDate?this.Food.DiscountEndDate:"",
                DiscountAmount: this.Food.DiscountAmount?this.Food.DiscountAmount:""
            }
            if(this.FoodMerge.FoodDetails && this.FoodMerge.FoodDetails.length > 0){
                this.FoodMerge.FoodDetails.forEach(element => {
                    element.NumberAmount = Base.formatNumber(element.Amount);
                });
            }
            this.FoodDetails = this.FoodMerge.FoodDetails ? this.FoodMerge.FoodDetails : [];
            let categoryData = await CategoryAPI.getFilterPaging({CategoryFilter:null,CategoryStatus:null});
            this.categoryList = categoryData.data;
            let sizeData = await SizeAPI.getFilterPaging({SizeFilter:null,SizeStatus:null});
            this.sizes = sizeData.data;
            let toppingData = await ToppingAPI.getFilterPaging({ToppingFilter:null,ToppingAmountMin:null,ToppingAmountMax:null,PageNumber:null,PageSize:null});
            this.toppings = toppingData.data;
            for (let index = 0; index < this.toppings.length; index++) {
                this.toppings[index].ToppingAmount = Base.formatNumber(this.toppings[index].ToppingAmount);
            }
            let discountData = await DiscountAPI.getFilterPaging({
                DiscountFilter: null,
                PageNumber: null,
                PageSize: null,
                DiscountFrom: new Date(),
                DiscountEnd: new Date(),
                DiscountAmount: null,
                DiscountOperation: null,
                DiscountMaxAmount: null,
                DiscountType: 0
            })
            this.discounts = discountData.data;
            for (let index = 0; index < this.discounts.length; index++) {
                this.discounts[index].DiscountMaxAmount = Base.formatNumber(this.discounts[index].DiscountMaxAmount);
                this.discounts[index].DiscountStartDate = Base.formatDateToString(this.discounts[index].DiscountStartDate);
                this.discounts[index].DiscountEndDate = Base.formatDateToString(this.discounts[index].DiscountEndDate);
            }
            this.changeLoader(false);
        } catch (error) {
            this.changeLoader(false);
            this.listMgs.push({
                showToast: true,
                titleMgs: "Tải dữ liệu thất bại! Vui lòng kiểm tra lại kết nối mạng hoặc liên hệ để được trợ giúp",
                iconToast: 'error'
            });
        }
    },
    async mounted(){
        this.FoodCode = this.$route.params.code;
        if(!this.FoodCode){
            await this.getNewCode();
        }
        this.$nextTick(() => {
            this.$refs.FoodCode.focus();
        })
    },
    data(){
        return {
            readonly: false,
            popShow: false,
            showInputDetail:true,
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
            categoryList:[],
            FoodMerge: {
                Food: {
                    FoodCode:'',
                    FoodName:'',
                    ListTopping:[],
                    FoodStar:0,
                },
                FoodDetails: []
            },
            FoodDetail:{
                Amount: '',
                NumberAmount:'',
                SizeId: '',
                SizeName: '',
            },
            FoodCode: "",
            dataDetailList: [],
            objectDetailName: 'FoodDetail',
            displayDetailName:'Chi tiết sản phẩm',
            formShow: false,
            crudDetail: '',
            categoryModel:{
                CategoryId:'',
                CategoryName:'',
                CategoryCode:''
            },
            discountModel:{
                DiscountId:'',
                DiscountCode:'',
                DiscountAmount:'',
                DiscountMaxAmount:'',
                DiscountStartDate:'',
                DiscountEndDate:'',
            },
            discounts:[],
            sizes:[],
            toppings:[],
            topping_model:[],
            Food: {
                FoodCode:'',
                FoodName:'',
                ListTopping:[],
                FoodStar:0,
                ImageURL:'',
            },
            FoodDetails: [],
            star: 0,
        }
    },
    // rule của các input
    validations: {
        Food: {
            FoodCode: {
                required,
                startWithFC(){
                    if(this.Food.FoodCode.startsWith('FC-')) return true;
                    else return false;
                }
            },
            FoodName: {
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
                for(let key in this.Food){
                    if(this.$v.Food[key]){
                        if(this.$v.Food[key].$error == true){
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
         * Create: PTDuyen(09/09/2021)
         */
        showPopup(crud) {
            this.popShow = true;
            this.action = crud;
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
                this.$refs.FoodCode.focus();
                this.$refs.FoodCode.classList.remove("is-invalid");
            }
            this.popShow = false;
        },
        /**
         * Đóng form bằng nút hủy hoặc x(khi có dữ liệu thay đổi)
         * Create: PTDuyen(11/09/2021)
         */
        btnXForm(){
            this.$router.go(-1);
        },
        /**
         * gọi đến hàm closeForm của FoodList
         * Create: PTDuyen(11/09/2021)
         */
        closeForm() {
            this.$router.go(-1);    
        },

        /**
         * Đổi trạng thái crud
         * Create: PTDuyen(10/09/2021)
         */
        changeCrud(action){
            this.crudDetail = action;
        },
        /**
         * delete toast message
         * Create: PTDuyen(13/09/2021)
         */
        deleteToast(index) {
            this.listMgs.splice(index, 1);
        },
        async crudObject(){
            try {
                let user = JSON.parse(localStorage.getItem("user"))
                if(user){
                    this.Food.DiscountId = this.discountModel.DiscountId;
                    this.Food.DiscountMaxAmount = Base.formatToNumber(this.discountModel.DiscountMaxAmount);
                    this.Food.DiscountAmount = this.discountModel.DiscountAmount;
                    this.Food.DiscountStartDate = new Date(Base.formatDate(this.discountModel.DiscountStartDate));
                    this.Food.DiscountEndDate = new Date(Base.formatDate(this.discountModel.DiscountEndDate));
                    this.Food.DiscountCode = this.discountModel.DiscountCode;
                    this.Food.DiscountTitle = this.discountModel.DiscountTitle;
                    this.Food.UserAction = user.userName;
                    if(this.validateForm() == true){
                        if(this.$refs.ImageURL.files && this.$refs.ImageURL.files[0]){
                            let formData = new FormData();
                            formData.append('file', this.$refs.ImageURL.files[0])
                            this.Food.ImageURL = await FoodAPI.uploadFile(formData);
                        }
                        else if(this.$el.querySelector("#img-Food").getAttribute('src')){
                            this.Food.ImageURL = this.$el.querySelector("#img-Food").getAttribute('src');
                        }
                        else {
                            this.Food.ImageURL = '';
                        }
                        this.Food.CategoryId = this.categoryModel.CategoryId ? this.categoryModel.CategoryId : null;
                        if(this.topping_model.length){
                            this.Food.ListTopping = this.topping_model.map(item => item.ToppingId).join(",");
                        }
                        else{
                            this.Food.ListTopping = "";
                        }
                        if(this.FoodDetails.length){
                            this.FoodDetails = this.FoodDetails.map(item =>(
                                {
                                    Amount: item.Amount,
                                    NumberAmount: Base.formatToNumber((item.Amount+"").split(",")[0]),
                                    SizeId: item.SizeId,
                                    SizeCode: item.SizeCode,
                                    SizeName: item.SizeName
                                }
                            ))
                        }
                        this.FoodMerge.Food = this.Food;
                        this.FoodMerge.FoodDetails = this.FoodDetails;
                        if(this.FoodDetail.Amount){
                            this.FoodDetail.NumberAmount = Base.formatToNumber((this.FoodDetail.Amount+"").split(",")[0]);
                            this.FoodMerge.FoodDetails.push(this.FoodDetail);
                        }
                        // thêm loader
                        this.changeLoader(true);
                        // nếu là thêm mới hoặc nhân bản
                        if((this.crudDetail == CRUD.Post)||(this.crudDetail == CRUD.Copy)){
                            // thực hiện thêm mới
                            //Base.formatObjToSave(this.Food);
                            console.log(this.FoodMerge);
                            let res = await FoodAPI.postFood(this.FoodMerge);
                            console.log(res);
                            // bỏ loader
                            this.changeLoader(false);
                            // nếu statusCode = 201
                            if(res.status == 201){
                                if(this.post_save == true){
                                    await this.postSave();
                                }
                                else this.$router.go(-1);
                            } 
                            // nếu không thêm mới thành công
                            else {
                                //Base.formatObj(this.Food);
                                this.message = res.data.data[0];
                                this.$refs.FoodCode.classList.add("is-invalid");
                                this.showPopup(PopupState.Duplicate);
                            }
                            // bỏ loader
                            this.changeLoader(false);
                            return;
                        }
                        // nếu là cập nhật
                        else if(this.crudDetail == CRUD.Put){
                            //  thực hiện cập nhật
                            //Base.formatObjToSave(this.Food);
                            let res = await FoodAPI.putFood(this.FoodMerge);
                            // bỏ loader
                            this.changeLoader(false);
                            // nếu statusCode = 202
                            if(res.status == 202){
                                if(this.post_save == true){
                                    await this.postSave();
                                }
                                else {
                                    this.$router.go(-1);
                                }
                            } 
                            // nếu cập nhật thất bại
                            else{
                                console.log(res);
                                this.message = res.data.data[0];
                                this.$refs.FoodCode.classList.add("is-invalid");
                                this.showPopup(PopupState.Duplicate);
                            }
                            this.changeLoader(false);
                            return;
                        }
                    }
                }
                else {
                    this.$router.push({path: '/login', name: 'Login'});
                }
            } catch (error) {
                // bỏ loader
                this.changeLoader(false);
                if((this.crudDetail == CRUD.Post)||(this.crudDetail == CRUD.Copy)){
                    this.listMgs.push({
                        showToast: true,
                        titleMgs: Base.stringFormat(ToastMgs.PostError,[this.Food.FoodName]),
                        iconToast: ToastMessageIcon.error
                    })
                }
                // nếu là cập nhật lỗi
                else if (this.crudDetail == CRUD.Put){
                    this.listMgs.push({
                        showToast: true,
                        titleMgs: Base.stringFormat(ToastMgs.PutError,[this.Food.FoodName]),
                        iconToast: ToastMessageIcon.error
                    })
                }
                else {
                    this.listMgs.push({
                        showToast: true,
                        titleMgs: ToastMgs.ExceptionError,
                        iconToast: ToastMessageIcon.error
                    })
                }
            }
        },

        getFile(imgSelector, event){
            Base.getFile(imgSelector, event);
        },
        deleteFile(imgSelector){
            this.Food.ImageURL = '';
            this.$el.querySelector(imgSelector).setAttribute('src', '');
        },
        changeLoader(value){
            this.loader = value;
        },

        formatNumber(index){
            if(typeof(index)=='number'){
                this.FoodDetails[index].Amount = Base.formatToNumber(this.FoodDetails[index].NumberAmount); 
                this.FoodDetails[index].NumberAmount = Base.formatNumber(this.FoodDetails[index].Amount);
                document.getElementById("amountDetail"+index).value = this.FoodDetails[index].NumberAmount;
            }
            else {
                // console.log(this.unitMoney_model);
                this.FoodDetail.Amount = Base.formatToNumber(this.FoodDetail.NumberAmount); 
                this.FoodDetail.NumberAmount = Base.formatNumber(this.FoodDetail.Amount);
                document.getElementById("amountDetail").value = this.FoodDetail.NumberAmount;
            }
        },

        /**
         * Xóa detail khoi list
         * Create:PTDuyen(19/10/2021)
         */
        removeDetail(index){
            if(typeof(index)=='number'){
                this.FoodDetails.splice(index,1);
            }
            else {
                if(index == 'all'){
                    this.FoodDetails = [];
                }
                else {
                    this.showInputDetail = false; 
                    this.FoodDetail = {
                        Amount: '',
                        Size: {
                            SizeId: '',
                            SizeName: '',
                        }
                    }
                }
            }
            if(this.FoodDetails.length == 0){
                this.showInputDetail = true; 
                    this.FoodDetail = {
                        Amount: '',
                        Size: {
                            SizeId: '',
                            SizeName: '',
                        }
                    }
            }
        },
        /**
         * Thêm detail vào list
         * Create:PTDuyen(19/10/2021)
         */
        addDetail(){
            // console.log("ok");
            this.FoodDetails = this.FoodDetails.filter(e => e != this.FoodDetail);
            this.FoodDetails.push(this.FoodDetail);
            this.FoodDetail = Object.assign({}, this.FoodDetails[this.FoodDetails.length-1]);
            console.log(this.FoodDetails);
        },

        /**
         * lấy ra phần tử được select
         * Create: PTDuyen(12/09)
         */
        getSelect(model, item){
            this[model] = item;
            switch (model){
                case 'topping_model':
                    if(item.length)
                        this.Food.ListTopping = item.map(topping => topping.ToppingId);
                    break;
                case 'country_model':
                    this.Food = item;
                    break;
                default:
                    break;
            }
        },
        /**
         * lấy mã code mới
         * Create: PTDuyen(18/08/2021)
         */
        async getNewCode(){
            try{
                // lấy mã nhân viên mới
                var res = await FoodAPI.getNewCode();
                this.Food.FoodCode = res;
            }
            // nếu lấy mã mới thất bại
            catch(res){
                console.log(res);
            }
        },
        /**
         * Nếu là hành động cất và thêm
         * Create: PTDuyen(11/10/2021)
         */
        async postSave(){
            // reset lại validate
            this.$nextTick(() => { this.$v.$reset() })
            // đổi hành động về thêm mới
            this.changeCrud(CRUD.Post);
            // provider chuyển về giá trị ban đầu
            this.FoodMerge = {};
            // lấy mã ncc mới
            await this.getNewCode();
            // focus vào ô input object_code
            this.$refs.FoodCode.focus();
            // chuyển cất và thêm về false
            this.post_save = false;
        },
    },
}
</script>


<style>
.food-detail{
    position: absolute;
    width: 100%;
    overflow: auto;
    height: calc(100% - 140px);
}

.food-detail .add-item{
    display: block;
    padding: 0 35px;
}

.food-detail .add-item .content-head{
    padding-left: 0;
}

table .w-30 {
    min-width: 30px;
    position: sticky !important;
    left: 0;
    border-style: none;
    background-color: #f4f5f7!important;
    z-index: 2 !important;
}

.food-detail .add-item .table-actions {
    margin-top: 10px;
    padding: 10px;
    height: 46px;
    position: sticky;
    bottom: 0;
    background-color: #f4f5f7!important;
}

.food-detail .add-item .table-action {
    background: #fff;
    border: 1px solid #8d9096;
    border-radius: 3px!important;
    padding: 2px 20px!important;
    margin-right: 10px;
    font-weight: 700;
}

table tr:hover > .w-30{
    background-color: #f4f5f7!important;
}


.stars-container {
    position: relative;
    display: inline-block;
    color: transparent;
    font-size: 20px;
}

.stars-container:before {
    position: absolute;
    top: 0;
    left: 0;
    content: '★★★★★';
    color: lightgray;
}

.stars-container:after {
    position: absolute;
    top: 0;
    left: 0;
    content: '★★★★★';
    color: gold;
    overflow: hidden;
}

.stars-0:after {
    width: 0%;
}

.stars-10:after {
    width: 10%;
}

.stars-20:after {
    width: 20%;
}

.stars-30:after {
    width: 30%;
}

.stars-40:after {
    width: 40%;
}

.stars-50:after {
    width: 50%;
}

.stars-60:after {
    width: 60%;
}

.stars-70:after {
    width: 70%;
}

.stars-80:after {
    width: 80%;
}

.stars-90:after {
    width: 90%;
}

.stars-100:after {
    width: 100;
}

.food-detail .form-footer,
.order-detail .form-footer{
    position: fixed;
    padding: 18px 20px;
    z-index: 4;
    width: calc(100% - 57px);
    padding: 18px 35px;
    background-color: #f4f5f7;
}
.food-detail .form-footer.form-right,
.order-detail .form-footer.form-right{
    width: calc(100% - 185px);
}
</style>
