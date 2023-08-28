import React, { useContext, useState, useEffect} from "react";
import classes from "./Product.module.css";
import { CartContext } from "../../store/cartContext";

const Product = ({ product, onDeleteProduct, onUpdateProduct, userType }) => {
  const [quantity, setQuantity] = useState(0);
  const [isButtonClicked, setIsButtonClicked] = useState(false);
  const [isAlreadyInCart, setIsAlreadyInCart] = useState(false);
  const cartContext = useContext(CartContext);

  useEffect(() => {    
    const cartItem = cartContext.cartItems.find((item) => item.id === product.id);

    if (cartItem) {
      setQuantity(cartItem.quantity);
      setIsAlreadyInCart(true);      
      setIsButtonClicked(true);
    } else {
      setQuantity(0);
      setIsAlreadyInCart(false);
      setIsButtonClicked(false);
    }

  }, [cartContext.cartItems, product.id]);

  const handleInputChange = (e) => {
    if (e.target.value < 0) {
      setQuantity(0);
    } else if (e.target.value > product.amount) {
      setQuantity(product.amount);
    } else {
      setQuantity(e.target.value);
    }
  };

  const handleAddToCart = () => {
    let number = parseInt(quantity);
    if (number === 0) {
      alert("Can't order 0 items");
      return;
    }

    const cartItem = {
      id: product.id,
      price: product.price,
      name: product.name,
      amount: product.amount,
      sellerId: product.sellerId,
      quantity: number,
    };

    cartContext.addToCart(cartItem);
  };

  return (
    <div className={classes.productItem}>
      <div className={classes.namePriceContainer}>
        <h3>{product.name}</h3>
        <p className={classes.price}>Price: ${product.price}</p>
      </div>
      {userType === "Seller" && (
        <div>
          <button
            className={classes.updateButton}
            onClick={() => onUpdateProduct(product.id)}
          >
            Update this product
          </button>
          <button
            className={classes.deleteButton}
            onClick={() => onDeleteProduct(product.id)}
          >
            X
          </button>
        </div>
      )}
      <img
        className={classes.picture}
        alt="Default"
        src={
          product.image
            ? `data:image/jpg;base64,${product.image}`
            : "defaultProduct.jpg"
        }
      />
      <p className={classes.amount}>In stock: {product.amount}</p>
      <p className={classes.description}>Description: {product.description}</p>
      {userType === "Buyer" && (
        <div className={classes.buyerSection}>
          <input
            type="text"
            className={classes.quantityInput}
            value={quantity}
            onChange={handleInputChange}
            readOnly={isAlreadyInCart}
          />
          <button
            onClick={handleAddToCart}
            className={classes.quantityButton}
            disabled={isButtonClicked}
          >
            Add To Cart
          </button>
        </div>
      )}
    </div>
  );
};

export default Product;
