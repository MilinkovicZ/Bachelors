import React, { useEffect, useState } from "react";
import API from "../api/api.js";
import jwtDecode from "jwt-decode";
import { useNavigate } from "react-router-dom";

const AuthContext = React.createContext();

export const AuthContextProvider = (props) => {
  const [token, setToken] = useState(null);
  const navigator = useNavigate();

  useEffect(() => {
    setToken(localStorage.getItem("token"));
  }, []);

  const loginHandler = async (loginData) => {
    try {
      const response = await API.post("auth/login", loginData);
      setToken(response.data.token);
      localStorage.setItem("token", response.data.token);
      localStorage.setItem("role", jwtDecode(response.data.token).UserType);
      localStorage.setItem("hasFullAccess", jwtDecode(response.data.token).HasFullAccess);
      localStorage.setItem("isAdult", jwtDecode(response.data.token).IsAdult);
      navigator("/dashboard");
    } catch (error) {
      if (error.response) 
        alert(error.response.data.Exception);
    }
  };

  const googleLogin = async (loginData) => {
    try {
      const response = await API.post("auth/register-via-google", {token: loginData.credential});
      setToken(response.data.token);
      localStorage.setItem("token", response.data.token);
      localStorage.setItem("role", jwtDecode(response.data.token).UserType);
      localStorage.setItem("hasFullAccess", jwtDecode(response.data.token).HasFullAccess);
      localStorage.setItem("isAdult", jwtDecode(response.data.token).IsAdult);
      navigator("/dashboard");
    } catch (error) {
      if (error.response) 
        alert(error.response.data.Exception);
    }
  }

  const logoutHandler = () => {
    setToken(null);
    localStorage.removeItem("token");
    localStorage.removeItem("role");
    localStorage.removeItem("hasFullAccess");
    localStorage.removeItem("isAdult");
    navigator("/");
  };

  return (
    <AuthContext.Provider
      value={{
        token,
        type: localStorage.getItem("role"),
        hasFullAccess: localStorage.getItem("hasFullAccess"),
        isAdult: localStorage.getItem("isAdult"),
        login: loginHandler,
        logout: logoutHandler,
        googleLogin: googleLogin
      }}
    >
      {props.children}
    </AuthContext.Provider>
  );
};

export default AuthContext;