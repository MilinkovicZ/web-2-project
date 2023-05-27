import React, { useEffect, useState } from "react";
import adminService from "../../services/adminService";
import classes from "./Users.module.css";
import User from "./User";

const Users = () => {
  const [verifiedUsers, setVerifiedUsers] = useState([]);
  const [unverifiedUsers, setUnverifiedUsers] = useState([]);

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      const allVerified = await adminService.getVerified();
      const allUnverified = await adminService.getUnverified();
      setVerifiedUsers(allVerified);
      setUnverifiedUsers(allUnverified);
    } catch (error) {
      if (error.response) 
        alert(error.response.data.Exception);
    }
  };

  const acceptUserHandler = async (userId) => {
    await verifyUserHandler({id: userId, verificationState: 1});
  }

  const declineUserHandler = async (userId) => {    
    await verifyUserHandler({id: userId, verificationState: 2});
  }

  const verifyUserHandler = async (data) => {
    try {
      await adminService.verifyUser(data);
      fetchUsers();
    } catch (error) {
      if (error.response) 
        alert(error.response.data.Exception);
    }
  }

  return (
    <div className={classes.usersContainer}>
      <div className={classes.column}>
        <h1 className={classes.title}>Verified Users</h1>
        {verifiedUsers.map((user) => (
          <div className={classes.userContainer} key={user.id}>
            <User user={user} />
          </div>
        ))}
      </div>
      <div className={classes.column}>
        <h1 className={classes.title}>Unverified Users</h1>
        {unverifiedUsers.map((user) => (
          <div className={classes.userContainer} key={user.id}>
            <User user={user} onAccept={() => acceptUserHandler(user.id)} onDecline={() => declineUserHandler(user.id)}/>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Users;