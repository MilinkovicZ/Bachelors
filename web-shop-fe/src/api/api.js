import axios from "axios";

const API = axios.create({
  baseURL: process.env.REACT_APP_ENDPOINT,
  headers: {
    "Content-Type": "application/json",
  },
});

API.interceptors.request.use((config) => {
  try{
      const token = localStorage.getItem("token");
      if(token){
          return {...config, headers: {
              ...config.headers,
              Authorization: `Bearer ${token}`,
          }};
      }
      return config;
  } catch(e) {
      console.log(e);
      return Promise.reject(e);
  }
});

export default API;