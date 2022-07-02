<template>
<div>
    <banner-common :title="'FAQ'" :displayTitle="'Câu hỏi thường gặp'"></banner-common>
    <div class="content-faq" style="text-align: center;">
        <div class="faq-item pointer" style="text-align: left;" v-for="(data, index) in faqList" :key="data.FaqId">
            <div class="faq-init">
                <div class="faq-question" :class="{'active-faq':activeFaq==index}" @click="activeFaq=index">{{data.FaqQuestion}}</div>
                <div class="faq-answer" :class="{'h-0':activeFaq!=index}">{{data.FaqAnswer}}</div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
import FaqAPI from '../../../../api/component/bussiness-item/FaqAPI';
import BannerCommon from '../common/BannerCommon.vue'
export default {
    components: { BannerCommon },
    name:'Faq',
    data(){
        return{
            faqList:[],
            activeFaq: 0,
        }
    },
    async created() {
        try {
            let faqData = await FaqAPI.getFilterPaging({FaqFilter:null,PageNumber:null,PageSize:null});
            this.faqList = faqData.data;
        } catch (error) {
            console.log(error);
        }
    },
}
</script>

<style>
.faq-item{
    width: 700px;
    margin: 20px auto;
    /* margin-top: 10px; */
}
.faq-item .faq-question,
.faq-item .faq-answer{
    padding: 10px;
    font-weight: bold;
    font-size: 15px;
    border: 1px solid var(--gray-color);
    border-radius: 5px;
}
.faq-item .faq-answer{
    font-weight: 100;
    font-size: 14px;
    overflow: hidden;
    transition: .2s;
}
.faq-item .faq-answer.h-0{
    padding: 0;
    border: 0px;
}
.faq-item .faq-question.active-faq{
    background-color: var(--primary-color);
}
</style>