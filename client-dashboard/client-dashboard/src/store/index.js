import Vue from "vue";
import Vuex from "vuex";
import auth from "./modules/auth";
import clients from "./modules/clients";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  modules: {
    auth,
    clients,
  },
});
