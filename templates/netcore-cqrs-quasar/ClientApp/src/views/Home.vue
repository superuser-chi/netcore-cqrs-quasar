<template>
  <div class="home">
    <div>
      <HelloWorld
        quote="Welcome to Your ASP.NET Core + Vue.js + TypeScript App"
        author="brought to you by superuser-chi"
      />
    </div>
    <h1 class="headline">Hello, world!</h1>
    <p>Welcome to your new single-page application, built with:</p>
    <ul>
      <li>
        <a href="https://get.asp.net" target="_blank"> ASP.NET Core</a> and
        <a
          href="https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx"
          target="_blank"
          >C#</a
        >
        for cross-platform server-side API code
      </li>
      <li>
        <a href="https://vuejs.org" target="_blank"> Vue.js</a> for client-side
        code
      </li>
      <li>
        <a href="https://cli.vuejs.org" target="_blank"> Vue CLI</a> for
        building, bundling and adding or removing vue plugins
      </li>
      <li>
        <a href="https://webpack.js.org" target="_blank">Webpack</a> internally
        used by Vue CLI
      </li>
      <li>
        <a href="https://quasar.dev/" target="_blank">Quasar</a> for layout and
        styling
      </li>
    </ul>

    <h3 class="headline mt-5">To help you get started, we've also set up:</h3>
    <ul>
      <li>
        <strong>Client-side navigation</strong>. For example, click
        <em>Counter</em> then <em>Back</em> to return here.
      </li>
      <li>
        <strong>Development server integration</strong>. In development mode,
        the development server from <code>vue-cli-service</code> runs in the
        background automatically, so your client-side resources are dynamically
        built on demand and the page refreshes when you modify any file.
      </li>
      <li>
        <strong>Efficient production builds</strong>. In production mode,
        development-time features are disabled, and the
        <code>webpack</code> build tool produces minified static CSS and
        JavaScript files.
      </li>
    </ul>

    <h3 class="headline mt-5">Integrated Vue plugins:</h3>
    <ul>
      <li>
        <a href="https://vuejs.org" target="_blank"> Vue.js</a> - reactive,
        component-oriented view layer for modern web interfaces.
      </li>
      <li>
        <a href="https://quasar.dev/" target="_blank">Quasar</a> - write code
        once and simultaneously deploy it as a website, a Mobile App and/or an
        Electron App
      </li>
      <li>
        <a
          href="https://github.com/vuejs/vue-class-component#readme"
          target="_blank"
          >Class Component</a
        >
        - ES201X/Typescript class decorator for Vue components.
      </li>
      <li>
        <a
          href="https://github.com/kaorun343/vue-property-decorator#readme"
          target="_blank"
          >Property Decorator</a
        >
        - property decorators for Vue components
      </li>
      <li>
        <a href="https://github.com/vuejs/vue-router#readme" target="_blank"
          >Router</a
        >
        - official router for Vue.js 2
      </li>
      <li>
        <a href="https://github.com/vuejs/vuex#readme" target="_blank">Vuex</a>
        - state management for Vue.js
      </li>
      <li>
        <a
          href="https://github.com/yyx990803/register-service-worker#readme"
          target="_blank"
          >Register Service Worker</a
        >
        - script for registering service worker with hooks for common events to
        simplify PWA development.
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import HelloWorld from "@/components/HelloWorld.vue"; // @ is an alias to /src

@Component({
  components: {
    HelloWorld,
  },
})
export default class Home extends Vue {}
</script>
