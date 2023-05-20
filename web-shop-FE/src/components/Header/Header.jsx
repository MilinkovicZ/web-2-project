import React, { useContext } from 'react';
import AuthContext from '../../store/authContext';
import classes from './Header.module.css';
import { Link } from 'react-router-dom';

const Header = () => {
  const authContext = useContext(AuthContext);

  const handleLogout = () => {
    authContext.logout();
  };

  return (
    <header className={classes.header}>
      <div className={classes.logo}>
        <img src="/shoppingCart.png" alt="Shop Logo" className={classes.logoImage} />
      </div>
      <div className={classes.nav}>
        {authContext.token && <Link to='/editProfile' className={classes.link}>Edit Profile</Link>}
        {authContext.token && <button onClick={handleLogout} className={classes.logoutButton}>Logout</button>}
      </div>
    </header>
  );
};

export default Header;