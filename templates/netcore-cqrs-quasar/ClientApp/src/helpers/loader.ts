import { Loading, QSpinnerGears } from "quasar";

export default class Loader {
    public static showLoader() {
        Loading.show({
            message: 'Working...'
        })
    }
    public static hideLoader() {
        Loading.hide()
    }
}