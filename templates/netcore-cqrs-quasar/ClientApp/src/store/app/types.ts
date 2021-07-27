import { ApplicationUser } from "@/models/user";

export interface AppState {
  appName: string;
  leftDrawer: boolean;
  sticky: boolean;
  user: ApplicationUser | null;
}
