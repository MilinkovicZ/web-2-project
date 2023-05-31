import React from "react";
import classes from "./Dashboard.module.css";
import { Link } from "react-router-dom";

const Dashboard = ({ userType }) => {
  return (
    <div>
      {userType === "Admin" && (
        <div className={classes.linkContainer}>
          <Link to="/users" className={classes.link}>
            Users
          </Link>
          <Link to="/orders" className={classes.link}>
            Orders
          </Link>
        </div>
      )}
      {userType === "Seller" && (
        <div className={classes.linkContainer}>
          <Link to="/products" className={classes.link}>
            Products
          </Link>
          <Link to="/orders_seller" className={classes.link}>
            Orders
          </Link>
          <Link to="/create_new_product" className={classes.link}>
            New Product
          </Link>
        </div>
      )}
      {userType === "Buyer" && (
        <div className={classes.linkContainer}>
          <Link to="/orders_buyer" className={classes.link}>
            Orders
          </Link>
          <Link to="/create_new_order" className={classes.link}>
            New Order
          </Link>
        </div>
      )}
    </div>
  );
};

export default Dashboard;
