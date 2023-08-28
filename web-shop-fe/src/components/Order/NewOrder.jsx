import React from "react";
import Products from "../Product/Products";
import classes from "./NewOrder.module.css"
import { Link } from "react-router-dom";

const NewOrder = () => {
  return (
    <div className={classes.container}>
      <Products userType="Buyer"></Products>
      <Link className={classes.cartLink} to="/cart">GO TO CART</Link>
    </div>
  )
}

export default NewOrder;