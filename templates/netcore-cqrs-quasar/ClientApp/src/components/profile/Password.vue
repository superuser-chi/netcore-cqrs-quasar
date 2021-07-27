<template>
  <q-form class="q-gutter-md" @submit="onSubmit">
    <div class="row items-start">
      <q-input
        filled
        v-model="password"
        class="col-12 col-md-12"
        label="Password"
        hint="Type your password"
        :type="isPwd ? 'password' : 'text'"
        lazy-rules
        :rules="[(val) => (val !== null && val !== '') || 'Invalid password']"
      >
        <template v-slot:append>
          <q-icon
            :name="isPwd ? 'visibility_off' : 'visibility'"
            class="cursor-pointer"
            @click="isPwd = !isPwd"
          />
        </template>
      </q-input>
    </div>
    <div class="row items-start">
      <q-input
        filled
        label="Confirm Password"
        class="col-12 col-md-12"
        hint="Confirm password"
        v-model="newPassword"
        :type="isPwd ? 'password' : 'text'"
        lazy-rules
        :rules="[(val) => (val !== null && val !== '') || 'Invalid password']"
      >
        <template v-slot:append>
          <q-icon
            :name="isPwd ? 'visibility_off' : 'visibility'"
            class="cursor-pointer"
            @click="isPwd = !isPwd"
          />
        </template>
      </q-input>
    </div>
    <div class="q-gutter-md row items-start">
      <q-btn label="Submit" type="submit" color="blue" icon-right="edit" />
      <q-btn label="Reset" type="reset" color="primary" flat class="q-ml-sm" />
    </div>
  </q-form>
</template>
<script lang="ts">
import { ApiEndPoints } from "@/helpers/api_endpoints";
import Loader from "@/helpers/loader";
import { updateToApi } from "@/helpers/network_util";
import Notifier from "@/helpers/notifier";
import { ApplicationUser } from "@/models/user";
import Vue from "vue";
import { Component } from "vue-property-decorator";
import { namespace } from "vuex-class";
const app = namespace("app");

@Component
export default class Password extends Vue {
  @app.Getter
  user!: ApplicationUser;

  @app.Action
  updateUser: any;

  newPassword = "";

  password = "";

  isPwd = "";

  public onSubmit() {
    updateToApi<ApplicationUser>(ApiEndPoints.users, this.user.id, this.user)
      .then((user) => {
        Loader.hideLoader();
        Notifier.showPositive(`You have succesfully updated your profile`);
        this.updateUser(user);
      })
      .catch(() => [
        Loader.hideLoader(),
        Notifier.showError("There was an error updating profile"),
      ]);
  }
}
</script>
