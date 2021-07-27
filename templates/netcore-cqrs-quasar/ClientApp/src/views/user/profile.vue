<template>
  <div class="profile row">
    <div
      :class="
        $q.screen.gt.sm
          ? 'q-pt-lg q-px-sm column col-md-1'
          : 'fit q-pt-lg q-px-sm  col-md-12'
      "
    >
      <q-btn
        round
        flat
        color="grey-8"
        stack
        no-caps
        size="26px"
        class="GPL__side-btn"
        @click="setGeneral"
      >
        <q-icon size="22px" name="fas fa-user-edit" />
        <div class="GPL__side-btn__label">General</div>
      </q-btn>

      <q-btn
        round
        flat
        color="grey-8"
        stack
        no-caps
        size="26px"
        class="GPL__side-btn"
        @click="setEmail"
      >
        <q-icon size="22px" name="email" />
        <div class="GPL__side-btn__label">Email</div>
      </q-btn>

      <q-btn
        round
        flat
        color="grey-8"
        stack
        no-caps
        size="26px"
        class="GPL__side-btn"
        @click="setPassword"
      >
        <q-icon size="22px" name="lock" />
        <div class="GPL__side-btn__label">Password</div>
      </q-btn>
    </div>
    <div class="q-pa-md col-12 col-md-11">
      <q-card :class="$q.screen.gt.sm ? 'q-pa-lg' : ''">
        <q-card-section class="q-pa-lg">
          <general :data="profileData" v-show="showGeneral" />
          <email :data="profileData" v-show="showEmail" />
          <password :data="profileData" v-show="showPassword" />
        </q-card-section>
      </q-card>
    </div>
  </div>
</template>
<script lang="ts">
import Vue from "vue";
import { Component } from "vue-property-decorator";
import General from "@/components/profile/General.vue";
import Email from "@/components/profile/Email.vue";
import Password from "@/components/profile/Password.vue";

enum profileTabs {
  general,
  email,
  password,
}

@Component({
  components: { General, Email, Password },
})
export default class Profile extends Vue {
  profileData = {
    firstname: "",
    surname: "",
    cardNumber: "",
    role: "",
    email: "",
    password: "",
  };
  tab = profileTabs.general;
  get showGeneral(): boolean {
    return this.tab === profileTabs.general;
  }
  setGeneral(): void {
    this.tab = profileTabs.general;
  }
  get showEmail(): boolean {
    return this.tab === profileTabs.email;
  }
  setEmail(): void {
    this.tab = profileTabs.email;
  }
  get showPassword(): boolean {
    return this.tab === profileTabs.password;
  }
  setPassword(): void {
    this.tab = profileTabs.password;
  }
  beforeMount() {
    this.setGeneral();
  }
}
</script>
<style scoped lang="sass">
.GPL
  &__side-btn
    &__label
      font-size: 12px
      line-height: 24px
      letter-spacing: .01785714em
      font-weight: 500
</style>
