import API from "../api/api.js";

const getVerified = async () => {
  try {
    const response = await API.get("Admin/GetVerified");
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

const getUnverified = async () => {
  try {
    const response = await API.get("Admin/GetUnverified");
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

const getDeclined = async () => {
  try {
    const response = await API.get("Admin/GetDeclined");
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

const getBuyers = async () => {
  try {
    const response = await API.get("Admin/GetBuyers");
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

const getOrders = async () => {
  try {
    const response = await API.get("Admin/GetOrders");
    return response.data;
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