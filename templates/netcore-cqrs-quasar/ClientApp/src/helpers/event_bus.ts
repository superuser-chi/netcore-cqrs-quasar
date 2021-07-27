import Vue from "vue";

export enum AppEventBusEvents {
  RequestDetalAddPassengerEvent = "request_detal_add_passenger_event",
  filterProjectByDepartmentEvent = "filter_project_by_department_event",
}

export const AppEventBus = new Vue();
