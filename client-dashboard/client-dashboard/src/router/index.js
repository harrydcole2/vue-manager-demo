import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import Login from "../views/Login.vue";
import Help from "../views/Help.vue";
import ClientDashboard from "../views/ClientDashboard.vue";
import Archive from "../views/Archive.vue";
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
    meta: {
      guest: true,
    },
  },
  {
    path: "/help",
    name: "Help",
    component: Help,
  },
  {
    path: "/clients",
    redirect: "/clients/active",
  },
  {
    path: "/clients/active",
    name: "ClientDashboard",
    component: ClientDashboard,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/archive",
    name: "Archive",
    component: Archive,
  },
  {
    path: "/clients/:id",
    name: "ClientManagement",
    component: ClientManagement,
    meta: {
      requiresAuth: true,
    },
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (!store.getters["auth/isAuthenticated"]) {
      next({
        path: "/login",
        query: { redirect: to.fullPath },
      });
    } else {
      next();
    }
  } else if (to.matched.some((record) => record.meta.guest)) {
    if (store.getters["auth/isAuthenticated"]) {
      next({ path: "/clients/active" });
    } else {
      next();
    }
  } else {
    next();
  }
});

export default router;
