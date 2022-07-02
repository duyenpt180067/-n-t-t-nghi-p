<template>
<div class="content-init access-log">
    <div class="flex content-head">
        <div class="content-title">Nhật ký truy cập</div>
        <div class="btn flex-center">
            <button class="admin-btn-primary add-btn" @click="openForm(null, 'post')">
                <i class="fa fa-plus"></i>
                Thêm mới
            </button>
        </div>
    </div>
    <div class="content-action flex">
        <div class="action-left flex">
            <div class="content-filter flex">
                <input placeholder="Nhập từ khóa để tìm kiếm" 
                        type="text" 
                        ref="Filter" 
                        id="filter"
                        v-model="keyword"
                        v-tooltip.bottom-end="{offset: '5px', content:'Nhấp Enter để tìm kiếm', classes:'tooltip'}" @keyup.enter="getFilterPage()">
                <div class="content-filter-search logo-icon other-icon pointer"></div>
            </div>
        </div>
        
        <div class="action-right flex">
            
            <div class="btn-filter-bar btn-action flex-center" v-tooltip.bottom="{content:'Lấy lại dữ liệu', classes:'tooltip', offset: '5px'}" @click="reload()">
                <div class="refresh-icon other-icon logo-icon pointer"></div>
            </div>
            <div class="btn-filter-bar btn-action flex-center" v-tooltip.bottom="{content:'Thiết lập cột', classes:'tooltip', offset: '5px'}" @click="settingColumn()">
                <div class="setting-icon other-icon logo-icon pointer"></div>
            </div>
        </div>
    </div>
    <common-table :oneAction="true" :layoutConfig="layoutConfig" :dataList="dataList" :formShow="formShow" @closeForm="formShow=false;"
                  :actions="actions" :checkPaging="true" @openForm="openForm"
                  ref="CommonTable" @crudObject="crudObject" :objectName="'AccessLog'" @getFilterPage="getFilterPage">
    </common-table>
    <div class="detail" v-if="formShow">
        <!-- loading -->
        <div v-if="loader" :class="{'background-loader':loader}">
            <div :class="{loader:loader}">
                <object :data="loaderUrl"></object>
            </div>
        </div>
        <!-- background -->
        <div class="cover-background"></div>
        <div class="content-form add-item">
            <div class="add-entity">
                <form class="form-add" id="scroll-thumb" autocomplete="off">
                    <div class="form-header">
                        <h2 class="form-title">Chi tiết hành động</h2>
                        <div class="header-btn-content">
                            <div v-tooltip.bottom="{content:'Đóng (Esc)', classes:'tooltip', offset: '5px'}" 
                                class="form-header-btn btn-close" id="btn-close" tabindex="0" 
                                @click="btnXForm()"
                                v-shortkey="['esc']" @shortkey="btnXForm()">
                            </div>
                        </div>
                    </div>
                    <div class="form-init header-content">
                        <div class="flex-center m-b-20">
                            <div class="w-1/3 m-r-12 center">Người dùng: <b>{{objectDetail.UserAction}}</b> </div>
                            <div class="w-1/3 m-r-12 center">Hành động: <b>{{objectDetail.Action}}</b> </div>
                            <div class="w-1/3 center">Đối tượng: <b>{{objectDetail.Subject}}</b> </div>
                        </div>
                        <div class="detail-action">
                            <div class="center m-b-20 detail-action-title">Chi tiết hành động</div>
                            <div class="detail-action-init">
                                <div class="created-date">Ngày thực hiện: {{formatDateTimeToString(objectDetail.CreatedDate)}}</div>
                                <div class="description">{{objectDetail.Description}}</div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
</template>

<script>
import CommonTable from '../../../base/CommonTable.vue';
import AccessLogAPI from '../../../../api/component/additional-element/AccessLogAPI'
import { ViewAction } from '../../../../base/vi/Resources';
import Base from '../../../../base/Base';
export default {
  components: { CommonTable },
    name: 'AccessLog',
    data(){
        return {
            layoutConfig:[],
            formShow: false,
            keyword: "",
            dataList:[],
            actions: ViewAction,
            // hiển thị loading
            loader:false,
            loaderUrl: require('../../../../assets/lib/img/common/loading.svg'),
            objectDetail:{},
        }
    },
    async created(){
        try {
            // hiển thị loader
            this.changeLoader(true);
            this.layoutConfig = await AccessLogAPI.getLayoutConfig();
            await this.getFilterPage();
            this.changeLoader(false);
        } catch (error) {
            this.changeLoader(false);
            console.log(error);
        }
    },
    methods:{
        async getFilterPage(){
            // hiển thị loader
            this.changeLoader(true);
            this.paramGet = {
                AccessLogFilter: this.keyword,
                PageNumber: this.$refs.CommonTable.currentPage,
                PageSize: this.$refs.CommonTable.pageSize
            };
            this.dataList = await AccessLogAPI.getFilterPaging(this.paramGet);
            this.changeLoader(false);
        },
        btnXForm(){
            this.formShow = false;
        },
        openForm(obj, action){
            this.formShow = true;
            this.objectDetail = Object.assign({}, obj);
            console.log(action, obj);
        },
        changeLoader(value){
            this.$emit('changeLoader', value);
        },
        crudObject(action, obj){
            this.openForm(obj, action);
        },
        formatDateTimeToString(date){
            return Base.formatDateTimeToString(date);
        }
    }
}
</script>

<style>
.access-log table td div.content-column{
    white-space: pre-wrap;
}
.access-log .form-add{
    width: 700px;
    font-size: 14px;
}
.access-log .detail-action{
    white-space: pre-line;
    line-height: 23px;
}
.access-log .detail-action-init{
    border-top: 1.5px solid var(--gray-color);
    padding: 20px;
    font-size: 13px;
}
</style>