import API from "../api/api.js";

const getVerified = async () => {
  try {
    const token = localStorage.getItem("token");
    const config = {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    };

    const response = await API.get("Admin/GetVerified", config);
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

const getUnverified = async () => {
  try {
    const token = localStorage.getItem("token");
    const config = {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    };

    const response = await API.get("Admin/GetUnverified", config);
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

const getOrders = async () => {
  try {
    const token = localStorage.getItem("token");
    const config = {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    };

    const response = await API.get("Admin/GetOrders", config);
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

const verifyUser = async (verifyUserValues) => {
  const token = localStorage.getItem("token");
  const config = {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  };

  await API.post("Admin/VerifyUser", verifyUserValues, config);
};

// eslint-disable-next-line
export default {
  getVerified,
  getUnverified,
  getOrders,
  verifyUser,
};