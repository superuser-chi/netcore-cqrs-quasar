<template>
  <div id="app" class="bg-grey-1">
    <component :is="layout">
      <router-view />
    </component>
  </div>
</template>
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
@Component
export default class Home extends Vue {
  get layout() {
    return `${this.$route.meta?.layout || "default"}-layout`;
  }
}
</script>
<style lang="sass">
html
    overflow: scroll
    overflow-x: hidden
::-webkit-scrollbar
    width: 0px  /* Remove scrollbar space */
    background: transparent  /* Optional: just make scrollbar invisible */
/* Optional: show position indicator in red */
::-webkit-scrollbar-thumb
    background: #FF0000
</style>
