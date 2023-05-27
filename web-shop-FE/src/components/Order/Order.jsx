import React from "react";
import OrderItem from "./OrderItem";
import classes from "./Order.module.css";

const Order = ({ order }) => {  
  const calculateTotalPrice = () => {
    let totalPrice = 0;
    order.items.forEach((item) => {
      totalPrice += item.productAmount * item.product.price;
    });
    return totalPrice;
  };

  return (
    <React.Fragment>
      <div className={classes.orderIdAddresContainer}>
        <h2 className={classes.orderId}>Order ID: {order.id}</h2>
        <p className={classes.deliveryAddress}>To: {order.deliveryAddress}</p>
      </div>
      <h3 className={classes.items}>Items:</h3>
      {order.items.map((item) => (
        <OrderItem key={item.productId} item={item} />
      ))}
      <div className={classes.orderCommentPriceContainer}>        
        <p className={classes.comment}>Comment: {order.comment}</p>
        <p className={classes.totalPrice}>Total: ${calculateTotalPrice()}</p>
      </div>
    </React.Fragment>
  );
};

export default Order;
