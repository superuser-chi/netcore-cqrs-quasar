import { GetterTree } from "vuex";
import { AppState } from "./types";
import { RootState } from "../types";
import { ApplicationUser } from "@/models/user";

export const getters: GetterTree<AppState, RootState> = {
  getAppName(state): string {
    return state.appName;
  },
  getLeftDrawer(state): boolean {
    return state.leftDrawer;
  },
  getSticky(state): boolean {
    return state.sticky;
  },
  user(state): ApplicationUser | null {
    return state.user;
  },
  authenticated(state): boolean {
    return state.user === null ? false : true;
  },
};
