import Vue from "vue";
import VueRouter from "vue-router";
import routes from "./router";
import store from "../store";

Vue.use(VueRouter);

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach((to, from, next) => {
  const authenticatedUser = store.getters["app/authenticated"];
  const requiresAuth = to.matched.some((record) => record.meta.requiresAuth);

  if (requiresAuth && !authenticatedUser) next("login");
  else next();
});
export default router;
