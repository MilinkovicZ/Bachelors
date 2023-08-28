import React, { useState } from "react";
import classes from "./Register.module.css";
import userService from "../../services/userService";
import { Link, useNavigate } from "react-router-dom";

const Register = () => {
  const navigator = useNavigate();

  const [registerValues, setRegisterValues] = useState({
    username: "",
    email: "",
    password: "",
    confirmPassword: "",
    fullName: "",
    birthDate: "",
    address: "",
    userType: 0,
  });

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!registerValues.username || registerValues.username.trim() === "") {
      alert("Username is required");
      return;
    }

    const usernameRegex = /^[a-zA-Z0-9]+$/;
    if (!usernameRegex.test(registerValues.username)) {
      alert("Invalid username format");
      return;
    }

    if (!registerValues.email) {
      alert("Email is required");
      return;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(registerValues.email)) {
      alert("Invalid email format");
      return;
    }

    if (!registerValues.password) {
      alert("Password is required");
      return;
    }

    if (!registerValues.confirmPassword) {
      alert("You have to confirm password");
      return;
    }

    if (!registerValues.fullName || registerValues.fullName.trim() === "") {
      alert("Full name is required");
      return;
    }

    if (!registerValues.birthDate) {
      alert("Birth date is required");
      return;
    }

    if (!registerValues.address || registerValues.address.trim() === "") {
      alert("Address is required");
      return;
    }

    if (registerValues.confirmPassword !== registerValues.password) {
      alert("Password are not matching. Try again");
      return;
    }

    try {
      registerValues.userType = parseInt(registerValues.userType);
      await userService.register(registerValues);
      navigator("/");
    } catch (error) {
      if (error.response) alert(error.response.data.Exception);
    }
  };

  return (
    <React.Fragment>
      <div className={classes.container}>
        <h1 className={classes.title}>Register</h1>
        <form onSubmit={handleSubmit}>
          <div className={classes.input}>
            <label htmlFor="username" className={classes.label}>
              Username
            </label>
            <input
              type="text"
              id="username"
              value={registerValues.username}
              onChange={(event) =>
                setRegisterValues({
                  ...registerValues,
                  username: event.target.value,
                })
              }
              className={classes.inputField}
            />
          </div>
          <div className={classes.input}>
            <label htmlFor="email" className={classes.label}>
              Email
            </label>
            <input
              type="text"
              id="email"
              value={registerValues.email}
              onChange={(event) =>
                setRegisterValues({
                  ...registerValues,
                  email: event.target.value,
                })
              }
              className={classes.inputField}
            />
          </div>
          <div className={classes.input}>
            <label htmlFor="password" className={classes.label}>
              Password
            </label>
            <input
              type="password"
              id="password"
              value={registerValues.password}
              onChange={(event) =>
                setRegisterValues({
                  ...registerValues,
                  password: event.target.value,
                })
              }
              className={classes.inputField}
            />
          </div>
          <div className={classes.input}>
            <label htmlFor="confirmPassword" className={classes.label}>
              Confirm Password
            </label>
            <input
              type="password"
              id="confirmPassword"
              value={registerValues.confirmPassword}
              onChange={(event) =>
                setRegisterValues({
                  ...registerValues,
                  confirmPassword: event.target.value,
                })
              }
              className={classes.inputField}
            />
          </div>
          <div className={classes.input}>
            <label htmlFor="fullName" className={classes.label}>
              Full Name
            </label>
            <input
              type="text"
              id="fullName"
              value={registerValues.fullName}
              onChange={(event) =>
                setRegisterValues({
                  ...registerValues,
                  fullName: event.target.value,
                })
              }
              className={classes.inputField}
            />
          </div>
          <div className={classes.input}>
            <label htmlFor="birthDate" className={classes.label}>
              Birth Date
            </label>
            <input
              type="date"
              id="birthDate"
              value={registerValues.birthDate}
              min="1900-01-01"
              max="2010-01-01"
              onChange={(event) =>
                setRegisterValues({
                  ...registerValues,
                  birthDate: event.target.value,
                })
              }
              className={classes.inputField}
            />
          </div>
          <div className={classes.input}>
            <label htmlFor="address" className={classes.label}>
              Address
            </label>
            <input
              type="text"
              id="address"
              value={registerValues.address}
              onChange={(event) =>
                setRegisterValues({
                  ...registerValues,
                  address: event.target.value,
                })
              }
              className={classes.inputField}
            />
          </div>
          <div className={classes.input}>
            <label htmlFor="userType" className={classes.label}>
              User Type
            </label>
            <select
              id="userType"
              value={registerValues.userType}
              onChange={(event) =>
                setRegisterValues({
                  ...registerValues,
                  userType: event.target.value,
                })
              }
              className={classes.inputField}
            >
              <option value="0">Buyer</option>
              <option value="1">Seller</option>
            </select>
          </div>
          <button type="submit" className={classes.button}>
            Register
          </button>
        </form>
        <p>
          Already have an account? <Link to="/"> Login here.</Link>
        </p>
      </div>
    </React.Fragment>
  );
};

export default Register;
