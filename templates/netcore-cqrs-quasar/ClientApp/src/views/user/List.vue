<template>
  <div>
    <div class="fit">
      <q-table
        title="Mange Users"
        :filter="filter"
        :data="users"
        :columns="columns"
        row-key="id"
      >
        <template v-slot:top-right>
          <q-input
            borderless
            dense
            debounce="300"
            v-model="filter"
            placeholder="Search"
          >
            <template v-slot:append>
              <q-icon name="search" />
            </template>
          </q-input>
        </template>
        <template v-slot:body="props">
          <q-tr>
            <q-td key="userName" :props="props">{{ props.row.userName }}</q-td>
            <q-td key="email" :props="props">{{ props.row.email }}</q-td>
            <q-td key="lockOutEnabled" :props="props">
              <q-toggle
                v-model="props.row.lockoutEnabled"
                checked-icon="check"
                color="green"
                unchecked-icon="clear"
              />
            </q-td>
            <q-td key="lockoutEnd" :props="props"
              >{{ props.row.lockoutEnd === null ? "No" : "Yes" }}
            </q-td>

            <q-td key="roles" :props="props">
              {{ props.row.roles }}
              <q-popup-edit
                v-model="props.row.roles"
                content-class="bg-purple text-white"
                :validate="(val) => val.length > 0"
                @save="
                  (val, initialValue) =>
                    updateProfile(val, initialValue, props.row, 'roles')
                "
              >
                <template
                  v-slot="{
                    initialValue,
                    value,
                    validate,
                    set,
                  }"
                >
                  <q-select
                    dark
                    v-model="props.row.roles"
                    dense
                    autofocus
                    counter
                    :options="roles"
                  >
                    <template v-slot:prepend>
                      <q-icon name="edit" color="accent" />
                    </template>
                    <template v-slot:after>
                      <q-btn
                        flat
                        dense
                        color="positive"
                        icon="check_circle"
                        @click="updateProfile(props.row)"
                        @click.stop="set"
                        :disable="
                          validate(value) === false || initialValue === value
                        "
                      />
                    </template>
                  </q-select>
                </template>
              </q-popup-edit>
            </q-td>
            <q-td
              key="actions"
              :props="props"
              class="text-right q-col-gutter-x-md"
            >
              <q-icon
                size="22px"
                :name="lockedIcon(props.row)"
                color="blue"
                @click="blockUser(props.row)"
              />
              <q-icon
                size="22px"
                name="delete"
                color="red"
                @click="deleteUser(props.row.id)"
              />
            </q-td>
          </q-tr>
        </template>
      </q-table>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { deleteFromApi, getFromApi, updateToApi } from "@/helpers/network_util";
import { ApplicationUser } from "@/models/user";
import { ApiEndPoints } from "@/helpers/api_endpoints";
import Notifier from "@/helpers/notifier";
import Loader from "@/helpers/loader";

@Component
export default class UsersView extends Vue {
  private users: ApplicationUser[] = [];
  columns = [
    {
      name: "userName",
      required: true,
      label: "UserName",
      align: "left",
      field: (row: ApplicationUser) => row.userName,
      sortable: true,
      classes: "bg-grey-2 ellipsis",
      style: "max-width: 100px",
      headerClasses: "bg-primary text-white",
    },
    {
      name: "email",
      required: true,
      label: "Email",
      field: (row: ApplicationUser) => row.email,
      format: (val: string) => `${val}`,
      sortable: true,
    },
    {
      name: "lockOutEnabled",
      required: true,
      label: "Lock Out Enabled?",
      field: (row: ApplicationUser) => row.lockoutEnd,
    },
    {
      name: "lockoutEnd",
      required: true,
      label: "Is Locked?",
      field: (row: ApplicationUser) => row.lockoutEnd,
    },
    {
      name: "roles",
      required: true,
      label: "Roles",
      field: (row: ApplicationUser) => row.role,
      sortable: true,
    },
    {
      name: "actions",
      required: true,
      label: "Actions",
      align: "right",
      field: "Actions",
    },
  ];
  filter = "";
  roles = ["Admin", "User"];

  lockedIcon(row: ApplicationUser): string {
    return row.lockoutEnd === null ? "lock" : "lock_open";
  }
  async fetchUsers() {
    this.users = await getFromApi<ApplicationUser[]>(ApiEndPoints.users);
  }
  async mounted() {
    await this.fetchUsers();
  }
  updateProfile(user: ApplicationUser) {
    updateToApi<ApplicationUser>(ApiEndPoints.users, user.id, user)
      .then((user) => {
        const index = this.users.findIndex((i) => user.id === i.id);
        this.users[index] = user;
        Loader.hideLoader();
        Notifier.showPositive(`You have succesfully updated your profile`);
      })
      .catch(() => [
        Loader.hideLoader(),
        Notifier.showError("There was an error updating profile"),
      ]);
  }
  blockUser(user: ApplicationUser) {
    updateToApi<any>(
      `${ApiEndPoints.users}/block-user`,
      user.id,
      user.lockoutEnd === null
    ).then(async () => {
      Notifier.showInfo("User has been blocked successfully");
      await this.fetchUsers();
    });
  }
  deleteUser(id: string) {
    deleteFromApi<ApplicationUser>(ApiEndPoints.users, id).then((d) => {
      const index = this.users.findIndex((i) => i.id == d.id);
      this.users.splice(index, 1);
      Notifier.showInfo(`${d.userName} was successfully deleted`);
    });
  }
}
</script>
