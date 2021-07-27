import { ApplicationUser } from "@/models/user";
import { MutationTree } from "vuex";
import { AppState } from "./types";

export const mutations: MutationTree<AppState> = {
  toggleLeftDrawer(state) {
    state.leftDrawer = !state.leftDrawer;
  },
  toggleSticky(state) {
    state.sticky = !state.sticky;
  },
  login(state, user: ApplicationUser) {
    state.user = user;
  },
};
