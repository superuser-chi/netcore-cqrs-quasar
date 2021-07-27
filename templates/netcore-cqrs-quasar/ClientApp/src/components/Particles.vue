<template>
  <div class="particles">
    <div id="particles-js"></div>
  </div>
</template>

<script>
import "particles.js";
import { ParticleConfig } from "@/helpers/particle_config";
export default {
  name: "particles",
  mounted() {
    this.initParticles();
  },
  methods: {
    initParticles() {
      window.particlesJS("particles-js", ParticleConfig);
    },
  },
};
</script>

<style scoped>
#particles-js {
  background: #232741;
  min-height: 100vh;
  width: 100%;
}
</style>
