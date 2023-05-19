import React from "react";
import Header from "../Header/Header";
import Profile from "../Profile/Profile";

const Dashboard = ({userType}) => {
    return (
        <React.Fragment>            
            <Header/>
            <Profile/>
        </React.Fragment>
    )
}

export default Dashboard;