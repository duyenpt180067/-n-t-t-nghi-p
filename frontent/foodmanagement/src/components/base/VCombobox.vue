<template>
<div class="combobox">
    <v-combobox class="app-combobox" ref="ComboboxComponent"
                :class="{invalid:invalid, 'readonly':readonly}" 
                v-model="defaultSelected" 
                @change="getSelect" 
                :items="items" 
                :item-text="item_show" 
                append-icon="arrow_drop_down" 
                :placeholder="placeholder" 
                return-object
                :tabindex="tabindex"
                :readonly="readonly"
                :multiple="multiple"
                :menu-props="{ maxHeight: '200', nudgeBottom: bottom, maxWidth: maxWidth }"
                @blur="checkRequired=true;checkValid(defaultSelected)">
        <template v-slot:prepend-item v-if="typeof(item_text)=='object'">
            <div class="header-menu">
                <th style="text-align:left;" v-for="(head, index) in groupName" :key="index">
                    <p>{{head}}</p>
                </th>
            </div>
        </template>
        <template v-slot:item="{ item }">
            <div v-if="typeof(vmodel)=='string'">{{item}}</div>
            <div v-else>
                <div v-if="typeof(item_text)=='string'">
                    {{ item[item_text] }}
                </div>
                <div v-if="typeof(item_text)=='object'">
                    <div v-if="padding_body">
                        <tr><td v-for="(row, index) in item_text" :key="index" :style="{'padding-left':item.grade*10+10+'px'}">{{item[item_text[index]]}}</td></tr>
                        <v-icon>checkbox</v-icon>
                    </div>
                    <div v-else>
                        <tr><td v-for="(row, index) in item_text" :key="index">{{item[item_text[index]]}}</td></tr>
                        <v-icon>checkbox</v-icon>
                    </div>
                </div>
            </div>
        </template>
        <template v-slot:selection="{ parent, item, selected }" v-if="typeof(item_text)=='object'&&multiple==true">
            <v-chip :input-value="selected" label small>
                <span v-if="display_item!=''&&display_item!=undefined&&display_item!=null">{{ item[display_item] }}</span>
                <span v-else>{{ item[item_text[0]] }}</span>
                <v-icon small @click="parent.selectItem(item)">$delete</v-icon>
            </v-chip>
        </template>
        <template v-slot:no-data>
            <v-list-item>
                <v-list-item-content>
                    <v-list-item-title style="text-align: center;" title="Không có dữ liệu hiển thị.">
                        Không có dữ liệu hiển thị.
                    </v-list-item-title>
                </v-list-item-content>
            </v-list-item>

        </template>
    </v-combobox>
</div>
</template>

<style>

.readonly{
    background-color: #e5e5e5 !important;
}

.v-menu__content .header-menu{
    position: sticky;
    top: 0;
    background-color: #f4f5f8;
    z-index: 9;
    vertical-align: middle;
    height: 32px;
    display: flex;
    align-items: center;
    padding-left: 13px;
}

.invalid{
    border: 1px solid #ff4747 !important;
}

.combobox .v-select {
    font-size: 12px !important;
    border-radius: 3px !important;
    margin: 0 !important;
    padding: 0 !important;
}

.combobox .v-input__slot {
    margin-bottom: 0 !important;
}

.combobox .v-label,
.combobox .v-list-item__title {
    font-size: 12px !important;
}

.combobox .v-select__slot {
    border-radius: 2px !important;
}

.combobox .v-input input {
    width: 100%;
    max-width: 100%;
    border: 0px;
    height: 29px;
}

.combobox .v-input input:focus{
    border: 0px !important;
}


.combobox .v-input {
    border: 1px solid var(--gray-color);
}

.combobox .v-input--is-focused {
    border: 1px solid var(--primary-admin-color);
    caret-color: black !important;
}

.combobox .v-text-field .v-input__prepend-inner,
.combobox .v-text-field .v-input__append-inner {
    height: 30px !important;
    align-items: center !important;
    margin-top: 0 !important;
    padding-left: 0px !important;
    position: absolute;
    right: 0;
}

.combobox .v-input__icon {
    height: 100% !important;
    width: 32px !important;
    cursor: pointer !important;
    position: absolute;
    top: 0;
    right: 0;
}

.combobox .theme--light.v-text-field>.v-input__control>.v-input__slot:before {
    border: none !important;
}

.combobox .theme--light.v-text-field>.v-input__control>.v-input__slot::after {
    border: none !important;
}

.combobox .v-icon {
    color: black !important;
    height: 30px !important;
    width: 30px !important;
}

.combobox .v-input__icon:focus {
    background-color: #e5e5e5 !important;
}

.combobox .app-combobox{
    height: fit-content;
}

.combobox .app-combobox.primary--text,
.combobox .app-combobox .v-icon.notranslate.material-icons.theme--light.primary--text {
    color: black !important;
    caret-color: rgb(2, 0, 0) !important;
}

.v-list-item.primary--text.v-list-item--active.v-list-item--link.theme--light.v-list-item--highlighted {
    background-color: var(--primary-admin-color) !important;
    color: white !important;
}

.theme--light.v-list-item.v-list-item--highlighted{
    color: var(--primary-admin-color) !important;
}

.v-list-item.primary--text.v-list-item--active.v-list-item--link.theme--light {
    background-color: var(--primary-admin-color) !important;
    color: white !important;
}

.combobox .v-input__icon.v-input__icon--clear button {
    width: 20px !important;
    height: 20px !important;
    font-size: 13px !important;
    background-color: #e5e5e5;
    border-radius: 50%;
    min-width: 0 !important;
}

.combobox .v-icon.notranslate.v-icon--link.material-icons.theme--light.primary--text {
    font-size: 12px !important;
    font-weight: 900;
}

.v-application .combobox .white--text:hover {
    color: rgba(0, 0, 0 0) !important;
    opacity: 0 !important;
}

.theme--light.v-list-item:hover{
    color: var(--primary-admin-color) !important;
}

.v-application .v-list-item--highlighted .white--text {
    color: rgba(0, 0, 0 0) !important;
    opacity: 0;
}

.v-application .v-list-item--active .white--text {
    color: white !important;
    opacity: 1;
}

.v-menu__content::-webkit-scrollbar{
    width: 6px;
    height: 3px;
    background-color: #F5F5F5;
}                                      
.v-menu__content::-webkit-scrollbar-thumb{
    border-radius: 10px;
    box-shadow: inset 0 0 6px rgba(0, 0, 0, .3);
    background-color: var(--gray-color);
}

.v-menu__content::-webkit-scrollbar-track {
    box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.3);
    border-radius: 10px;
    background-color: #F5F5F5;
    max-height: 50px;
}

.v-autocomplete__content.v-menu__content{
    margin-top: 1px;
}

.combobox .v-select--is-multi input{
    border-style: none;
}

.v-menu__content .v-list-item i{
    width: 32px;
    justify-content: left;
    color: #fff;
    position: absolute;
    right: 15px;
    font-size: 15px;
    align-items: center;
    top: 20%;
}

.v-menu__content .v-list-item .v-chip{
    background: inherit !important;
    height: auto !important;
    justify-content: left;
    min-width: 130px;
}

.v-menu__content .v-list-item.v-list-item--highlighted i{
    color: #dfefdd !important;
}

.v-menu__content .v-list-item.v-list-item--active .v-chip .v-chip__content span{
    color: #fff;
} 

.combobox .v-select__selections{
    height: fit-content;
}

.v-text-field__details{
    min-height: 0 !important;
    height: 0;
}

/* table */
.v-menu__content td {
    max-height: 100%;
    height: 29px;
    padding: 0;
    border-style: none;
    vertical-align: middle;
    background-color: inherit;
}

.v-menu__content th,
.v-menu__content td{
    min-width: 140px;
    overflow: hidden;
    text-overflow: ellipsis;
}

.v-menu__content th:first-child+th,
.v-menu__content td:first-child+td{
    min-width: 190px;
    max-width: 190px;
}

.v-menu__content td:first-child+td {
    padding: 0 !important;
    padding-right: 10px!important;
}

</style>

<script>
export default {
    name: 'TheCombobox',
    props: {
        placeholder: [String, Array],
        item_text: [String, Array],
        items: [Array, String, Object],
        // check xem input có hợp lệ không true-cần kiểm tra, false-không cần
        combobox_valid: Boolean,
        vmodel: [Object, String, Array],
        multiple: Boolean,
        tabindex:[String, Number],
        nudgeBottom: [String, Number],
        groupName:[Array, Object],
        bottom: [String],
        maxWidth:[String],
        readonly:Boolean,
        padding_body:Boolean,
        display_item:[String]
    },
    watch:{
        vmodel(){
            this.defaultSelected = this.vmodel;
        },
        defaultSelected(){
            this.getSelect(this.defaultSelected);
            this.checkValid(this.defaultSelected);
        }
    },
    created(){
        if(typeof(this.item_text)=='string'){
            this.item_show = this.item_text;
        }
        else {
            if(this.display_item){
                this.item_show = this.display_item;
            }
            else this.item_show = this.item_text[0];
        }
        // console.log(this.item_text, this.item_show);
    },
    data() {
        return {
            defaultSelected: this.vmodel,
            invalid: false,
            checkRequired: false,
            item_show: '',
        }
    },
    methods: {
        /**
         * chọn item của list
         * Create:PTDuyen(17/08/2021)
         */
        getSelect(items) {
            this.$emit('getSelect', items);
            // console.log(items);
        },
        /**
         * Kiểm tra xem input có hợp lệ không
         * Ctreate:PTDuyen(20/08/2021)
         */
        checkValid(value){
            if((this.combobox_valid == true)&&(this.checkRequired == true)){
                if((value == "")||(value == null)||(value[this.item_text] == null)){
                    this.invalid = true;
                    this.checkRequired = false;
                }
                else {
                    this.invalid = false;
                }
            }
        }
    }
}
</script>
