<template>
  <q-form class="q-gutter-md" @submit="onSubmit">
    <div class="row items-start">
      <q-input
        class="col-12 col-md-12"
        outlined
        bottom-slots
        v-model="user.email"
        label="current Email"
        counter
        readonly
        lazy-rules
        :rules="[(val) => (val && val.length > 0) || 'Please type something']"
      >
        <template v-slot:prepend>
          <q-icon name="email" />
        </template>
        <template v-slot:append>
          <q-icon name="close" @click="text = ''" class="cursor-pointer" />
        </template>

        <template v-slot:hint>
          Type your current email
        </template>
      </q-input>
    </div>
    <div class="row items-start">
      <q-input
        class="col-12 col-md-12"
        outlined
        bottom-slots
        v-model="newEmail"
        label="New Email"
        counter
        lazy-rules
        :rules="
          [(val) => (val && val.length > 0) || 'Please type something'] ||
            isValidEmail
        "
      >
        <template v-slot:prepend>
          <q-icon name="email" />
        </template>
        <template v-slot:append>
          <q-icon name="close" @click="text = ''" class="cursor-pointer" />
        </template>
        <template v-slot:hint>
          Type your new email
        </template>
      </q-input>
    </div>
    <div class="q-gutter-md row items-start">
      <q-btn
        label="Change email"
        type="submit"
        color="blue"
        icon-right="edit"
      />
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
import { Action, Getter } from "vuex-class";
const namespace = "app";

@Component
export default class Email extends Vue {
  @Action("updateUser", { namespace })
  updateUser: any;

  @Getter("user", { namespace })
  user!: ApplicationUser;

  private newEmail = "";

  isValidEmail(val: string) {
    const emailPattern = /^(?=[a-zA-Z0-9@._%+-]{6,254}$)[a-zA-Z0-9._%+-]{1,64}@(?:[a-zA-Z0-9-]{1,63}\.){1,8}[a-zA-Z]{2,63}$/;
    return emailPattern.test(val) || "Invalid email";
  }

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
