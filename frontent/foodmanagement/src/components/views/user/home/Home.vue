<template>
    <div class="home">
        <banner></banner>
        <category></category>
        <introduce></introduce>
        <popular-dish></popular-dish>
    </div>
</template>

<style>
    
</style>
<script>
import Banner from './Banner.vue'
import Category from './Category.vue'
import Introduce from './Introduce.vue'
import PopularDish from './PopularDish.vue'
// import TheSelect from '../base/TheSelect.vue';
export default {
  components: { Banner, Category, Introduce, PopularDish },
//   components: { TheSelect },
    props:{
    },
    name:'Home',
    created(){
        let user = JSON.parse(localStorage.getItem('user'));
        if(user && user.isEmployee){
            this.$emit('changeUser', false);
            this.$router.push({ path: '/app/home' });
        }
    },
}
</script>