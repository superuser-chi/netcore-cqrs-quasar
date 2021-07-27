<template>
  <div>
    <p>
      Current count (Vuex):
      <strong>{{ currentCount }}</strong>
    </p>
    <q-btn
      color="primary"
      icon="add"
      @click="increment"
      label="Increment"
      class="q-mr-md"
    />
    <q-btn color="red" icon="undo" @click="reset" label="Reset" />
  </div>
</template>

<script lang="ts">
import { Action, Getter } from "vuex-class";
import { Component, Vue } from "vue-property-decorator";
const namespace = "counter";

@Component
export default class Counter extends Vue {
  @Getter("currentCount", { namespace })
  private currentCount!: number;
  @Action("increment", { namespace })
  private incrementCounter: any;
  @Action("reset", { namespace })
  private resetCounter: any;

  private increment() {
    this.incrementCounter();
  }
  private reset() {
    this.resetCounter();
  }
}
</script>
