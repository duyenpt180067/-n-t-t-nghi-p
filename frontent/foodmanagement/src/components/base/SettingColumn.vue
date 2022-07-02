<template>
    <div class="column-setting">
        <div class="arrow-up"></div>
        <div class="setting-column">
            <div class="flex-center setting-column-top">Thiết lập cột</div>
            <div class="column flex pointer" v-for="(column) in layoutValue" :key="column.DataBind">
                <div class="w-1/5">
                    <input type="checkbox" :id="'column-'+column.DataBind" :checked="column.State" class="checkbox pointer"/>
                </div>
                <label :for="'column-'+column.DataBind" class="column-name pointer w-4/5">{{column.NameColumn}}</label>
            </div>
            <div class="flex-center btn-setting-column">
                <div @click="defaultLayout" class="default-layout pointer">Mặc định</div>
                <button class="btn admin-btn-primary" @click="saveColumn">Lưu</button>
            </div>
        </div>
    </div>
</template>

<style scoped>
.column-setting{
    position: absolute;
    top: 175px;
    right: 12px;
    z-index: 3;
    min-width: 180px;
    max-height: 250px;
}
.arrow-up {
    width: 0; 
    height: 0; 
    border-left: 15px solid transparent;
    border-right: 15px solid transparent;
    border-bottom: 15px solid #e5e5e5;
    position: absolute;
    top: -8px;
    right: 5px;
}
.setting-column {
    box-shadow: 0px 4px 6px rgb(0 0 0 / 40%);
    overflow: auto;
    border-radius: 3px;
    background: #fff;
    overflow: hidden;
}
.setting-column .setting-column-top{
    min-height: 30px;
    background-color: rgb(229, 229, 229);
    position: sticky;
    top: 0;
    z-index: 1;
}
.setting-column .column{
    min-height: 40px;
    align-items: center;
    padding: 0px 15px;
    transition: .2s;
}
.setting-column .column:hover{
    background-color: #f6fcfc;
    color: var(--primary-admin-color);
}
.setting-column .column input[type=checkbox]:checked:after{
    margin-left: 30%;
}
.setting-column .btn-setting-column{
    position: sticky;
    bottom: 0px;
    background: #fff;
    padding: 5px 0;
}
.setting-column .admin-btn-primary{
    padding: 5px 20px;
}
.setting-column .default-layout:hover{
    color: var(--primary-admin-color);
    text-decoration: underline var(--primary-admin-color);
}
</style>

<script>
export default {
    name:'SettingColumn',
    props:{
        layoutValue:Array,
    },
    methods:{
        saveColumn(){
            let listStateTrue = [];
            listStateTrue = [...this.$el.querySelectorAll("input[type=checkbox]:checked")].map(item => item.id.split("-")[1]);
            this.$emit("saveColumn", listStateTrue);
        },
        defaultLayout(){
            let listStateTrue = [];
            listStateTrue = this.layoutValue.map(item => item.DataBind);
            this.$emit("saveColumn", listStateTrue);
        }
    }
}
</script>

<style>

</style>