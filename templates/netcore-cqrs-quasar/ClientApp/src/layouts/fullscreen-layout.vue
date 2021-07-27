<template>
  <q-layout view="lHh Lpr lFf">
    <q-page-container>
      <slot />
    </q-page-container>
  </q-layout>
</template>
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class FullScreen extends Vue {}
</script>

<style></style>
