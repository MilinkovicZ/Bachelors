import API from "../api/api.js";
import OrderModel from "../models/orderModel.js";
import UserModel from "../models/userModel.js";

const getVerified = async () => {
  try {
    const response = await API.get("admin/verified-users");
    return (response.data.map(u => new UserModel(u)));
  } catch (error) {
    console.log(error);
  }
};

const getUnverified = async () => {
  try {
    const response = await API.get("admin/unverified-users");
    return (response.data.map(u => new UserModel(u)));
  } catch (error) {
    console.log(error);
  }
};

const getDeclined = async () => {
  try {
    const response = await API.get("admin/declined-users");
    return (response.data.map(u => new UserModel(u)));
  } catch (error) {
    console.log(error);
  }
};

const getBuyers = async () => {
  try {
    const response = await API.get("admin/buyers");
    return (response.data.map(u => new UserModel(u)));
  } catch (error) {
    console.log(error);
  }
};

const getOrders = async () => {
  try {
    const response = await API.get("admin/orders");
    return (response.data.map(o => new OrderModel(o)));
  } catch (error) {
    console.log(error);
  }
};

const verifyUser = async (verifyUserValues) => {
  await API.post("admin/verify-user", verifyUserValues);
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