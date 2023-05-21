import axios from "axios";

const API = axios.create({
  baseURL: import.meta.env.VITE_ENDPOINT,
  headers: {
    "Content-Type": "application/json",
  },
});

export default API;