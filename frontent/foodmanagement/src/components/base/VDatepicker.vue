<template>
    <!-- <date-pick v-bind:value="date" v-on:input="date = $event" :inputAttributes="{placeholder: '__/__/____'}" :format="'DD/MM/YYYY'" 
    :nextMonthCaption="'Tháng sau'" :prevMonthCaption="'Tháng trước'" :weekdays="['T2', 'T3', 'T4', 'T5', 'T6', 'T7', 'CN']" :months="[
        'Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4',
        'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8',
        'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'
    ]">
    </date-pick> -->
    <date-picker v-model="date" 
                 :lang="lang" 
                 :format="format" 
                 :disabled="readonly"
                  placeholder="DD/MM/YYYY"
                 :disabled-date="((date) => date > new Date(target))">
    </date-picker>
</template>

<script>
// import DatePick from 'vue-date-pick';
// import 'vue-date-pick/dist/vueDatePick.css'

// import Base from '../../base/base'
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';
import 'vue2-datepicker/locale/vi';
export default {
    name: 'TheDatepicker',
    data: function () {
        return {
            date: '',
            format: 'DD/MM/YYYY',
            lang: {
                locale:'vi'
            },
            querySelect: '',
            invalid:true
        }
    },
    mounted(){
        // this.date = new Date(this.dataDate);
        // document.querySelectorAll(this.querySelect)[0].setAttribute("tabindex", this.tab);
    },
    created() {
        this.querySelect = '#'+this.idDate + ' .mx-datepicker input';
        this.date = this.dataDate ? new Date(this.dataDate) : null;
        // console.log(this.date);
    },
    props: {
        dataDate: [String, Date],
        tab:String,
        idDate:String,
        checkDateValid:Boolean,
        target: [String,Date],
        readonly: Boolean,
    },
    components: {
        // DatePick,
        DatePicker
    },
    watch: {
        date(){
            this.$emit('changeDate', this.date);
            // console.log(this.date);
        },
        // dataDate(){
        //     this.date = new Date(this.dataDate);
        //     // this.date = this.dataDate;
        //     // console.log(this.date);
        //     this.$emit('changeDate', this.date);
        //     // console.log(this.date);
        // },
        // readonly(){
        //     document.querySelector("input").readOnly = (this.readonly==true)?true:false;
        //     console.log(this.readonly);
        // }
    }
}
</script>

<style>
.vdpComponent {
    display: block !important;
    font-size: 12px!important;
}
button.vdpClearInput {
    width: 30px !important;
}

.mx-datepicker{
    width: 100%;
    color: black;
}

.mx-input{
    border: 1px solid var(--gray-color);
    box-shadow: none;
}

.mx-calendar-header-label{
    color: var(--primary-color);
}

.mx-calendar-header-label:hover{
    color: var(--hover-admin-color);
}

.mx-table-date .today, .mx-btn,.mx-btn:hover{
    color: var(--hover-admin-color);
}

.mx-input:hover{
    border: 1px solid var(--gray-color) !important;
}

.mx-input:focus{
    border-color:var(--hover-admin-color) !important;
}

.mx-datepicker .mx-input{
    height: 30px !important;
    font-size: 12px !important;
    font-family: 'GoogleSans-Regular' !important;
    padding-left: 12px !important;
    color: black;
}

.mx-table tr{
    background-color: white !important;
    color: black;
}

.mx-table tr:hover{
    background-color: white !important;
}

.mx-calendar-content .cell:hover{
    background-color: var(--hover-admin-color) !important;
    color: white;
}

.mx-calendar-content .cell.active{
    background-color: var(--hover-admin-color);
}
</style>
