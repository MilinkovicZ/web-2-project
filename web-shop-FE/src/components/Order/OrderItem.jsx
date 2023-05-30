import React from "react";
import classes from "./OrderItem.module.css";

const OrderItem = ({ item }) => {
  return (
    <React.Fragment>
      <div className={classes.orderItemContainer}>
        <p className={classes.productName}>{item.product.name}</p>
        <p className={classes.productPrice}>${item.currentPrice}</p>
        <p className={classes.productAmount}>No: {item.productAmount}</p>
      </div>
      <p className={classes.productDescription}>Description: {item.product.description}</p>
    </React.Fragment>
  );
};

export default OrderItem;
