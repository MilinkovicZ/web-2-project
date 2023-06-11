import React, { useContext } from "react";
import AuthContext from "../../store/authContext";
import classes from "./Header.module.css";
import { Link, useLocation } from "react-router-dom";

const Header = () => {
  const authContext = useContext(AuthContext);
  const location = useLocation();

  const handleLogout = () => {
    authContext.logout();
  };

  const renderEditProfileLink = () => {
    if (location.pathname === '/dashboard') {
      return (
        <Link to="/edit-profile" className={classes.link}>
          Edit Profile
        </Link>
      );
    } else {
      return (
        <Link to="/dashboard" className={classes.link}>
          Dashboard
        </Link>
      );
    }
  }

  return (
    <header className={classes.header}>
      <div className={classes.logo}>
        <img
          src="/shoppingCart.png"
          alt="Shop Logo"
          className={classes.logoImage}          
        />
      </div>
      <div className={classes.nav}>
        {authContext.token && renderEditProfileLink()}
        {authContext.token && (
          <button onClick={handleLogout} className={classes.logoutButton}>
            Logout
          </button>
        )}
      </div>
    </header>
  );
};

export default Header;