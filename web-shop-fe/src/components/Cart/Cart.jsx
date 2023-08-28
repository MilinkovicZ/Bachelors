import React, { useContext, useState } from "react";
import { CartContext } from "../../store/cartContext";
import classes from "./Cart.module.css";
import { Link, useNavigate } from "react-router-dom";
import { useEffect, useRef } from "react";
import { useJsApiLoader, Autocomplete } from "@react-google-maps/api";

const Cart = () => {
  const navigator = useNavigate();
  const cartContext = useContext(CartContext);
  const [address, setAddress] = useState("");
  const [comment, setComment] = useState("");
  const [libraries] = useState(["places"]);
  const autocompleteRef = useRef(null);

  useEffect(() => {
    setAddress(cartContext.address);
    setComment(cartContext.comment);
  }, [cartContext.address, cartContext.comment]);

  const { isLoaded } = useJsApiLoader({
    googleMapsApiKey: process.env.REACT_APP_GEOCODE_KEY,
    language: "en",
    libraries,
  });

  const handleRemoveItem = (itemId) => {
    cartContext.removeFromCart(itemId);
  };

  const handleIncrementChange = (itemId) => {
    cartContext.increaseItemQuantity(itemId);
  };

  const handleDecrementChange = (itemId) => {
    cartContext.decreaseItemQuantity(itemId);
  };

  const calculateTotalPrice = () => {
    let totalPrice = 0;
    const sellerIds = [];
    for (const item of cartContext.cartItems) {
      totalPrice += item.price * item.quantity;
      if (!sellerIds.includes(item.sellerId)) sellerIds.push(item.sellerId);
    }

    return (totalPrice + sellerIds.length * 2.99).toFixed(2);
  };

  const handlePurchase = async () => {
    if (!address || address.trim() === "") {
      alert("Address is reqired");
      return;
    }

    cartContext.setCartAddress(address);
    cartContext.setCartComment(comment);
    navigator("/purchase");
  };

  if (!isLoaded) {
    return <div>Loading...</div>;
  }

  return (
    <div className={classes.shoppingCart}>
      <h2>Shopping Cart</h2>
      <ul className={classes.itemList}>
        {cartContext.cartItems.map((item) => (
          <li key={item.id} className={classes.item}>
            <div className={classes.itemNamePrice}>
              <span className={classes.itemName}>{item.name}</span>
              <div className={classes.itemPrice}>
                Price: ${item.price * item.quantity}
              </div>
            </div>
            <div className={classes.itemActions}>
              <button
                className={classes.decrementButton}
                onClick={() => handleDecrementChange(item.id)}
                disabled={item.quantity === 1}
              >
                -
              </button>
              <input
                type="text"
                className={classes.itemQuantity}
                value={item.quantity}
                readOnly
              />
              <button
                className={classes.incrementButton}
                onClick={() => handleIncrementChange(item.id)}
                disabled={item.quantity === item.amount}
              >
                +
              </button>
            </div>
            <button
              className={classes.removeButton}
              onClick={() => handleRemoveItem(item.id)}
            >
              Remove
            </button>
          </li>
        ))}
      </ul>
      <div className={classes.inputFields}>
        <label htmlFor="address">Address:</label>
        <Autocomplete
          onLoad={(autocomplete) => {
            autocompleteRef.current = autocomplete;
          }}
          onPlaceChanged={() => {
            const selectedPlace = autocompleteRef.current.getPlace();
            if (selectedPlace && selectedPlace.formatted_address) {
              setAddress(selectedPlace.formatted_address);
            }
          }}
        >
          <input
            placeholder="Insert your address"
            type="text"
            id="address"
            required
          />
        </Autocomplete>
        <label htmlFor="comment">Comment:</label>
        <input
          placeholder="Insert your comment"
          type="text"
          id="comment"
          value={comment}
          onChange={(e) => setComment(e.target.value)}
        />
      </div>
      <div className={classes.totalPrice}>
        Total Price: ${calculateTotalPrice()}
      </div>
      <div className={classes.note}>
        <p>
          Delivery fee is <strong>$2.99</strong> per seller.
        </p>
      </div>
      <div className={classes.linkButton}>
        <Link className={classes.link} to="/new-order" />
        <button className={classes.confirmPurchase} onClick={handlePurchase}>
          Confirm Purchase
        </button>
      </div>
    </div>
  );
};

export default Cart;
