import API from "../api/api.js";
import ProfileModel from "../models/profileModel.js";

const register = async (registerValues) => {
  await API.post("auth/register", registerValues);
};

const getProfile = async () => {
  try {
    const response = await API.get("user");
    return new ProfileModel(response.data);
  } catch (error) {
    console.log(error);
  }
};

const editProfile = async (editProfileValues) => {
  await API.put("user", editProfileValues);
};

const addPicture = async (image) => {
  await API.put("user/add-picture", image, {headers: {"content-type": "multipart/form-data"}});
};

// eslint-disable-next-line
export default {
  register,
  getProfile,
  editProfile,
  addPicture,
};