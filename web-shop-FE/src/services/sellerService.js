import API from "../api/api.js";

const getProduct = async (id) => {
  try {
    const token = localStorage.getItem("token");
    const config = {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    };

    const response = await API.get("Seller/GetProduct/" + id, config);
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

const getProducts = async () => {
  try {
    const token = localStorage.getItem("token");
    const config = {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    };

    const response = await API.get("Seller/GetProducts", config);
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

const createProduct = async (createProductValues) => {
  const token = localStorage.getItem("token");
  const config = {
    headers: {
      Authorization: `Bearer ${token}`,
      "content-type": "multipart/form-data",
    },
  };

  await API.post("Seller/CreateProduct", createProductValues, config);
};

const updateProduct = async (id, updateProductValues) => {
  const token = localStorage.getItem("token");
  const config = {
    headers: {
      Authorization: `Bearer ${token}`,
      "content-type": "multipart/form-data",
    },
  };

  await API.put("Seller/UpdateProduct/" + id, updateProductValues, config);
};

const deleteProduct = async (id) => {
  const token = localStorage.getItem("token");
  const config = {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  };

  await API.post("Seller/DeleteProduct/" + id, null, config);
};

const getOrders = async () => {
  try {
    const token = localStorage.getItem("token");
    const config = {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    };

    const response = await API.get("Seller/GetOrders", config);
    return response.data;
  } catch (error) {
    console.log(error);
  }
}

const getNewOrders = async () => {
  try {
    const token = localStorage.getItem("token");
    const config = {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    };

    const response = await API.get("Seller/GetNewOrders", config);
    return response.data;
  } catch (error) {
    console.log(error);
  }
}

// eslint-disable-next-line
export default {
  getProduct,
  getProducts,
  createProduct,
  updateProduct,
  deleteProduct,
  getOrders,
  getNewOrders
};