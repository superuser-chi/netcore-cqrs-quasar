import { Module } from "vuex";
import { getters } from "./getters";
import { actions } from "./actions";
import { mutations } from "./mutations";
import { AppState } from "./types";
import { RootState } from "../types";

export const state: AppState = {
  appName: "netcore-cqrs-quasar",
  leftDrawer: true,
  sticky: false,
  user: null,
};

const namespaced = true;

export const app: Module<AppState, RootState> = {
  namespaced,
  state,
  getters,
  actions,
  mutations,
};
