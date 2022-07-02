<template>
<div class="home blog-list">
    <div class="blog-item" v-for="blog in listBlog.slice(0, 9)" :key="blog.BlogCode">
        <div class="blog-img center">
            <img :src="blog.BlogImage" alt="">
        </div>
        <div class="blog-title">{{blog.BlogTopic}}</div>
        <div class="blog-topic">{{blog.BlogTitle}}</div>
        <div class="blog-intro">{{blog.BlogIntro}}</div>
        <div class="read-more">
            <router-link tag="button" :to="'/user/blog/' + blog.BlogCode">Read more</router-link>
        </div>
    </div>
</div>
</template>

<script>
import BlogAPI from '../../../../api/component/bussiness-item/BlogAPI'
export default {
    name: 'BlogList',
    data(){
        return {
            listBlog: [],
        }
    },
    async created(){
        try {
            this.listBlog = (await BlogAPI.getFilterPaging({BlogFilter: null, PageNumber:null, PageSize: null})).data; 
        } catch (error) {
            console.log(error);
        }
    }
}
</script>

<style>
.blog-item{
    margin: 20px 50px;
    padding: 20px;
    border: 1px solid var(--gray-color);
    border-radius: 10px;
}
.blog-item .blog-img{
    margin: 25px 0;
    border-radius: 10px;
}
.blog-item .blog-img img{
    max-width: 880px;
    width: 100%;
    height: 500px;
    border-radius: 10px;
}
.blog-item .blog-title{
    font-size: 23px;
    font-weight: bold;
    padding: 0 30px 20px;
}
.blog-item .blog-topic,
.blog-item .blog-intro{
    font-size: 15px;
    color: #808080;
    padding: 0 30px 20px;
}
.blog-item .read-more{
    padding: 0 30px 20px;
}
</style>