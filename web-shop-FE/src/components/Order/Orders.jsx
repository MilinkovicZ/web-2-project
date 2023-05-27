import React, { useEffect, useState } from "react";
import Order from "./Order";
import adminService from "../../services/adminService";
import classes from "./Orders.module.css";

const Orders = () => {
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    fetchOrders();
  }, []);

  const fetchOrders = async () => {
    try {
      const allOrders = await adminService.getOrders();
      setOrders(allOrders);
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
          <Order order={order} />
        </div>
      ))}
    </div>
  );
};

export default Orders