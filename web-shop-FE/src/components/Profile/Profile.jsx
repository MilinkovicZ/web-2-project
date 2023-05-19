import React, { useContext, useEffect, useState } from 'react';
import AuthContext from '../../store/authContext';
import { useNavigate } from 'react-router-dom';
import classes from './Profile.module.css';
import userService from '../../services/userService';

const Profile = () => {
  const navigate = useNavigate();
  const authContext = useContext(AuthContext);
  const token = authContext.token;

  const [editValues, setEditValues] = useState({
    username: '',
    email: '',
    fullName: '',
    birthDate: '',
    address: '',
    password: '',
    newPassword: '',
  });

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const user = await userService.getProfile();
        const date = new Date(user.birthDate);
        user.birthDate = date.toISOString().split('T')[0];
        setEditValues({
          username: user.username,
          email: user.email,
          fullName: user.fullName,
          birthDate: user.birthDate,
          address: user.address,
        });
      } catch (error) {
        console.log(error);
      }
    };

    fetchUser();
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!editValues.username) {
      alert('Username is required');
      return;
    }

    const usernameRegex = /^[a-zA-Z0-9]+$/;
    if (!usernameRegex.test(editValues.username)) {
      alert('Invalid username format');
      return;
    }

    if (!editValues.email) {
      alert('Email is required');
      return;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(editValues.email)) {
      alert('Invalid email format');
      return;
    }

    if (!editValues.fullName) {
      alert('Full name is required');
      return;
    }

    if (!editValues.birthDate) {
      alert('Birth date is required');
      return;
    }

    if (!editValues.address) {
      alert('Address is required');
      return;
    }

    try {
      await userService.editProfile(editValues);
      navigate('/');
    } catch (error) {
      if (error.response) 
      alert(error.response.data.Exception);
    }
  };

  return (
    <div className={classes.container}>
      <h2 className={classes.title}>Edit Profile</h2>
      <form onSubmit={handleSubmit}>
        <div className={classes.input}>
          <label className={classes.label}>Username:</label>
          <input
            type="text"
            value={editValues.username}
            onChange={(e) => setEditValues({ ...editValues, username: e.target.value })}
            className={classes.inputField}
          />
        </div>
        <div className={classes.input}>
          <label className={classes.label}>Email:</label>
          <input
            type="email"
            value={editValues.email}
            onChange={(e) => setEditValues({ ...editValues, email: e.target.value })}
            className={classes.inputField}
          />
        </div>
        <div className={classes.input}>
          <label className={classes.label}>Full Name:</label>
          <input
            type="text"
            value={editValues.fullName}
            onChange={(e) => setEditValues({ ...editValues, fullName: e.target.value })}
            className={classes.inputField}
          />
        </div>
        <div className={classes.input}>
          <label className={classes.label}>Birth Date:</label>
          <input
            type="date"
            value={editValues.birthDate}
            onChange={(e) => setEditValues({ ...editValues, birthDate: e.target.value })}
            className={classes.inputField}
          />
        </div>
        <div className={classes.input}>
          <label className={classes.label}>Address:</label>
          <input
            type="text"
            value={editValues.address}
            onChange={(e) => setEditValues({ ...editValues, address: e.target.value })}
            className={classes.inputField}
          />
        </div>
        <div className={classes.input}>
          <label className={classes.label}>Enter Your Password:</label>
          <input
            type="password"
            id="password"
          />
        </div>
        <button type="submit" className={classes.button}>
          Save Changes
        </button>
      </form>
    </div>
  );
};

export default Profile;