<template>
<div class="detail" style="background: #fff;">
    <!-- loading -->
    <div v-if="loader" :class="{'background-loader':loader}">
        <div :class="{loader:loader}">
            <object :data="loaderUrl"></object>
        </div>
    </div>
    <!-- background -->
    <div class="cover-background" style="z-index: 4"></div>
    <div class="add-item">
        <div class="add-entity">
             <form class="form-add" id="scroll-thumb" autocomplete="off">
                <div class="form-header">
                    <h2 class="form-title">Thông tin đơn hàng chi tiết</h2>
                    <div class="header-btn-content">
                        <div v-tooltip.bottom="{content:'Đóng (Esc)', classes:'tooltip', offset: '5px'}" 
                             class="form-header-btn btn-close" id="btn-close" tabindex="0" 
                             @click="btnXForm()"
                             v-shortkey="['esc']" @shortkey="btnXForm()">
                        </div>
                    </div>
                </div>
                <div class="form-init" style="overflow: hidden;">
                    <div class="form-init-general">
                        <div class="general-row flex">
                            <div class="w-2/5 m-r-12">
                                <p>Sản phẩm <span>*</span></p>
                                <v-combobox @getSelect="foodModel=$event;" 
                                            :combobox_valid="false" 
                                            :placeholder="''" 
                                            :item_text="['FoodCode','FoodName']" 
                                            :items="foodList"
                                            :display_item="'FoodName'" 
                                            :groupName="['Mã sản phẩm','Tên sản phẩm']"
                                            :vmodel="foodModel"
                                            :readonly="readonly" 
                                            :multiple="false">
                                </v-combobox>
                            </div>
                            <div class="w-2/5 m-r-12">
                                <p>Kích thước <span>*</span></p>
                                <v-combobox @getSelect="foodDetailModel=$event;" 
                                            :combobox_valid="false" 
                                            :placeholder="''" 
                                            :item_text="['SizeCode','SizeName']" 
                                            :items="foodListDetail"
                                            :display_item="'SizeName'" 
                                            :groupName="['Mã kích thước','Kích thước']"
                                            :vmodel="foodDetailModel"
                                            :readonly="readonly" 
                                            :multiple="false">
                                </v-combobox>
                            </div>
                            <div class="w-1/5 end">
                                <p>Đơn giá</p>
                                <div>{{foodDetailModel.Amount}} VND</div>
                                <!-- <input type="text" readonly v-model="foodDetailModel.Amount"> -->
                            </div>
                        </div>
                        <div class="general-row flex" style="position: relative;">
                            <div class="w-4/5 m-r-12">
                                <p>Danh sách Deal</p>
                                <!-- <input class="w-full" type="text" maxlength="255"> -->
                                <v-combobox @getSelect="toppingModel=$event" 
                                            :combobox_valid="false" 
                                            :placeholder="''" 
                                            :item_text="['ToppingCode','ToppingName', 'ToppingAmount']" 
                                            :items="toppings" 
                                            :vmodel="toppingModel"
                                            :multiple="true"
                                            :groupName="['Mã Topping','Tên Topping', 'Giá Topping']"
                                            :display_item="'ToppingName'"
                                            :readonly="readonly">
                                </v-combobox>
                            </div>
                            <div class="w-1/5 end">
                                <p></p>
                                <div></div>
                            </div>
                        </div>
                        <div class="general-row"></div>
                    </div>
                </div>
                <div class="line"></div>
                <div class="form-footer flex">
                    <div class="admin-btn-normal btn-delete" tabindex="18" @click="btnXForm()" @keyup.enter="btnXForm()">Hủy</div>
                    <div class="btn-save-all flex">
                        <!-- :disable="readonly" -->
                        <div class="admin-btn-normal btn-save" tabindex="16"
                             @click="crudObject()"
                             @keyup.enter="crudObject()"
                             v-shortkey="['ctrl','s']" @shortkey="crudObject()"
                             v-tooltip.top="{content:'Cất (Ctrl + S)', classes:'tooltip', offset: '5px'}">Cất</div>
                        <div class="admin-btn-normal admin-btn-primary"
                             tabindex="17"
                             @click="post_save=true,crudObject()" 
                             @keyup.enter="post_save = true,crudObject()"
                             v-shortkey="['ctrl','shift','s']" @shortkey="post_save = true,crudObject()"
                             v-tooltip.top="{content:'Cất và thêm (Ctrl+ Shift + S)', classes:'tooltip', offset: '5px'}">Cất và Thêm</div>
                    </div>
                </div>
            </form>
        </div>
    </div>
    <div>
    </div>
</div>
</template>

<script>
import FoodAPI from '../../../../api/component/food/FoodAPI';
import ToppingAPI from '../../../../api/component/additional-element/ToppingAPI';
import VCombobox from '@/components/base/VCombobox';
export default {
    name:'AddOrderDetail',
    components:{
        VCombobox
    },
    props:{
        readonly: Boolean
    },
    data(){
        return {
            // hiển thị loading
            loader:false,
            loaderUrl: require('../../../../assets/lib/img/common/loading.svg'),
            foodList:[],
            foodListDetail:[],
            foodModel:{
                FoodCode:'',
                FoodName:'',
            },
            foodDetailModel:{
                SizeName:'',
                SizeCode:'',
                Amount:0,
            },
            orderDetail:{
                Food:{
                    FoodCode:'',
                    FoodName:'',
                },
                FoodDetail:{
                    SizeName:'',
                    SizeCode:''
                }
            },
            toppings:[],
            toppingModel:[],
        }
    },
    watch:{
        async foodModel(){
            this.foodDetailModel = {
                SizeName:'',
                SizeCode:'',
                Amount:0,
            };
            let modelStart = {FoodId:null,FoodName:null,FoodCode:null};
            this.check_model('foodModel', this.foodList, 'FoodName', modelStart);
            if((this.foodModel)&&(this.foodModel.FoodId)){
                this.foodListDetail = (await FoodAPI.getFoodByCode(this.foodModel.FoodCode)).FoodDetails;
            }
            else {
                this.foodModel = null;
                this.foodDetailModel = null;
                this.foodListDetail = [];
            }
        },
    },
    async created() {
        let foodData = await FoodAPI.getFilterPaging({FoodFilter:null,CategoryId:null,PageNumber:null,PageSize:null});
        this.foodList = foodData.data;
        let toppingData = await ToppingAPI.getFilterPaging({ToppingFilter:null,ToppingAmountMin:null,ToppingAmountMax:null,PageNumber:null,PageSize:null});
        this.toppings = toppingData.data;
    },
    methods:{
        /**
         * Đóng form bằng nút hủy hoặc x(khi có dữ liệu thay đổi)
         * Create: PTDuyen(11/09/2021)
         */
        btnXForm(){
            this.$emit('btnXForm');
        },
        crudObject(){

        },
        // kiểm tra model có type là string hay không(khi người dùng nhập không có dữ liệu)
        check_model(model, items, item_text, modelStart){
            if(typeof(this[model]) == "string"){
                if(this[model].trim() != ""){
                    let getModel = false;
                    // lấy đối tượng đầu tiên chứa ký tự nhập
                    for(let item of items){
                        if(item[item_text].toLowerCase().trim().includes(this[model].toLowerCase().trim())){
                            this[model] = item;
                            getModel = true;
                            break;
                        }
                    }
                    // nếu không tồn tại đối tượng nào chứa thì đưa về null
                    if(getModel == false){
                        this[model] = modelStart;
                    }
                }
                else {
                    this[model] = modelStart;
                    // console.log(this[model]);
                }
            }
        },
    }
}
</script>

<style>

</style>