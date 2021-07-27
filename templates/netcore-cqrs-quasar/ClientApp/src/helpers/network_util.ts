import _axios from "@/plugins/axios";
import store from "@/store";

/**
 * Returns T[]
 * @param endpoint the api endpoint
 * @param params the parameters that you wish to pass along with the request
 * @param headers the headers that you wish to pass along with the request 
 */
export async function getFromApi<T>(
  endpoint: string,
  params: any = {},
  headers: any = {}
): Promise<T> {
  try {
    const token = store.getters["app/token"];
    const authHeader = token ? { Authorization: `Bearer ${token}` } : {};
    const response = await _axios.get(`api/${endpoint}`, {
      params: params,
      headers: {
        ...authHeader,
        ...headers
      },
    });
    const item = await response.data;
    return item;
  } catch (error) {
    const message = `An error has occured: ${error.status}`;
    throw new Error(message);
  }
}

/**
 * Returns posted object
 * @param endpoint the api endpoint
 * @param obj the entity to be added to the api
 */
export async function postToApi<T>(
  endpoint: string,
  obj: T,
  headers = {}
): Promise<T> {
  try {
    const token = store.getters["app/token"];
    const response = await _axios.post(`api/${endpoint}`, obj, {
      headers: {
        Authorization: `Bearer ${token}`,
        ...headers
      },
    });
    const item: T = await response.data;
    return item;
  } catch (error) {
    const message = `An error has occured: ${error.status}`;
    throw new Error(message);
  }
}
/**
 * Returns updated object
 * @param endpoint the api endpoint
 * @param id the id of the entity to be updated
 * @param obj the json object to be updated
 */
export async function updateToApi<T>(
  endpoint: string,
  id: string | number,
  param: T,
  headers = {}
): Promise<T> {
  try {
    const token = store.getters["app/token"];
    const response = await _axios.put(`api/${endpoint}/${id}`, param, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    const item: T = await response.data;
    return item;
  } catch (error) {
    const message = `An error has occured: ${error.status}`;
    throw new Error(message);
  }
}
/**
 * Returns deleted object
 * @param endpoint the api endpoint
 * @param id the id of the entity to be deleted
 * @param T the json object to be deleted
 */
export async function deleteFromApi<T>(
  endpoint: string,
  id: string | number,
  headers = {}
): Promise<T> {
  try {
    const token = store.getters["app/token"];
    const response = await _axios.delete(`api/${endpoint}/${id}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    const item: T = await response.data;
    return item;
  } catch (error) {
    const message = `An error has occured: ${error.message}`;
    throw new Error(message);
  }
}
