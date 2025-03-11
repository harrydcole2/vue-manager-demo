import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import Login from "../views/Login.vue";
import Help from "../views/Help.vue";
import ClientList from "../views/ClientList.vue";
import ClientDetails from "../views/ClientDetails.vue";
import store from "../store";

Vue.use(VueRouter);

const routes = [
  { path: "/", component: Home },
  { path: "/login", component: Login },
  { path: "/help", component: Help },
  {
    path: "/clients",
    component: ClientList,
    beforeEnter: (to, from, next) => {
      store.state.auth.user ? next() : next("/login");
    },
  },
  {
    path: "/clients/:id",
    component: ClientDetails,
    beforeEnter: (to, from, next) => {
      store.state.auth.user ? next() : next("/login");
    },
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
