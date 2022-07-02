<template>
    <v-pagination v-model="currentPage" 
    :page-count="totalPages" 
    :classes="bootstrapPaginationClasses" 
    :labels="customLabels" 
    @change="getEmployeesPage"></v-pagination>                 
</template>

<script>
import vPagination from 'vue-plain-pagination';
export default {
    'name': 'ThePagination',
    components:{
        vPagination
    },
    props:{
        currPage: Number,
        totalPages: Number
    },
    data(){
        return {
            currentPage: 1,
            // lớp của thư viện pagination
            bootstrapPaginationClasses: {
                ul: 'pagination',
                li: 'page-item',
                liActive: 'active',
                liDisable: 'disabled',
                button: 'page-link'
            },
            customLabels: {
                first: false,
                prev: 'Trước',
                next: 'Sau',
                last: false
            },
        }
    },
    methods:{
        getEmployeesPage(){
            this.$emit('getEmployeesPage', this.currentPage);
        }
    }
}
</script>