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
          <Link to="/my-orders" className={classes.link}>
            Orders
          </Link>
          <Link to="/new-product" className={classes.link}>
            New Product
          </Link>
          <Link to="/packages" className={classes.link}>
            Map
          </Link>
        </div>
      )}
      {userType === "Buyer" && (
        <div className={classes.linkContainer}>
          <Link to="/previous-orders" className={classes.link}>
            Orders
          </Link>
          <Link to="/new-order" className={classes.link}>
            New Order
          </Link>
        </div>
      )}
    </div>
  );
};

export default Dashboard;
