<template>
  <div class="row q-pt-lg login">
    <particles class="fit" />
    <q-card class="fixed-center col-10 col-md-6">
      <q-card-section class="q-pt-none">
        <div class="q-pa-md">
          <div class="q-pa-md row justify-center">
            <logo />
          </div>
          <q-form @submit="onSubmit" @reset="onReset" class="q-gutter-md">
            <h6 class="text-h6 text-center">Sign In</h6>
            <q-input
              filled
              v-model="name"
              label="Username"
              hint="Type your username"
              lazy-rules
              :rules="[
                (val) => (val && val.length > 0) || 'Please type username',
              ]"
            />

            <q-input
              filled
              v-model="password"
              label="Password"
              hint="Type your password"
              :type="isPwd ? 'password' : 'text'"
              lazy-rules
              :rules="[
                (val) => (val !== null && val !== '') || 'Invalid password',
              ]"
            >
              <template v-slot:append>
                <q-icon
                  :name="isPwd ? 'visibility_off' : 'visibility'"
                  class="cursor-pointer"
                  @click="isPwd = !isPwd"
                />
              </template>
            </q-input>

            <q-toggle
              v-model="rememberMe"
              checked-icon="check"
              color="green"
              class="text-grey-10"
              label="Remember me?"
              unchecked-icon="clear"
            />

            <div>
              <q-btn
                size="md"
                class="fit"
                color="secondary"
                label="Submit"
                type="submit"
                icon-right="fas fa-sign-in-alt"
              />
            </div>
          </q-form>
          <div class="q-mt-md">
            <a class="text-primary q-mt-xl" @click="$router.push('/register')"
              >Don't have an account?
            </a>
          </div>
        </div>
      </q-card-section>
    </q-card>
  </div>
</template>
<script lang="ts">
import Vue from "vue";
import { Component } from "vue-property-decorator";
import Particles from "@/components/Particles.vue";
import { postToApi } from "@/helpers/network_util";
import { ApiEndPoints } from "@/helpers/api_endpoints";
import { namespace } from "vuex-class";
import Notifier from "@/helpers/notifier";
import Logo from "@/components/Logo.vue";

const app = namespace("app");

@Component({ components: { Logo, Particles } })
export default class Login extends Vue {
  @app.Action
  login: any;

  rememberMe = false;
  isPwd = true;
  name = "";
  password = "";
  passwordInputType = "Password";
  logo = `@assets/logo.png`;
  onSubmit() {
    // implement login
    postToApi<any>(ApiEndPoints.login, {
      userName: this.name,
      password: this.password,
    })
      .then((user) => {
        Notifier.showPositive("You have succesfully logged in");
        this.login(user);
        this.$router.push("/");
      })
      .catch(() => [Notifier.showError("bad username/password combination")]);
  }
  onReset() {
    this.name = "";
    this.password = "";
    this.rememberMe = false;
  }
}
</script>
<style lang="sass">
.login
  background-color: #232741 !important
.GPL
  @media (min-width: 1024px)
    &__page-container
      padding-left: 0px
</style>
