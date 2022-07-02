<template>
<div class="blog-detail">
    <div class="flex">
        <div class="blog-item w-3/5 m-r-26">
            <div class="blog-img center">
                <img :src="blog.BlogImage" alt="">
            </div>
            <div class="blog-title">{{blog.BlogTopic}}</div>
            <div class="blog-topic">{{blog.BlogTitle}}</div>
            <div class="blog-intro">{{blog.BlogIntro}}</div>
            <div class="blog-intro">{{blog.BlogContent}}</div>
            <div class="blog-intro"><b>{{blog.BlogHighlight}}</b></div>
            <div class="blog-quote">"{{blog.BlogQuote}}"</div>
            <div class="blog-intro">{{blog.BlogOther}}</div>
            <div class="blog-quote">{{blog.BlogSummary}}</div>
        </div>
        <div class="w-2/5">
            <div class="lastest-news">
                <b style="font-size:20px;">Bài viết mới nhất</b>
                <div class="newest">
                    <div @click="reloadPage(item.BlogCode)" class="newest-item flex pointer" v-for="item in listBlog.slice(0, 5)" :key="item.BlogCode">
                        <div class="newest-img w-1/4 m-r-12">
                            <img :src="item.BlogImage" alt="">
                        </div>
                        <div class="w-3/4">
                            <div class="newest-title"><b>{{item.BlogTopic}}</b></div>
                            <div class="newest-intro">{{item.BlogIntro}}</div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="prev-next" v-if="listBlog.length>=2">
                <div class="prev-next-item prev-item pointer" @click="reloadPage(blogPrev.BlogCode)" v-if="blogPrev!=null">
                    <div><p>Bài viết trước</p></div>
                    <b class="prev-next-item-title">{{blogPrev.BlogTopic}}</b>
                </div>
                <div class="prev-next-item next-item pointer" @click="reloadPage(blogNext.BlogCode)" v-if="blogNext!=null">
                    <div><p>Bài viết sau</p></div>
                    <b class="prev-next-item-title">{{blogNext.BlogTopic}}</b>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
import BlogAPI from '../../../../api/component/bussiness-item/BlogAPI'
export default {
    name:'BlogDetail',
    data(){
        return {
            blog:{},
            listBlog:[],
            blogPrev:null,
            blogNext: null,
        }
    },
    async created(){
        this.listBlog = (await BlogAPI.getFilterPaging({BlogFilter: null, PageNumber:null, PageSize: null})).data;
        this.listBlog.forEach((e, index) => {
            if(this.$route.params.blogCode == e.BlogCode){
                this.blog = e;
                if(this.listBlog.length-2 >= index >= 1){
                    this.blogPrev = this.listBlog[index-1];
                    this.blogNext = this.listBlog[index+1];
                }
                else if(index == 0){
                    this.blogPrev = null;
                    this.blogNext = this.listBlog[index+1];
                }
                else if(index == this.listBlog.length-1){
                    this.blogPrev = this.listBlog[index-1];
                    this.blogNext = null;
                }
                return;
            }
        });
    },
    methods:{
        reloadPage(code){
            this.$router.push({
                path: '/user/blog/'+code, 
                params: {
                    blogCode: code,
                }
            });
            location.reload();
        },
    }
}
</script>

<style>
.blog-detail .blog-item{
    margin-left: 20px;
}
.blog-quote{
    font-size: 15px;
    color: #000;
    padding: 0 30px 20px;
}
.prev-next,
.lastest-news{
    height: fit-content;
    padding: 20px 15px;
    margin-right: 20px;
    margin-top: 20px;
    border: 1px solid var(--gray-color);
    border-radius: 10px;
}
.prev-next{
    border: 0;
    padding: 10px 0;
}
.newest-item{
    margin: 20px 0;
}
.newest-item:hover > div .newest-title{
    color: var(--primary-color);
}
.newest-item .newest-img img{
    width: 100%;
    border-radius: 3px;
}
.newest-item .newest-intro{
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
    overflow: hidden;
    display: -webkit-box;
}
.prev-next-item {
    background-color: #f7f4ef;
    padding: 10px;
    margin: 20px 0;
    border-radius: 5px;
}
.prev-next-item:hover .prev-next-item-title{
    color: var(--primary-color);
}
.prev-next-item .prev-next-item-title{
    font-size: 20px;
}
</style>