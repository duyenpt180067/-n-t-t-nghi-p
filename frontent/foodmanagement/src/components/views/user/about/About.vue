<template>
<div class="about">
    <div class="wellcome">
        <div class="flex">
            <div class="wellcome-init w-3/5">
                <div class="wellcome-img">
                    <img src="../../../../assets/lib/img/common/wellcome.png" alt="">
                </div>
                <div class="wellcome-title">
                    <h1>Best burger ideas & traditions from the whole world</h1>
                </div>
                <div class="wellcome-content">
                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam.</p><br>
                    <p>Mauris tempus erat laoreet turpis lobortis, eu tincidunt erat fermentum. Aliquam non tincidunt urna. Integer tincidunt nec nisl vitae ullamcorper. Proin sed ultrices erat.</p>
                    <router-link tag="a" to="/user/shop"><button type="">ORDER NOW</button></router-link>
                </div>
            </div>
            <div class="wellcome-img12 w-1/3 fade-in-top m-r-12">
                <img src="../../../../assets/lib/img/about/image1-h4.jpg" alt="">
            </div>
            <div class="wellcome-img12 w-1/3">
                <img class="fade-in-top" src="../../../../assets/lib/img/about/image2-h4.jpg" alt="">
                <img class="fade-in-top" style="margin-top: 20px;" src="../../../../assets/lib/img/about/image3-h4.jpg" alt="">
            </div>
        </div>
    </div>

    <div class="quality">
        <div class="flex quality1">
            <div class="quality-content w-3/5">
                <div class="quality-img">
                    <img src="../../../../assets/lib/img/common/quality.png" alt="">
                </div>
                <div class="quality-title">
                    <h3>Chicken</h3>
                </div>
                <div class="quality-main">
                    <p>Quality is our #1 ingredient. That’s why our Chicken Wings, Chicken Bites and Grilled Chicken Topping are made from chickens raised without antibiotics and fed an all vegetable-grain diet, with no animal by-products. Plus, our Bites are
                        made with 100% chicken breast meat.</p>
                    <router-link tag="a" to="/user/shop"><button type="">ORDER NOW</button></router-link>
                </div>
            </div>
            <div class="img-quality w-2/5 fade-in-left">
                <img src="../../../../assets/lib/img/about/about-imager-5.png" alt="">
            </div>
        </div>
        <div class="flex quality1">
            <div class="img-quality w-2/5 wow animate__animated animate__fadeInTopRight">
                <img style="width: 75%;" src="../../../../assets/lib/img/about/about-imager-6.png" alt="">
            </div>
            <div class="quality-content w-3/5">
                <div class="quality-img">
                    <img src="../../../../assets/lib/img/common/quality.png" alt="">
                </div>
                <div class="quality-title">
                    <h3>Burger</h3>
                </div>
                <div class="quality-main">
                    <p>Some of the world’s best cheese is made close to home! All our deliciously melty Mozzarella is made with 100% Canadian milk. We’re proud to support Canadian dairy farmers.</p>
                    <router-link tag="a" to="/user/shop"><button type="">ORDER NOW</button></router-link>
                </div>
            </div>
        </div>
        <div class="flex quality1">
            <div class="quality-content w-3/5">
                <div class="quality-img">
                    <img src="../../../../assets/lib/img/common/quality.png" alt="">
                </div>
                <div class="quality-title">
                    <h3>Pizza Douch</h3>
                </div>
                <div class="quality-main">
                    <p>Quality is our #1 ingredient. That’s why our Chicken Wings, Chicken Bites and Grilled Chicken Topping are made from chickens raised without antibiotics and fed an all vegetable-grain diet, with no animal by-products. Plus, our Bites are
                        made with 100% chicken breast meat.</p>
                    <router-link tag="a" to="/user/shop"><button type="">ORDER NOW</button></router-link>
                </div>
            </div>
            <div class="img-quality w-2/5 fade-in-left">
                <img src="../../../../assets/lib/img/about/about-imager-7.png" alt="">
            </div>
        </div>
    </div>

    <div class="comment">
        <div class="comment-img">
            <img src="../../../../assets/lib/img/about/about-pizza.jpg" alt="">
        </div>
    </div>

    <div class="chefs">
        <div class="chef-title fade-in-top">
            <img src="../../../../assets/lib/img/about/about-quality.png" alt="">
            <p>Our Talented Chefs</p>
        </div>
        <div class="chefs-main flex">
            <div class="w-1/3 chefs-one m-r-12">
                <div class="chef-img">
                    <img class="fade-in-top" src="../../../../assets/lib/img/chef/box-team-1-2.jpg" alt="">
                </div>
                <div class="position">
                    <p>CHEF</p>
                </div>
                <h4>WILLIAM SMITH</h4>
                <p>Everything We Pizza, We Pizza With Love. Designer Fastfood.</p>
                <div class="contact-login">
                    <ul>
                        <li><a><i class="fab fa-facebook"></i></a></li>
                        <li><a><i class="fab fa-twitter"></i></a></li>
                        <li><a><i class="fab fa-google-plus"></i></a></li>
                    </ul>
                </div>
            </div>
            <div class="w-1/3 chefs-one m-r-12">
                <div class="chef-img">
                    <img class="fade-in-top" src="../../../../assets/lib/img/chef/box-team-6-1.jpg" alt="">
                </div>
                <div class="position">
                    <p>MANAGER</p>
                </div>
                <h4>JOHN DOE</h4>
                <p>Everything We Pizza, We Pizza With Love. Designer Fastfood.</p>
                <div class="contact-login">
                    <ul>
                        <li><a><i class="fab fa-facebook"></i></a></li>
                        <li><a><i class="fab fa-twitter"></i></a></li>
                        <li><a><i class="fab fa-google-plus"></i></a></li>
                    </ul>
                </div>
            </div>
            <div class="w-1/3 chefs-one m-r-12">
                <div class="chef-img">
                    <img class="fade-in-top" src="../../../../assets/lib/img/chef/box-team-2-1.jpg" alt="">
                </div>
                <div class="position">
                    <p>CHEF</p>
                </div>
                <h4>BRADD L.</h4>
                <p>Everything We Pizza, We Pizza With Love. Designer Fastfood.</p>
                <div class="contact-login">
                    <ul>
                        <li><a><i class="fab fa-facebook"></i></a></li>
                        <li><a><i class="fab fa-twitter"></i></a></li>
                        <li><a><i class="fab fa-google-plus"></i></a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="delivery">
        <div class="flex delivery-init">
            <div class="w-1/3 img-fastfood">
                <img src="../../../../assets/lib/img/food/fast-food-1.png" alt="">
            </div>
            <div class="delivery-content w-1/3">
                <h4 class="fade-in-top" style="color: #ffc222; font-style: italic;">We guarantee</h4>
                <h3 class="fade-in-top">30 Minutes Delivery!</h3>
                <p class="fade-in-top">If you’re having a meeting, working late at night and need an extra push. Let us know and we will be there</p>
                <router-link tag="a" to="/user/shop">
                    <button type="" class="fade-in-top">MAKE AN ORDER</button>
                </router-link>
            </div>
            <div class="w-1/3 img-ship">
                <img src="../../../../assets/lib/img/contact/shipper.jpg" alt="">
            </div>
        </div>
    </div>
</div>
</template>

<script>
export default {
    name:'About',
}
</script>

<style>
.about p{
    font-size: 15px;
}
.about ul {
    padding: 0px;
    list-style-type: none;
    display: flex;
    justify-content: center;
    margin-top: 10px;
}

.about ul>li {
    margin: 0 5px;
}
.about a i{
    text-decoration: none;
    color: #0f172b;
    font-size: 15px;
}

.about a:hover i{
    text-decoration: none;
    color: #ffc222;
}
.about .wellcome {
    margin: 100px 120px;
}

.about .quality {
    margin: 50px 120px;
}

.about .chefs {
    margin: 50px 100px;
    text-align: center;
}

.about .delivery {
    margin: 0;
    padding: 0 35px;
}

.about .delivery-init {
    margin: 70px 0;
    background-color: floralwhite;
    text-align: center;
}

.about .delivery img {
    width: 100%;
    height: 100%;
}

.about .delivery-content {
    margin: 67px 0;
}

.about .wellcome-img img {
    width: 155px;
}

.about .wellcome-title h1 {
    font-size: 58px;
    font-weight: 800;
}

.about .wellcome-content {
    color: #a3a3a3;
    margin-top: 25px;
    font-size: 16px;
}

.about .wellcome-img12 img {
    border-radius: 10px;
    transition: 300ms;
}

.about .wellcome-img12 img:hover {
    transform: translateY(-8px);
}

.about button {
    border-radius: 6px;
    height: 50px;
    width: 150px;
    background-color: #ffc222;
    color: #0f172b;
    font-weight: 700;
    transition: 200ms;
    border: none;
    font-size: 15px;
    margin-top: 25px;
}

.about button:hover {
    color: white;
}

.about button:focus {
    outline: none;
}

.about .quality-title h3 {
    font-weight: 700;
    font-size: 50px;
}

.about .img-quality img {
    width: 100%;
}

.about .quality-main p {
    color: #a3a3a3;
    margin: 30px 40px 20px 0;
}

.about .quality1 {
    margin-top: 50px;
}

.about .comment-img img {
    width: 100%;
    height: 580px;
    filter: brightness(0.5);
    margin: 50px 0;
}

.about .chef-title {
    margin-bottom: 30px;
    font-size: 40px;
    font-weight: 750;
}

.about .chefs-main img {
    border-radius: 10px;
    transition: 300ms;
    width: 100%;
}

.about .chefs-one{
    position: relative;
}
.about .chefs-one:hover img {
    filter: brightness(0.5);
}

.about .position {
    margin-top: -25px;
    position: absolute;
    width: 100%;
}

.about .chefs-one h4 {
    font-weight: 700;
    padding-top: 50px;
}

.about .chefs-one p {
    color: #a3a3a3;
}

.about .position p {
    background-color: #ffc222;
    width: fit-content;
    padding: 10px 20px;
    border-radius: 10px;
    color: black;
    margin: auto;
}
</style>