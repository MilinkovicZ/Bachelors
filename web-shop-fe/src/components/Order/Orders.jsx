import React, { useEffect, useState } from "react";
import Order from "./Order";
import adminService from "../../services/adminService";
import sellerService from "../../services/sellerService";
import classes from "./Orders.module.css";
import buyerService from "../../services/buyerService";

const Orders = ({ userType }) => {
  const [orders, setOrders] = useState([]);
  const [newOrders, setNewOrders] = useState(userType === "Seller" ? [] : null);

  useEffect(() => {
    if (userType === "Admin") {
      fetchAdminOrders();
    } else if (userType === "Seller") {
      fetchSellerOrders();
    } else if (userType === "Buyer") {
      fetchBuyerOrders();
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

  const fetchBuyerOrders = async () => {
    try {
      const allOrders = await buyerService.getOrders();
      setOrders(allOrders);
    } catch (error) {
      if (error.response) {
        alert(error.response.data.Exception);
      }
    }
  };

  const cancelOrderHandler = async (id) => {
    try {
      await buyerService.declineOrder(id);
      fetchBuyerOrders();
    } catch (error) {
      if (error.response) {
        alert(error.response.data.Exception);
      }
    }
  }

  return (
    <div className={classes.container}>
      <div className={classes.ordersContainer}>
        <h1 className={classes.title}>All Orders</h1>
        {orders.length === 0 ? (
          <h1 className={classes.ordersMissing}>There are no orders!</h1>
        ) : (
          orders.map((order) => (
            <div className={classes.orderContainer} key={order.id}>
              <Order order={order} userType={userType} onCancel={() => cancelOrderHandler(order.id)}/>
            </div>
          ))
        )}
      </div>
      {userType === "Seller" && (
        <div className={classes.ordersContainer}>
          <h1 className={classes.title}>New Orders</h1>
          {newOrders.length === 0 ? (
            <h1 className={classes.ordersMissing}>There are no new orders!</h1>
          ) : (
            newOrders.map((order) => (
              <div className={classes.orderContainer} key={order.id}>
                <Order order={order} userType={userType} />
              </div>
            ))
          )}
        </div>
      )}
    </div>
  );
};

export default Orders;
