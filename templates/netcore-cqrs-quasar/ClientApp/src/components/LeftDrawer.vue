<template>
  <q-drawer
    v-model="drawer"
    show-if-above
    bordered
    content-class="bg-grey-2"
    :width="leftSticky ? 60 : 240"
  >
    <q-scroll-area class="fit">
      <q-list padding>
        <q-item-label header>Quick Links</q-item-label>

        <q-item
          v-for="link in links1"
          :key="link.text"
          v-ripple
          clickable
          @click.native="$router.push(link.url)"
        >
          <q-item-section avatar>
            <q-icon color="grey" :name="link.icon" />
          </q-item-section>
          <q-item-section>
            <q-item-label>{{ link.text }}</q-item-label>
          </q-item-section>
        </q-item>

        <q-separator class="q-my-md" />
        <q-item-label header>User Management</q-item-label>

        <q-item
          v-for="link in links2"
          :key="link.text"
          v-ripple
          clickable
          @click.native="$router.push(link.url)"
        >
          <q-item-section avatar>
            <q-icon color="grey" :name="link.icon" />
          </q-item-section>
          <q-item-section>
            <q-item-label>{{ link.text }}</q-item-label>
          </q-item-section>
        </q-item>

        <q-separator class="q-mt-md q-mb-xs" />
        <q-item-label header>Extra</q-item-label>

        <q-item
          v-for="link in links3"
          :key="link.text"
          v-ripple
          clickable
          @click.native="$router.push(link.url)"
        >
          <q-item-section avatar>
            <q-icon color="grey" :name="link.icon" />
          </q-item-section>
          <q-item-section>
            <q-item-label>{{ link.text }}</q-item-label>
          </q-item-section>
        </q-item>
      </q-list>
    </q-scroll-area>
  </q-drawer>
</template>

<script lang="ts">
import { Action, Getter } from "vuex-class";
import { Component, Vue } from "vue-property-decorator";

const namespace = "app";

@Component
export default class LeftDrawer extends Vue {
  @Getter("getLeftDrawer", { namespace })
  private leftDrawer!: boolean;
  @Action("toggleLeftDrawer", { namespace })
  private toggleLeftDrawer: any;

  @Getter("getSticky", { namespace })
  private leftSticky!: boolean;

  get drawer() {
    return this.leftDrawer;
  }
  set drawer(value: boolean) {
    this.toggleLeftDrawer();
  }
  buttons1 = [
    { text: "About" },
    { text: "Press" },
    { text: "Copyright" },
    { text: "Contact us" },
    { text: "Creators" },
    { text: "Advertise" },
    { text: "Developers" },
  ];
  buttons2 = [
    { text: "Terms" },
    { text: "Privacy" },
    { text: "Policy & Safety" },
    { text: "Test new features" },
  ];
  links1 = [
    {
      icon: "format_list_bulleted",
      text: "Weatherforecast",
      url: "/fetchdata",
    },
    { icon: "add_circle", text: "Counter", url: "/counter" },
    { icon: "upload_file", text: "Load from excel" },
  ];
  links2 = [
    { icon: "person", text: "Profile", url: "/profile-user" },
    { icon: "supervisor_account", text: "Manage users", url: "/list-user" },
  ];
  links3 = [
    { icon: "settings", text: "Settings" },
    { icon: "support", text: "Support" },
    { icon: "feedback", text: "Feedback" },
  ];
  links4 = [];
}
</script>
