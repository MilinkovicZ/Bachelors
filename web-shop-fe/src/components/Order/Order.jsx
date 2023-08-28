import React, { useEffect, useState } from "react";
import OrderItem from "./OrderItem";
import classes from "./Order.module.css";

const Order = ({ order, userType, onCancel }) => {
  let orderState = "";
  if (order.orderState === 0) orderState = "Preparing";
  else if (order.orderState === 1) orderState = "Delivered";
  else orderState = "Canceled";

  const [remainingTime, setRemainingTime] = useState(
    calculateRemainingTime(order.deliveryTime)
  );

  useEffect(() => {
    const interval = setInterval(() => {
      const newRemainingTime = calculateRemainingTime(order.deliveryTime);
      setRemainingTime(newRemainingTime);
    }, 1000);

    return () => {
      clearInterval(interval);
    };
  }, [order.deliveryTime]);

  function calculateRemainingTime(deliveryTime) {
    const deliveryDate = new Date(deliveryTime);
    const startDate = new Date();
    const remainingTime = deliveryDate.getTime() - startDate.getTime();

    const hours = Math.floor(remainingTime / (1000 * 60 * 60));
    const minutes = Math.floor(
      (remainingTime % (1000 * 60 * 60)) / (1000 * 60)
    );
    const seconds = Math.floor((remainingTime % (1000 * 60)) / 1000);

    const formattedTime = `${hours}:${minutes
      .toString()
      .padStart(2, "0")}:${seconds.toString().padStart(2, "0")}`;

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
        <p className={classes.orderStatusState}>{order.isAccepted ? orderState : "Not Accepted"}</p>
      </div>
      {order.orderState === 0 && order.isAccepted && (
        <div className={classes.deliveryTime}>
          <p className={classes.deliveryTimeLabel}>Delivery Time :</p>
          <p className={classes.deliveryTimeState}>
            {remainingTime}
          </p>
        </div>
      )}
      <h3 className={classes.items}>Items:</h3>
      {order.items.map((item) => (
        <OrderItem key={item.productId} item={item} />
      ))}
      <div className={classes.orderCommentPriceContainer}>
        <p className={classes.comment}>Comment: {order.comment}</p>
        <p className={classes.totalPrice}>Total: ${order.totalPrice.toFixed(2)}</p>
      </div>
      {userType === "Buyer" && order.orderState === 0 && (
        <div className={classes.buttonContainer}>
          <button
            className={classes.cancelButton}
            onClick={() => onCancel(order.id)}
          >
            Cancel Order
          </button>
        </div>
      )}
    </React.Fragment>
  );
};

export default Order;
