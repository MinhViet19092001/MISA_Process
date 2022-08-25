import axios from "axios";
export const HTTP = axios.create({
  baseURL: `https://localhost:44328/api/v1/`,
});

HTTP.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error && error.response && error.response.status == 401) {
      console.log("expired");
    }
    return Promise.reject(error);
  }
);
