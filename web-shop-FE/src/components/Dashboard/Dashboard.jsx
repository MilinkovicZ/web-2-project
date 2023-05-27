import React from "react";
import Users from "../User/Users";
import Orders from "../Order/Orders";
import classes from "./Dashboard.module.css"

const Dashboard = ({ userType }) => {
  return (
    <div className={classes.container}>
      {userType === "Admin" && <Users/>}
      {userType === "Admin" && <Orders/>}
    </div>
  );
};

export default Dashboard;