<template>
  <div class="row q-pt-lg login">
    <particles class="fit" />
    <q-card class="fixed-center col-10 col-md-6">
      <q-card-section class="q-pt-none">
        <div class="q-pa-md">
          <div class="q-pa-md row justify-center">
            <q-avatar rounded size="100px" @click.native="$router.push('/')">
              <img alt="Quasar logo" src="@/assets/logo.png" />
            </q-avatar>
          </div>
          <q-form @submit="onSubmit" @reset="onReset" class="q-gutter-md">
            <h6 class="text-h6 text-center">Sign Up</h6>
            <div class="row items-start q-pl-md q-col-gutter-x-md">
              <q-input
                filled
                class="col-12 col-md-12"
                v-model="name"
                label="Full Name"
                lazy-rules
                :rules="[(val) => val && val.length > 0]"
              />
            </div>
            <div class="row items-start q-pl-md q-col-gutter-x-md">
              <q-input
                filled
                class="col-12 col-md-6"
                v-model="email"
                label="Email"
                type="email"
                lazy-rules
                :rules="[isValidEmail]"
              />
              <q-input
                filled
                class="col-12 col-md-6"
                outlined
                bottom-slots
                v-model="phone"
                label="Phone number"
                counter
                lazy-rules
                :rules="[
                  (val) => (val && val.length > 0) || 'Please type something',
                ]"
              />
            </div>

            <q-input
              filled
              v-model="password"
              label="Password"
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
import Notifier from "@/helpers/notifier";
import Loader from "@/helpers/loader";
import { namespace } from "vuex-class";
import { ApplicationUser } from "@/models/user";
const app = namespace("app");
@Component({ components: { Particles } })
export default class Login extends Vue {
  @app.Action
  login: any;

  name = "";
  phone = "";
  password = "";
  isPwd = true;
  confirmPassword = "";
  email = "";
  passwordInputType = "Password";
  logo = `@assets/logo.png`;
  onSubmit() {
    // implement register
    Loader.showLoader();
    postToApi(ApiEndPoints.register, {
      fullName: this.name,
      phoneNumber: this.phone,
      userName: this.email,
      email: this.email,
      password: this.password,
      role: "User",
    })
      .then((user) => {
        Notifier.showPositive(`You have succesfully registered`);
        this.login(user);
        this.$router.push("/");
      })
      .catch(() => Notifier.showError("Error occured during registration"))
      .finally(() => {
        Loader.hideLoader();
      });
  }
  isValidEmail(val: string) {
    const emailPattern = /^(?=[a-zA-Z0-9@._%+-]{6,254}$)[a-zA-Z0-9._%+-]{1,64}@(?:[a-zA-Z0-9-]{1,63}\.){1,8}[a-zA-Z]{2,63}$/;
    return emailPattern.test(val) || "Invalid email";
  }
  onReset() {
    this.name = "";
    this.email = "";
    this.password = "";
    this.confirmPassword = "";
  }
  mounted() {
    this.$store.dispatch("app/logout");
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
