import React, { useContext, useState } from "react";
import classes from "./Login.module.css";
import { Link } from "react-router-dom";
import AuthContext from "../../store/authContext";
import { GoogleLogin } from "@react-oauth/google";

const Login = () => {
  const authContext = useContext(AuthContext);

  const [loginValues, setLoginValues] = useState({
    email: "",
    password: "",
  });

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!loginValues.email) {
      alert("Email is required");
      return;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(loginValues.email)) {
      alert("Invalid email format");
      return;
    }

    if (!loginValues.password) {
      alert("Password is required");
      return;
    }

    authContext.login(loginValues);
  };

  const handleGoogleLogin = (e) => {
    authContext.googleLogin(e);
  }

  return (
    <React.Fragment>
      <div className={classes.container}>
        <h1 className={classes.title}>Welcome to Web Shop!</h1>
        <form onSubmit={handleSubmit}>
          <div className={classes.input}>
            <label htmlFor="email" className={classes.label}>
              Email
            </label>
            <input
              type="text"
              id="email"
              value={loginValues.email}
              onChange={(event) =>
                setLoginValues({ ...loginValues, email: event.target.value })
              }
              className={classes.inputField}
            />
          </div>
          <div className={classes.input}>
            <label htmlFor="password" className={classes.label}>
              Password:
            </label>
            <input
              type="password"
              id="password"
              value={loginValues.password}
              onChange={(event) =>
                setLoginValues({ ...loginValues, password: event.target.value })
              }
              className={classes.inputField}
            />
          </div>
          <button type="submit" className={classes.button}>
            Login
          </button>
          <p>
            Don't have an account? <Link to="/register"> Register here.</Link>
          </p>
        </form>
        
        <GoogleLogin
          onSuccess={handleGoogleLogin}
          onError={(e) => alert("An error occured, please try again.")}
        />
      </div>
    </React.Fragment>
  );
};

export default Login;