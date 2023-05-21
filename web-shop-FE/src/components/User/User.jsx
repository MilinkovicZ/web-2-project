import React from 'react';
import classes from './User.module.css';

const User = ({ user,  onAccept, onDecline }) => {
  user.birthDate = user.birthDate.split("T")[0];

  return (
    <React.Fragment>
      <div className={classes.userInfo}>
        <p className={classes.label}>Username:</p>
        <p>{user.username}</p>
      </div>
      <div className={classes.userInfo}>
        <p className={classes.label}>Email:</p>
        <p>{user.email}</p>
      </div>
      <div className={classes.userInfo}>
        <p className={classes.label}>Full Name:</p>
        <p>{user.fullName}</p>
      </div>
      <div className={classes.userInfo}>
        <p className={classes.label}>Birth Date:</p>
        <p>{user.birthDate}</p>
      </div>
      <div className={classes.userInfo}>
        <p className={classes.label}>Address:</p>
        <p>{user.address}</p>
      </div>
      <div className={classes.userInfo}>
        <p className={classes.label}>ID:</p>
        <p>{user.id}</p>
        {user.verificationState === 0 && <button className={classes.verifyButton} onClick={() => onAccept(user.id)}>Verify this user</button>}
        {user.verificationState === 0 && <button className={classes.declineButton} onClick={() => onDecline(user.id)}>Decline this user</button>}
      </div>
      </React.Fragment>
  );
};

export default User;