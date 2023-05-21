import React, { useContext } from "react";
import Login from "./components/Login/Login.jsx";
import Register from "./components/Register/Register.jsx";
import { Route, Routes, Navigate } from "react-router-dom";
import Dashboard from "./components/Dashboard/Dashboard.jsx";
import AuthContext from "./store/authContext.jsx";
import Header from "./components/Header/Header.jsx";
import Profile from "./components/Profile/Profile.jsx";

function App() {
  const context = useContext(AuthContext);
  const token = context.token;
  const role = context.type;

  return (
    <React.Fragment>
      <Header/>
      <Routes>
        <Route
          path="/"
          element={!role ? <Login /> : <Navigate to="/dashboard" />}
        />
        <Route
          path="/register"
          element={!role ? <Register /> : <Navigate to="/dashboard" />}
        />
        <Route
          path="/dashboard"
          element={role ? <Dashboard userType={role} /> : <Navigate to="/" />}
        />
        <Route
          path="/editProfile"
          element={role ? <Profile /> : <Navigate to="/" />}
        />
      </Routes>
    </React.Fragment>
  );
}

export default App;
