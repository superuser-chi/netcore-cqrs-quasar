"use strict";

import axios, { AxiosRequestConfig } from "axios";

// Full config:  https://github.com/axios/axios#request-config
// axios.defaults.baseURL = process.env.baseURL || process.env.apiUrl || '';
// axios.defaults.headers.common['Authorization'] = AUTH_TOKEN;
// axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

const config = {
  // baseURL: process.env.baseURL || process.env.apiUrl || ""
  // timeout: 60 * 1000, // Timeout
  // withCredentials: true, // Check cross-site Access-Control
};

// tslint:disable-next-line: variable-name
const _axios = axios.create(config);

_axios.interceptors.request.use(
  // Do something before request is sent
  (conf: AxiosRequestConfig) => conf,
  // Do something with request error
  (error: any) => Promise.reject(error)
);

// Add a response interceptor
_axios.interceptors.response.use(
  // Do something with response data
  (response: any) => response,
  // Do something with response error
  (error: any) => Promise.reject(error)
);

export default _axios;
