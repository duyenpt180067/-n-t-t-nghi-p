<template>
<div class="footer">
    <div class="icon-footer flex-center">Project</div>
    <div class="flex">
        <div class="w-1/4 content-footer center">
            <h3>ADDRESS</h3>
            <p>570 8th Ave,<br>New York, NY 10018<br>United States</p>
        </div>
        <div class="w-1/4 content-footer center">
            <h3>BOOK A TABLE</h3>
            <p>Dogfood och Sliders foodtruck.<br>Under Om oss kan ni läsa<br>
                <span style="color:#ffc222; font-size: 20px;font-weight:700">(850) 435-4155</span></p>
        </div>
        <div class="w-1/4 content-footer center">
            <h3>OPENING HOURS</h3>
            <p>Monday – Friday: 8am – 4pm<br>Saturday: 9am – 5pm</p>
            <div class="contact-link flex-center">
                <div class="elementor-grid-item"> 
                    <a class="" href="https://www.facebook.com/opalwordpress/" target="_blank">
                        <i class="fab fa-facebook"></i>
                    </a>
                </div>
                <div class="elementor-grid-item"> 
                    <a class="" href="#" target="_blank"> 
                        <i class="fab fa-twitter"></i> 
                    </a>
                </div>
                <div class="elementor-grid-item"> 
                    <a class="" href="https://www.youtube.com/user/WPOpalTheme" target="_blank"> 
                        <i class="fab fa-youtube"></i> 
                    </a>
                </div>
                <div class="elementor-grid-item"> 
                    <a class="" href="https://demo2.pavothemes.com/project/menu-restaurant" target="_blank"> 
                        <i class="fab fa-wordpress"></i>
                    </a>
                </div>
            </div>
        </div>
        <div class="w-1/4 content-footer center">
            <h3>NEWSLETTER</h3>
            <p>Subscribe to the weekly newsletter<br>for all the latest updates</p>
        </div>
    </div>
</div>
</template>

<style>
.icon-footer{
    font-size: 55px;
    font-style: italic;
    color: #fff;
    text-shadow: -1px 0 #00a850, 0 1px #00a850, 1px 0 #00a850, 0 -1px #00a850;
    font-weight: bold;
}
.icon-footer::before{
    border-top: 2px solid #fff;
    content: "";
    width: calc(50% - 95px);
    position: absolute;
    left: 0;
}
.icon-footer::after{
    border-top: 2px solid #fff;
    content: "";
    width: calc(50% - 95px);
    position: absolute;
    right: 0;
}
.fadeleft{
    width: 56px!important;
}
.textleft{
    font-size: 0 !important;
}
.logoleft{
    width: 0 !important;
    transition: 100ms;
}

.nav-item.router-link-active{
    background-color: #5d5d61;
    font-weight: bold;
    border-left: 5px solid #1c3ba0 !important;

}

.toggle-icon{
    cursor: pointer;
}

.toggle-icon .router-link-active{
    background-color: #393a3d;
    border-left: none !important;
}
</style>

<script>
    export default {
        name:'TheFooter',
    }
</script>