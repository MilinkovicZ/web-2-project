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

  let orderState = "";
  if (order.orderState === 0) orderState = "Preparing";
  else if (order.orderState === 1) orderState = "Delivered";
  else orderState = "Canceled";

  function calculateRemainingTime(deliveryTime) {
    const deliveryDate = new Date(deliveryTime);
    const startDate = new Date();
    const remainingTime = deliveryDate.getTime() - startDate.getTime();
  
    const hours = Math.floor(remainingTime / (1000 * 60 * 60));
    const minutes = Math.floor((remainingTime % (1000 * 60 * 60)) / (1000 * 60));
    const formattedTime = `${hours.toString().padStart(2, '0')}h ${minutes.toString().padStart(2, '0')}min`;
  
    return formattedTime;
  }

  return (
    <React.Fragment>
      <div className={classes.orderIdAddresContainer}>
        <h2 className={classes.orderId}>Order ID: {order.id}</h2>
        <p className={classes.deliveryAddress}>To: {order.deliveryAddress}</p>
      </div>
      <div className={classes.orderStatus}>
        <p className={classes.orderStatusLabel}>Status:</p>
        <p className={classes.orderStatusState}>{orderState}</p>
      </div>
      {order.orderState === 0 && (
        <div className={classes.deliveryTime}>
          <p className={classes.deliveryTimeLabel}>Delivery Time :</p>
          <p className={classes.deliveryTimeState}>
            {calculateRemainingTime(order.deliveryTime)}
          </p>
        </div>
      )}
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
