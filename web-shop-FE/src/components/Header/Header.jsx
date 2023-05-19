import React, { useContext } from 'react';
import AuthContext from '../../store/authContext';
import userService from '../../services/userService';
import classes from './Header.module.css';
import { useNavigate } from 'react-router-dom';

const Header = () => {
  const authContext = useContext(AuthContext);
  const navigator = useNavigate();

  const handleLogout = () => {
    authContext.logout();
  };

  const handleEditProfile = () => {
    console.log("EDITING PROFILE")
  };

  return (
    <header className={classes.header}>
      <div className={classes.logo}>
        <img src="/shoppingCart.png" alt="Shop Logo" className={classes.logoImage} />
      </div>
      <div className={classes.nav}>
        <button onClick={handleEditProfile} className={classes.editButton}>
          Edit Profile
        </button>
        <button onClick={handleLogout} className={classes.logoutButton}>
          Logout
        </button>
      </div>
    </header>
  );
};

export default Header;