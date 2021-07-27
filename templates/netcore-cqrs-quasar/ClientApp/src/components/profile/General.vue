<template>
  <q-form class="q-gutter-md" @submit="onSubmit">
    <div class="row items-start q-pl-md q-col-gutter-x-md">
      <q-input
        class="col-12 col-md-6 "
        outlined
        bottom-slots
        v-model="user.userName"
        label="Username"
        counter
        lazy-rules
        :rules="[(val) => (val && val.length > 0) || 'Please type something']"
      >
        <template v-slot:prepend>
          <q-icon name="person" />
        </template>
        <template v-slot:append>
          <q-icon name="close" @click="text = ''" class="cursor-pointer" />
        </template>

        <template v-slot:hint>
          Type your username
        </template>
      </q-input>
      <q-input
        class="col-12 col-md-6"
        outlined
        bottom-slots
        v-model="user.fullName"
        label="Fullname"
        counter
        lazy-rules
        :rules="[(val) => (val && val.length > 0) || 'Please type something']"
      >
        <template v-slot:prepend>
          <q-icon name="person" />
        </template>
        <template v-slot:append>
          <q-icon name="close" @click="text = ''" class="cursor-pointer" />
        </template>

        <template v-slot:hint>
          Type your username
        </template>
      </q-input>
    </div>
    <div class="row items-start q-pl-md q-col-gutter-x-md">
      <q-input
        class="col-12 col-md-6"
        outlined
        bottom-slots
        v-model="user.phoneNumber"
        label="Phone number"
        counter
        lazy-rules
        :rules="[(val) => (val && val.length > 0) || 'Please type something']"
      >
        <template v-slot:prepend>
          <q-icon name="phone" />
        </template>
        <template v-slot:append>
          <q-icon name="close" @click="text = ''" class="cursor-pointer" />
        </template>

        <template v-slot:hint>
          Your role
        </template>
      </q-input>
      <q-input
        class="col-12 col-md-6"
        outlined
        bottom-slots
        v-model="user.role"
        label="Role"
        counter
        readonly
        lazy-rules
        :rules="[(val) => (val && val.length > 0) || 'Please type something']"
      >
        <template v-slot:prepend>
          <q-icon name="person" />
        </template>
        <template v-slot:append>
          <q-icon name="close" @click="text = ''" class="cursor-pointer" />
        </template>

        <template v-slot:hint>
          Your role
        </template>
      </q-input>
    </div>
    <div>
      <q-btn label="Submit" type="submit" color="blue" icon-right="edit" />
      <q-btn label="Reset" type="reset" color="primary" flat class="q-ml-sm" />
    </div>
  </q-form>
</template>
<script lang="ts">
import { ApiEndPoints } from "@/helpers/api_endpoints";
import { updateToApi } from "@/helpers/network_util";
import Notifier from "@/helpers/notifier";
import Loader from "@/helpers/loader";
import { ApplicationUser } from "@/models/user";
import Vue from "vue";
import { Component } from "vue-property-decorator";
import { namespace } from "vuex-class";

const app = namespace("app");

@Component
export default class General extends Vue {
  @app.Action updateUser: any;
  @app.Getter user!: ApplicationUser;

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
