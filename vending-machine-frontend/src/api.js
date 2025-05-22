import axios from "axios";

const API_BASE = "https://localhost:7285/api/coffee";

export const getCoffeeTypes = async () =>
  (await axios.get(`${API_BASE}/types`)).data;

export const getCustomizations = async () =>
  (await axios.get(`${API_BASE}/customizations`)).data;

export const postOrder = async (payload) =>
  (await axios.post(`${API_BASE}/order`, payload)).data;
