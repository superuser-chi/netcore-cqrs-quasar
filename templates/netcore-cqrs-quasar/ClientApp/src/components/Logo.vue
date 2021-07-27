<template>
  <div class="q-pr-lg" @click="$router.push('/dashboard')">
    <img :src="logo" :style="style" />
  </div>
</template>
<script lang="ts">
import Vue from "vue";
import { Component, Prop } from "vue-property-decorator";
@Component
export default class Logo extends Vue {
  @Prop({ required: false, default: "11em" })
  width!: string;
  @Prop({ required: false, default: "4em" })
  height!: string;

  logo = `/images/logo.png`;

  get style(): string {
    return `width:${this.width}; height:${this.height}`;
  }
}
</script>
