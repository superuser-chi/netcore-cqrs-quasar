<template>
  <div>
    <h3>Weather forecast</h3>
    <p>This component demonstrates fetching data from the server.</p>
    <div class="fit">
      <q-table :data="forecasts" :columns="columns" row-key="name">
        <template v-slot:body="props">
          <q-tr>
            <q-td key="date" :props="props">{{ props.row.date }}</q-td>
            <q-td key="temperatureC" :props="props">
              <q-btn round :color="getColor(props.row.temperatureC)">
                {{ props.row.temperatureC }}
              </q-btn>
            </q-td>
            <q-td key="temperatureF" :props="props">{{
              props.row.temperatureF
            }}</q-td>
            <q-td key="summary" :props="props">{{ props.row.summary }}</q-td>
          </q-tr>
        </template>
      </q-table>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { getFromApi } from "@/helpers/network_util";
import { Forecast } from "@/models/Forecast";
import { ApiEndPoints } from "@/helpers/api_endpoints";
import { format } from "date-fns";
@Component
export default class FetchDataView extends Vue {
  private forecasts: Forecast[] = [];
  columns = [
    {
      name: "date",
      required: true,
      label: "Date (100g serving)",
      align: "left",
      field: (row: Forecast) => row.date,
      format: (val: string) => `${format(Date.parse(val), "dd/MM/yyyy")}`,
      sortable: true,
      classes: "bg-grey-2 ellipsis",
      style: "max-width: 100px",
      headerClasses: "bg-primary text-white",
    },
    {
      name: "temperatureC",
      required: true,
      label: "Temp. (C)",
      field: (row: Forecast) => row.temperatureC,
      format: (val: Forecast) => `${val}`,
      sortable: true,
    },
    {
      name: "temperatureF",
      required: true,
      label: "Temp. (F)",
      field: (row: Forecast) => row.temperatureF,
      format: (val: Forecast) => `${val}`,
      sortable: true,
    },
    {
      name: "summary",
      required: true,
      label: "Summary",
      field: (row: Forecast) => row.summary,
      format: (val: Forecast) => `${val}`,
      sortable: true,
    },
  ];
  async mounted() {
    this.forecasts = await getFromApi<Forecast[]>(ApiEndPoints.weatherforecast);
  }
  getColor(temperature: number) {
    if (temperature < 0) {
      return "red";
    } else if (temperature >= 0 && temperature < 30) {
      return "primary";
    } else {
      return "accent";
    }
  }
}
</script>
