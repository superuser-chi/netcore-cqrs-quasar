<template>
  <q-header elevated class="bg-white text-grey-8 q-py-xs" height-hint="58">
    <q-toolbar>
      <q-btn
        flat
        dense
        round
        @click="drawer = !drawer"
        aria-label="Menu"
        icon="menu"
      />

      <!-- <q-btn flat no-caps no-wrap class="q-ml-xs" v-if="$q.screen.gt.xs">
        <q-toolbar-title shrink class="text-weight-bold">
          {{ appName }}
        </q-toolbar-title>
      </q-btn> -->

      <q-space />

      <div class="YL__toolbar-input-container row no-wrap justify-center">
        <logo />
        <!-- <q-input
          dense
          outlined
          square
          v-model="search"
          placeholder="Search"
          class="bg-white col"
        />
        <q-btn
          class="YL__toolbar-input-btn"
          color="grey-3"
          text-color="grey-8"
          icon="search"
          unelevated
        /> -->
      </div>

      <q-space />

      <div class="q-gutter-sm row items-center no-wrap">
        <q-btn dense flat no-wrap round class="q-pa-sm">
          <q-avatar
            size="26px"
            v-if="!authenticated"
            @click="$router.push('/login')"
          >
            <q-icon name="fas fa-sign-in-alt" />
          </q-avatar>
          <div v-if="authenticated">
            <q-icon name="arrow_drop_down" size="16px" />
            <q-avatar color="primary" class="text-white text-caption">
              {{ usernameFirstLetter }}
            </q-avatar>
            <q-menu auto-close>
              <q-list dense>
                <q-item class="GL__menu-link-signed-in">
                  <q-item-section>
                    <div>
                      Signed in as
                      <strong>{{ userName }}</strong>
                    </div>
                  </q-item-section>
                </q-item>
                <q-separator />
                <q-item clickable class="GL__menu-link">
                  <q-item-section @click="$router.push('/profile-user')"
                    >Your profile</q-item-section
                  >
                </q-item>
                <q-separator />
                <q-item clickable class="GL__menu-link">
                  <q-item-section @click="goToLogin">Sign out</q-item-section>
                </q-item>
              </q-list>
            </q-menu>
          </div>
        </q-btn>
      </div>
    </q-toolbar>
  </q-header>
</template>
<script lang="ts">
import { ApplicationUser } from "@/models/user";
import Vue from "vue";
import { Component } from "vue-property-decorator";
import { Action, Getter } from "vuex-class";
import Logo from "@/components/Logo.vue";

const namespace = "app";
@Component({ components: { Logo } })
export default class ToolBar extends Vue {
  @Getter("getAppName", { namespace })
  private appName!: string;
  @Getter("authenticated", { namespace })
  private authenticated!: boolean;
  @Getter("getLeftDrawer", { namespace })
  private leftDrawer!: boolean;
  @Action("logout", { namespace })
  private logout: any;
  @Action("toggleLeftDrawer", { namespace })
  private toggleLeftDrawer: any;
  @Getter("user", { namespace })
  private user!: ApplicationUser;
  @Getter("getSticky", { namespace })
  private leftSticky!: boolean;
  @Action("toggleSticky", { namespace })
  private toggleLeftSticky: any;

  search = "";
  get drawer() {
    return this.leftDrawer;
  }
  set drawer(value: boolean) {
    this.toggleLeftDrawer();
  }
  get sticky() {
    return this.leftSticky;
  }
  set sticky(value: boolean) {
    this.toggleLeftSticky();
  }
  get StickIcon() {
    return this.sticky ? "chevron_left" : "chevron_right";
  }
  get usernameFirstLetter() {
    return this.userName.charAt(0);
  }
  get userName() {
    if (this.user === null || this.user == undefined) {
      return "Please login";
    }
    return this.user.userName;
  }
  goToLogin() {
    this.logout();
    this.$router.push("/");
  }
}
</script>

<style lang="sass">
.GPLAY
  &__toolbar-input-container
      min-width: 100px
      width: 17%
</style>
