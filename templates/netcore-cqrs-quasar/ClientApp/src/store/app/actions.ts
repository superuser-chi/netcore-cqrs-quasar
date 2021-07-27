import { ActionTree } from "vuex";
import { AppState } from "./types";
import { RootState } from "../types";
import { ApplicationUser } from "@/models/user";

export const actions: ActionTree<AppState, RootState> = {
  toggleLeftDrawer({ commit }): any {
    commit("toggleLeftDrawer");
  },
  toggleSticky({ commit }): any {
    commit("toggleSticky");
  },
  login({ commit }, user: ApplicationUser): any {
    commit("login", user);
  },
};
