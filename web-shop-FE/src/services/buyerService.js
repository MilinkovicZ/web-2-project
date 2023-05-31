import API from "../api/api";

const getProducts = async () => {
  try {
    const response = await API.get("Buyer/GetProducts");
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

const getOrders = async () => {
  try {
    const response = await API.get("Buyer/GetOrders");
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

const createOrder = async (createOrderValues) => {
  await API.post("Buyer/CreateOrder", createOrderValues);
}

const declineOrder = async (id) => {
  await API.post("Buyer/DeclineOrder/" + id);
}

// eslint-disable-next-line
export default {
  getProducts,
  getOrders,
  createOrder,
  declineOrder
}