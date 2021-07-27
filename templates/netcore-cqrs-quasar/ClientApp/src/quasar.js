import Vue from "vue";

import "./styles/quasar.sass";
import "quasar/dist/quasar.ie.polyfills";
import "@quasar/extras/roboto-font/roboto-font.css";
import "@quasar/extras/material-icons/material-icons.css";
import "@quasar/extras/fontawesome-v5/fontawesome-v5.css";
import "@quasar/extras/ionicons-v4/ionicons-v4.css";
import { Quasar, Notify } from "quasar";

Vue.use(Quasar, {
  config: {},
  plugins: {Notify}
});
