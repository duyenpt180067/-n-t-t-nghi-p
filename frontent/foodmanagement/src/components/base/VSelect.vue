<template>
    <div class="select-option">
        <v-select :items="items" 
                  transition="slide-y-transition" 
                  append-icon="arrow_drop_down" 
                  :menu-props="{ offsetY: true, top:top }" 
                  :item-text="item_text" 
                  v-model="defaultItem" 
                  @change="getSelect" :return-object="return_object">
            <template v-slot:item="{ item }">
                <div v-if="!stt">
                    <div v-if="return_object">
                        {{ item[item_text] }}
                    </div> 
                    <div v-else>
                        {{item}}
                    </div>
                </div>
                <div v-if="stt">
                    {{items.indexOf(item) + 1}}. {{item[item_text]}}
                </div>
            </template>
            <template v-slot:selection="{ item }" v-if="stt">
                <div v-if="return_object">{{items.indexOf(item) + 1}}. {{item[item_text]}}</div>
                <div v-else>{{items.indexOf(item) + 1}}. {{item}}</div>
            </template>
        </v-select>
    </div>
</template>

<style>

.v-menu__content {
    box-shadow: none !important;
    border: 1px solid var(--gray-color) !important;
    border-radius: 0;
    margin-top: -3px;
    padding-right: 0;
    background-color: white;
    border-radius: 2px !important;
    /* min-width: 116.5px !important; */
}

.v-text-field .v-input__prepend-inner, 
.v-text-field .v-input__append-inner{
    height: 100%;
    display: flex;
    align-items: center;
    width: 30px;
    align-self: center;
    margin-top: 0 !important;
    padding-left: 0 !important;
    /* align-self: auto !important; */
}

.v-list-item__content{
    padding: 0 !important;
}

/* action - chức năng */
.action .v-select__slot{
    position: absolute;
    right: 24px;
    top: -30px;
}

.action .select-option .v-input{
    padding: 0;
    margin: 0;
    height: 0px;
    width: 0px;
    margin-top: 35px;
}

.action .select-option .v-select .v-select__selections{
    height: 30px;
}

.action .select-option .v-select .v-select__selections input{
    visibility: hidden;
}

.select-option .theme--light.v-text-field > .v-input__control > .v-input__slot:before{
    border: none;
}

.select-option .v-text-field > .v-input__control > .v-input__slot:after{
    border: none;
}

.v-list{
    padding: 0 !important;
}

.v-list-item{
    min-height: 30px !important;
    height: 30px !important;
    padding: 0 13px !important;
}


.v-list-item__title{
    font-size: 12px !important;
}

.action .v-select.v-select--is-menu-active .v-input__icon--append .v-icon{
    transform: none;
    border: 1px solid #459bd2;
}

/* page-size - chọn page-size */

.page-size .v-input{
    padding: 0;
}
.page-size .v-select__slot{
    border: 1px solid var(--gray-color);
    height: 33px;
    border-radius: 2px;
}

.page-size .v-input__slot{
    margin-bottom: 0;
}

.page-size .v-select__selections{
    font-size: 13px;
    margin-right: 5px;
}

.page-size .v-select__selections input{
    display: none;
}

.page-size .v-select__selections .v-select__selection{
    max-width: 100% !important;
    margin: 0 !important;
    padding-left: 10px;
}

.page-size .v-input__append-inner:hover{
    background-color: #e5e5e5;
}

.page-size .v-select--is-menu-active .v-input__append-inner{
    background-color: #e5e5e5;
}

.page-size .v-select--is-menu-active .v-input__append-inner .v-input__icon i{
    color: rgba(0, 0, 0, 0.54) !important;
}

</style>
<script>
export default {
    name:'TheSelect',
    props:{
        items:[Array, Object],
        vmodel: [String, Object],
        item_text: [String, Array],
        top:Boolean,
        stt:Boolean
    },
    data(){
        return {
            defaultItem : this.vmodel,
            return_object: false
        }
    },
    created(){
        if(typeof(this.vmodel) == 'object'){
            this.return_object = true;
        }
        else this.return_object = false;
    },
    methods: {
        getSelect(item) {
            this.$emit('getSelect', item);
        },
        getAllObj(){
            this.defaultSelected = this.items[0];
        }
    },
    watch:{
        defaultItem(){
            if(typeof(this.vmodel) == 'string'){
                this.defaultItem = '';
            }
        },
        vmodel(){
            this.defaultItem = this.vmodel;
        },
    }
}
</script>