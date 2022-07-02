import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import store from './store/data'
import VueRouter from 'vue-router'
import { routes } from './router/router'
import 'material-icons'
import axios from 'axios'
import VueAxios from 'vue-axios'
import Vuelidate from 'vuelidate'
import Toasted from 'vue-toasted'
import VTooltip from 'v-tooltip'
import VueWow from 'vue-wow'

// mount with global
Vue.use(VueWow)
Vue.use(require('vue-shortkey'))
Vue.use(VTooltip)
Vue.use(Vuelidate)
Vue.use(VueRouter);
Vue.use(VueAxios, axios);
Vue.use(Toasted);

Vue.config.productionTip = false

const router = new VueRouter({
    scrollBehavior() {
        return { x: 0, y: 0 };
    },
    mode: 'history',
    routes,
})

new Vue({
    vuetify,
    store,
    router,
    render: h => h(App)
}).$mount('#app')