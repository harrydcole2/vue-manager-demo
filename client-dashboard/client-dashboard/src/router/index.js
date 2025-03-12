import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import Login from "../views/Login.vue";
import Help from "../views/Help.vue";
import ClientDashboard from "../views/ClientDashboard.vue";
import ClientManagement from "../views/ClientManagement.vue";
import store from "../store";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/login",
    name: "Login",
    component: Login,
    meta: { guestOnly: true },
  },
  {
    path: "/help",
    name: "Help",
    component: Help,
  },
  {
    path: "/clients",
    name: "ClientDashboard",
    component: ClientDashboard,
    meta: { requiresAuth: true },
  },
  {
    path: "/clients/:id",
    name: "ClientManagement",
    component: ClientManagement,
    meta: { requiresAuth: true },
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach((to, from, next) => {
  const isLoggedIn = store.getters["auth/isAuthenticated"];

  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (!isLoggedIn) {
      next({ name: "Login" });
    } else {
      next();
    }
  } else if (to.matched.some((record) => record.meta.guestOnly)) {
    if (isLoggedIn) {
      next({ name: "ClientDashboard" });
    } else {
      next();
    }
  } else {
    next();
  }
});

export default router;
