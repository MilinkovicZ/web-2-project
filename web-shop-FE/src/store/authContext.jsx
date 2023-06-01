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
      const response = await API.post("Auth/Login", loginData);
      setToken(response.data.token);
      localStorage.setItem("token", response.data.token);
      navigator("/dashboard");
    } catch (error) {
      if (error.response) 
        alert(error.response.data.Exception);
    }
  };

  const googleLogin = async (loginData) => {
    try {
      const response = await API.post("Auth/RegisterViaGoogle", {token: loginData.credential});
      setToken(response.data.token);
      localStorage.setItem("token", response.data.token);
      navigator("/dashboard");
    } catch (error) {
      if (error.response) 
        alert(error.response.data.Exception);
    }
  }

  const roleHandler = () => {
    try {
      if (!token) return null;

      return jwtDecode(token).UserType;
    } catch (e) {
      console.log(e);
    }
  };

  const logoutHandler = () => {
    setToken(null);
    localStorage.removeItem("token");
    navigator("/");
  };

  return (
    <AuthContext.Provider
      value={{
        token,
        type: roleHandler(),
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