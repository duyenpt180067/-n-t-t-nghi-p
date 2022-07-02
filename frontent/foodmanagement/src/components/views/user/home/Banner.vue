<template>
<div class="banner">
    <div class="img-banner">
        <img src="../../../../assets/lib/img/banner/h1_shape-1.png" style="background-color:#333;" alt="">
    </div>
    <div class="img-tomato">
        <img src="../../../../assets/lib/img/banner/h1_tomato.png" alt="">
    </div>
    <div class="img-vegetables">
        <img src="../../../../assets/lib/img/banner/h1_shape-5.png" alt="">
    </div>
    <div class="img-pizza">
        <img src="../../../../assets/lib/img/banner/h1_pizza.png" alt="">
    </div>
    <div class="spice1">
        <img src="../../../../assets/lib/img/banner/h1_shape-6.png">
    </div>
    <div class="banner-order">
        <h1 class="unlimited" style="font-weight: 700">UNLIMITED <br>MEDIUM <br><span style="color: var(--primary-color)">PIZZAS</span></h1>
        <h3 class="description-banner">Medium 2-topping* pizza</h3>
        <p class="description-order">*Additional charge for premium toppings. Minimum of 2 required.</p>
        <router-link tag="a" to="/user/shop" class="order-btn"><button type="">ORDER NOW</button></router-link>
    </div>
</div>
</template>
<style scoped>
@import url('../../../../assets/css/user/home/banner.css');
</style>
<script>
export default {
    name:'Banner'
}
</script>