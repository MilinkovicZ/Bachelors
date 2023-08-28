import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import classes from "./Profile.module.css";
import userService from "../../services/userService";

const Profile = () => {
  const navigate = useNavigate();

  const [photo, setPhoto] = useState(null);
  const [editValues, setEditValues] = useState({
    username: "",
    email: "",
    fullName: "",
    birthDate: "",
    address: "",
    password: "",
    newPassword: "",
  });

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const user = await userService.getProfile();
        setPhoto(user.image);
        setEditValues({
          username: user.username,
          email: user.email,
          fullName: user.fullName,
          birthDate: user.birthDate.split("T")[0],
          address: user.address,
        });
      } catch (error) {
        console.log(error);
      }
    };

    fetchUser();
  }, [photo]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!editValues.username || editValues.username.trim() === "") {
      alert("Username is required");
      return;
    }

    const usernameRegex = /^[a-zA-Z0-9]+$/;
    if (!usernameRegex.test(editValues.username)) {
      alert("Invalid username format");
      return;
    }

    if (!editValues.email) {
      alert("Email is required");
      return;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(editValues.email)) {
      alert("Invalid email format");
      return;
    }

    if (!editValues.fullName || editValues.fullName.trim() === "") {
      alert("Full name is required");
      return;
    }

    if (!editValues.birthDate) {
      alert("Birth date is required");
      return;
    }

    if (!editValues.address || editValues.address.trim() === "") {
      alert("Address is required");
      return;
    }

    if (!editValues.password) {
      alert("Password is reqired");
      return;
    }

    try {
      await userService.editProfile(editValues);
      navigate("/dashboard");
    } catch (error) {
      if (error.response) {
        alert(error.response.data.Exception);
      }
    }
  };

  const pictureSubmit = async (e) => {
    e.preventDefault();

    const image = e.target.elements.image.files[0];

    if (!image) {
      alert("No file selected");
      return;
    }

    const formData = new FormData();
    formData.append("image", image);
    
    try {
      await userService.addPicture(formData);      
      setPhoto("");
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <React.Fragment>
      <div className={classes.pictureContainer}>
        <h2 className={classes.title}>Change picture</h2>
        <img
          className={classes.picture} alt='Default'
          src={photo ? `data:image/jpg;base64,${photo}` : "default.jpg"}
        />
        <form onSubmit={pictureSubmit} className={classes.pictureForm}>
          <input
            className={classes.pictureInputField}
            type="file"
            name="image"
            accept="image/*"
          />
          <button className={classes.pictureButton} type="submit">
            Upload photo
          </button>
        </form>
      </div>
      <div className={classes.formContainer}>
        <h2 className={classes.title}>Edit profile</h2>
        <form onSubmit={handleSubmit}>
          <div className={classes.input}>
            <label className={classes.label}>Username:</label>
            <input
              type="text"
              value={editValues.username}
              onChange={(e) =>
                setEditValues({ ...editValues, username: e.target.value })
              }
              className={classes.inputField}
            />
          </div>
          <div className={classes.input}>
            <label className={classes.label}>New Password:</label>
            <input
              type="password"
              onChange={(e) =>
                setEditValues({ ...editValues, newPassword: e.target.value })
              }
              className={classes.inputField}
            />
          </div>
          <div className={classes.input}>
            <label className={classes.label}>Email:</label>
            <input
              type="email"
              value={editValues.email}
              onChange={(e) =>
                setEditValues({ ...editValues, email: e.target.value })
              }
              className={classes.inputField}
            />
          </div>
          <div className={classes.input}>
            <label className={classes.label}>Full Name:</label>
            <input
              type="text"
              value={editValues.fullName}
              onChange={(e) =>
                setEditValues({ ...editValues, fullName: e.target.value })
              }
              className={classes.inputField}
            />
          </div>
          <div className={classes.input}>
            <label className={classes.label}>Birth Date:</label>
            <input
              type="date"
              value={editValues.birthDate}
              min="1900-01-01"
              max="2010-01-01"
              onChange={(e) =>
                setEditValues({ ...editValues, birthDate: e.target.value })
              }
              className={classes.inputField}
            />
          </div>
          <div className={classes.input}>
            <label className={classes.label}>Address:</label>
            <input
              type="text"
              value={editValues.address}
              onChange={(e) =>
                setEditValues({ ...editValues, address: e.target.value })
              }
              className={classes.inputField}
            />
          </div>
          <div className={classes.input}>
            <label className={classes.labelPassword}>
              Enter Your Password:
            </label>
            <input
              type="password"
              id="password"
              className={classes.inputField}
              onChange={(e) =>
                setEditValues({ ...editValues, password: e.target.value })
              }
            />
          </div>
          <button type="submit" className={classes.button}>
            Save Changes
          </button>
        </form>
      </div>
    </React.Fragment>
  );
};

export default Profile;