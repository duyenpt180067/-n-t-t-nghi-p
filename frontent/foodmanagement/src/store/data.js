import Vue from "vue";
import Vuex from 'vuex';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        notiOrderStatus: 0,
        orderChange: ""
    },

    getters: {
        getNotiOrderStatus: state => state.notiOrderStatus,
        getOrderChange: state => state.orderChange,
    },

    mutations: {
        changeOrder(state, obj) {
            state.notiOrderStatus = obj.OrderStatus;
            state.orderChange = obj.OrderCode;
        },
    },

    actions: {
        handleChangeOrder(context, obj) {
            context.commit("changeOrder", obj);
        },

    }
})