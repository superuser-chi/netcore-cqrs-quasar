<template>
  <q-layout view="lHh Lpr lFf" class="bg-white">
    <q-header elevated height-hint="150">
      <toolbar />
    </q-header>

    <left-drawer />

    <q-page-container>
      <q-scroll-area
        :thumb-style="thumbStyle"
        :content-style="contentStyle"
        :content-active-style="contentActiveStyle"
        style="height: 100vh; max-width: 100%;"
      >
        <router-view class="q-ma-md" />
      </q-scroll-area>
    </q-page-container>
  </q-layout>
</template>
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import LeftDrawer from "@/components/LeftDrawer.vue";
import Toolbar from "@/components/Toolbar.vue";
@Component({
  components: {
    LeftDrawer,
    Toolbar,
  },
})
export default class Default extends Vue {
  contentStyle = {
    color: "#555",
  };
  contentActiveStyle = {
    backgroundColor: "#eee",
    color: "black",
  };
  thumbStyle = {
    right: "2px",
    borderRadius: "5px",
    backgroundColor: "#027be3",
    width: "5px",
    opacity: 0.75,
  };
}
</script>

<style lang="sass">
.YL
  &__toolbar-input-container
    min-width: 100px
    width: 55%
  &__toolbar-input-btn
    border-radius: 0
    border-style: solid
    border-width: 1px 1px 1px 0
    border-color: rgba(0,0,0,.24)
    max-width: 60px
    width: 100%
  &__drawer-footer-link
    color: inherit
    text-decoration: none
    font-weight: 500
    font-size: .75rem
    &:hover
      color: #000
</style>
