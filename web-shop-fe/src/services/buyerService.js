import API from "../api/api";
import OrderModel from "../models/orderModel";
import ProductModel from "../models/productModel";

const getProducts = async () => {
  try {
    const response = await API.get("buyer/products");
    return response.data.map((p) => new ProductModel(p));
  } catch (error) {
    console.log(error);
  }
};

const getOrders = async () => {
  try {
    const response = await API.get("buyer/orders");
    return response.data.map((o) => new OrderModel(o));
  } catch (error) {
    console.log(error);
  }
};

const createOrder = async (createOrderValues) => {
  await API.post("buyer/create-order", createOrderValues);
};

const declineOrder = async (id) => {
  await API.post("buyer/decline-order/" + id);
};

const getTotalPrice = async (listItems) => {
  try {    
    const response = await API.post("buyer/price", listItems);
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

// eslint-disable-next-line
export default {
  getProducts,
  getOrders,
  createOrder,
  declineOrder,
  getTotalPrice
};
