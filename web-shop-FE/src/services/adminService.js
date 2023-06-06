import API from "../api/api.js";
import OrderModel from "../models/orderModel.js";
import UserModel from "../models/userModel.js";

const getVerified = async () => {
  try {
    const response = await API.get("Admin/GetVerified");
    return (response.data.map(u => new UserModel(u)));
  } catch (error) {
    console.log(error);
  }
};

const getUnverified = async () => {
  try {
    const response = await API.get("Admin/GetUnverified");
    return (response.data.map(u => new UserModel(u)));
  } catch (error) {
    console.log(error);
  }
};

const getDeclined = async () => {
  try {
    const response = await API.get("Admin/GetDeclined");
    return (response.data.map(u => new UserModel(u)));
  } catch (error) {
    console.log(error);
  }
};

const getBuyers = async () => {
  try {
    const response = await API.get("Admin/GetBuyers");
    return (response.data.map(u => new UserModel(u)));
  } catch (error) {
    console.log(error);
  }
};

const getOrders = async () => {
  try {
    const response = await API.get("Admin/GetOrders");
    return (response.data.map(o => new OrderModel(o)));
  } catch (error) {
    console.log(error);
  }
};

const verifyUser = async (verifyUserValues) => {
  await API.post("Admin/VerifyUser", verifyUserValues);
};

// eslint-disable-next-line
export default {
  getVerified,
  getUnverified,
  getOrders,
  verifyUser,
  getDeclined,
  getBuyers
};