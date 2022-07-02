<template>
<div class="combobox">
    <v-combobox class="app-combobox" 
        :class="{invalid:invalid}" 
        v-model="defaultSelected" 
        @change="getSelect" 
        :items="items" 
        :item-text="item_show" 
        append-icon="arrow_drop_down" 
        :placeholder="placeholder" 
        return-object
        :tabindex="tabindex"
        :multiple="multiple"
        
        @blur="checkRequired=true;checkValid(defaultSelected)">
        <!-- <v-toolbar>
            <v-toolbar-title>
                Title
            </v-toolbar-title>
            <v-toolbar-items> -->
                <template v-slot:prepend-item>
                    <div>
                        <td>{{item_text[0]}}</td>
                    </div>
                </template>
                <template v-slot:item="{ item }">
                    <div v-if="typeof(item_text)=='string'">
                        {{ item[item_text] }}
                    </div>
                    <div v-if="typeof(item_text)=='object'">
                        <tr><td v-for="(row, index) in item_text" :key="index">{{item[item_text[index]]}}</td></tr>
                        <v-icon>checkbox</v-icon>
                    </div>
                </template>
                <template v-slot:selection="{ parent, item, selected }" v-if="typeof(item_text)=='object'">
                    <v-chip :input-value="selected" 
                            label
                            small>
                        <span>{{ item[item_text[0]] }}</span>
                        <v-icon small @click="parent.selectItem(item)">$delete</v-icon>
                    </v-chip>
                </template>
                <template v-slot:no-data>
                    <v-list-item>
                        <v-list-item-content>
                            <v-list-item-title>
                                Không tìm thấy dữ liệu phù hợp
                            </v-list-item-title>
                        </v-list-item-content>
                    </v-list-item>

                </template>
            <!-- </v-toolbar-items>
        </v-toolbar> -->
    </v-combobox>
</div>
</template>
<script>
export default {
    name: 'ComboboxList',
    props: {
        placeholder: [String, Array],
        item_text: [String, Array],
        items: [Array, Object],
        // check xem input có hợp lệ không true-cần kiểm tra, false-không cần
        combobox_valid: Boolean,
        vmodel: [Object, String, Array],
        multiple: Boolean,
        tabindex:[String, Number]
    },
    // created(){
    //     console.log(this.item_text, typeof(this.item_text), this.item_text.length);
    // },
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
            this.item_show = this.item_text[0]
            console.log(this.item_show);
        }
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
            // this.$emit('getSelect', items);
            console.log(items);
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
