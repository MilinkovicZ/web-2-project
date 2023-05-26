import React from "react";
import Users from "../User/Users";

const Dashboard = ({ userType }) => {
  return (
    <React.Fragment>
      <h1>DASHBOARD, {userType}</h1>
      {userType === "Admin" && <Users/>}
    </React.Fragment>
  );
};

export default Dashboard;