<template>
<v-app :class="{'h-100vh': isEmployee}">
    <div class="head" style="z-index: 1;">
        <div class="head-contact flex align-items">
            <div class="flex contact-addr w-1/2 align-items" style="min-width: 385px;">
                <div class="call-us w-1/2 flex align-items">
                    <div class="logo-icon icon-small-phone"></div>
                    <div>CALL US: +381 65 666 6666</div>
                </div>
                <div class="contact-address w-1/2 flex align-items">
                    <div class="logo-icon icon-small-addr"></div>
                    <div>71 Madison Ave</div>
                </div>
            </div>
            <div class="contact-link w-1/2 flex" style="justify-content: end;">
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
    </div>
    <the-header :tabActive="tabActive" v-if="!isEmployee"></the-header>
    <the-menu :fadeleft="fadeleft" @fadeLeft="fadeLeft" @fade="fadeRight" v-if="isEmployee" @removeNavTop="removeNavTop"></the-menu>
    <router-view :fadeleft="fadeleft" @changeUser="changeUser"></router-view>
    <the-footer v-if="!isEmployee"></the-footer>
    <div id="rasa-chat-widget" data-websocket-url="http://localhost:8081/" style="bottom: 60px;"></div>
    <script src="https://unpkg.com/@rasahq/rasa-chat" type="application/javascript"></script>
</v-app>
</template>

<style>
@import url('./assets/lib/fonts/fontawesome-5.15.1/css/all.min.css');
@import url('./assets/css/common/animate.min.css');
@import url('./assets/css/common/common.css');
@import url('./assets/css/common/popup.css');
@import url('./assets/css/common/header.css');
@import url('./assets/css/common/footer.css');
@import url('./assets/css/common/content.css');
@import url('./assets/css/common/grid.css');

html {
    overflow-y: auto !important;
    overflow-x: hidden !important;
}

#rasa-chat-widget-container{
    bottom: 60px;
}

.css-1kgb40s{
    left: 20px !important;
    right: auto !important;
}

#rasa-chat-widget-container > .css-vurnku{
    height: 65px;
    width: 100%;
}
</style>

<script>
import TheHeader from './components/layout/TheHeader.vue'
import TheFooter from './components/layout/TheFooter.vue'
import TheMenu from './components/layout/TheMenu.vue'
import {Tabs} from './base/Resources'
export default {
    name: 'App',
    data(){
        return {
            tabActive: Tabs.Home,
            isEmployee: false,
            fadeleft: false,
            windowWidth: window.innerWidth,
            user: localStorage.getItem("user"),
        }
    },
    components: {
        TheHeader,
        TheFooter,
        TheMenu,
    },
    mounted() {
        this.$nextTick(() => {
            window.addEventListener('resize', this.onResize);
        })
    },

    beforeDestroy() {
        window.removeEventListener('resize', this.onResize);
    },

    watch: {
        windowWidth(newWidth) {
            if (newWidth <= 1150) {
                this.fadeleft = true;
            }
        },
        user(){
            if(localStorage.getItem("user")){
                this.isEmployee = JSON.parse(localStorage.getItem("user")).isEmployee;
            }
            else {
                this.isEmployee = false;
            }
        }
    },
    created() {
        if (this.windowWidth <= 1150) {
            this.fadeleft = true;
        } else this.fadeleft = false;
        if(localStorage.getItem("user")){
            this.isEmployee = JSON.parse(localStorage.getItem("user")).isEmployee;
        }
        else {
            this.isEmployee = false;
        }
        setTimeout(function(){
            let isEmp = false
            if(localStorage.getItem("user")){
                isEmp = JSON.parse(localStorage.getItem("user")).isEmployee;
            }
            else {
                isEmp = false;
            }
            if(document.querySelector('.css-qmypsf.large') && document.querySelector('.css-qmypsf.large').classList){
                if(isEmp){
                    document.querySelectorAll('.css-qmypsf.large').forEach(el =>{
                        el.classList.add('display-none');
                    })
                }
                else { 
                    document.querySelectorAll('.css-qmypsf.large').forEach(el =>{
                        el.classList.remove('display-none');
                    })
                }
            }
        }, 500)
    },
    methods:{
        removeNavTop(){
            this.isEmployee = true;
        },
        /**
         * Thu nhỏ menu
         * Create:PTDuyen(20/08/2021)
         */
        fadeLeft() {
            this.fadeleft = true;
        },

        /**
         * Phóng to chiều rộng menu
         * Create:PTDuyen(20/08/2021)
         */
        fadeRight() {
            this.fadeleft = false;
        },

        changeUser(val){
            this.isEmployee = val;
        },

        /**
         * Lấy chiều rộng của màn hình
         * Create: PTDuyen(20/08/2021)
         */
        onResize() {
            this.windowWidth = window.innerWidth
        },
    }
};
</script>
