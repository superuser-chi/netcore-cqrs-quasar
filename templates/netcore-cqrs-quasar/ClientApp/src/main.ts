import Vue from "vue";
import App from "./App.vue";
import "./plugins/axios";
import router from "./router";
import store from "./store/index";
import "./quasar";
import dateFilter from "@/filters/date.filter";
import Default from "@/layouts/default-layout.vue"
import FullScreen from "@/layouts/fullscreen-layout.vue"

Vue.component("default-layout", Default);
Vue.component("fullscreen-layout", FullScreen);

Vue.config.productionTip = false;

Vue.filter("date", dateFilter);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
