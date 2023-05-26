import axios from "axios";

const API = axios.create({
  baseURL: process.env.REACT_APP_ENDPOINT,
  headers: {
    "Content-Type": "application/json",
  },
});

export default API;