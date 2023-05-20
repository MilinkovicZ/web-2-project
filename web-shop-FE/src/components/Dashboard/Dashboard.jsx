import React from "react";
import Profile from "../Profile/Profile";

const Dashboard = ({userType}) => {
    return (
        <React.Fragment>
            <h1>DASHBOARD, {userType}</h1>
        </React.Fragment>
    )
}

export default Dashboard;