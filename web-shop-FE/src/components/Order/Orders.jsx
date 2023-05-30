import React, { useEffect, useState } from "react";
import Order from "./Order";
import adminService from "../../services/adminService";
import sellerService from "../../services/sellerService";
import classes from "./Orders.module.css";

const Orders = ({ userType }) => {
  const [orders, setOrders] = useState([]);
  const [newOrders, setNewOrders] = useState(userType === "Seller" ? [] : null);

  useEffect(() => {
    if (userType === "Admin") {
      fetchAdminOrders();
    } else if (userType === "Seller") {
      fetchSellerOrders();
    }
  }, [userType]);

  const fetchAdminOrders = async () => {
    try {
      const allOrders = await adminService.getOrders();
      setOrders(allOrders);
    } catch (error) {
      if (error.response) {
        alert(error.response.data.Exception);
      }
    }
  };

  const fetchSellerOrders = async () => {
    try {
      const allOrders = await sellerService.getOrders();
      const newSellerOrders = await sellerService.getNewOrders();
      setOrders(allOrders);
      setNewOrders(newSellerOrders);
    } catch (error) {
      if (error.response) {
        alert(error.response.data.Exception);
      }
    }
  };

  return (
    <div className={classes.ordersContainer}>
      <h1 className={classes.title}>All Orders</h1>
      {orders.map((order) => (
        <div className={classes.orderContainer} key={order.id}>
          <Order order={order} userType={userType} />
        </div>
      ))}
      {userType === "Seller" && (
        <div>
          <h1 className={classes.title}>New Orders</h1>
          {newOrders !== null &&
            newOrders.map((order) => (
              <div className={classes.orderContainer} key={order.id}>
                <Order order={order} userType={userType} />
              </div>
            ))}
        </div>
      )}
    </div>
  );
};

export default Orders;
