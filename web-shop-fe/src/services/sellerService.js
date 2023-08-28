import API from "../api/api.js";
import OrderModel from "../models/orderModel.js";
import ProductModel from "../models/productModel.js";

const getProduct = async (id) => {
  try {
    const response = await API.get("seller/products/" + id);
    return new ProductModel(response.data);
  } catch (error) {
    console.log(error);
  }
};

const getProducts = async () => {
  try {
    const response = await API.get("seller/products");
    return (response.data.map(p => new ProductModel(p)));
  } catch (error) {
    console.log(error);
  }
};

const createProduct = async (createProductValues) => {
  await API.post("seller/products", createProductValues, {headers: {"content-type": "multipart/form-data"}});
};

const updateProduct = async (id, updateProductValues) => {
  await API.put("seller/products/" + id, updateProductValues, {headers: {"content-type": "multipart/form-data"}});
};

const deleteProduct = async (id) => {
  await API.delete("seller/products/" + id);
};

const getOrders = async () => {
  try {
    const response = await API.get("seller/orders");
    return (response.data.map(o => new OrderModel(o)));;
  } catch (error) {
    console.log(error);
  }
}

const getNewOrders = async () => {
  try {
    const response = await API.get("seller/new-orders");
    return (response.data.map(o => new OrderModel(o)));;
  } catch (error) {
    console.log(error);
  }
}

const acceptOrder = async (id) => {
  await API.post("seller/accept-order/" + id);
};

// eslint-disable-next-line
export default {
  getProduct,
  getProducts,
  createProduct,
  updateProduct,
  deleteProduct,
  getOrders,
  getNewOrders,
  acceptOrder
};