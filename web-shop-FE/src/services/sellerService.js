import API from "../api/api.js";
import OrderModel from "../models/orderModel.js";
import ProductModel from "../models/productModel.js";

const getProduct = async (id) => {
  try {
    const response = await API.get("Seller/GetProduct/" + id);
    return new ProductModel(response.data);
  } catch (error) {
    console.log(error);
  }
};

const getProducts = async () => {
  try {
    const response = await API.get("Seller/GetProducts");
    return (response.data.map(p => new ProductModel(p)));
  } catch (error) {
    console.log(error);
  }
};

const createProduct = async (createProductValues) => {
  await API.post("Seller/CreateProduct", createProductValues, {headers: {"content-type": "multipart/form-data"}});
};

const updateProduct = async (id, updateProductValues) => {
  await API.put("Seller/UpdateProduct/" + id, updateProductValues, {headers: {"content-type": "multipart/form-data"}});
};

const deleteProduct = async (id) => {
  await API.delete("Seller/DeleteProduct/" + id);
};

const getOrders = async () => {
  try {
    const response = await API.get("Seller/GetOrders");
    return (response.data.map(o => new OrderModel(o)));;
  } catch (error) {
    console.log(error);
  }
}

const getNewOrders = async () => {
  try {
    const response = await API.get("Seller/GetNewOrders");
    return (response.data.map(o => new OrderModel(o)));;
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