import { Notify } from "quasar";

export default class Notifier {
    public static showError(message: string){
        Notify.create({
            type: "negative",
            message
        });
    }
    public static showPositive(message: string){
        Notify.create({
            type: "positive",
            message
        });
    }
    public static showInfo(message: string){
        Notify.create({
            type: "info",
            message
        });
    }
    public static showWarning(message: string){
        Notify.create({
            type: "warning",
            message
        });
    }
}