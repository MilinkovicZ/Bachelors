import React, { useContext, useEffect, useState } from "react";
import AuthContext from "../../store/authContext";
import sellerService from "../../services/sellerService";
import Product from "./Product";
import classes from "./Products.module.css";
import { useNavigate } from "react-router-dom";
import buyerService from "../../services/buyerService";

const Products = ({ userType }) => {
  const [products, setProducts] = useState([]);
  const authContext = useContext(AuthContext);

  const navigator = useNavigate();

  useEffect(() => {
    if (userType === "Seller") {
      fetchSellerProducts();
    } else if (userType === "Buyer") {
      fetchBuyerProducts();
    }
  }, [userType]);

  const fetchSellerProducts = async () => {
    try {
      const fetchedProducts = await sellerService.getProducts();
      setProducts(fetchedProducts);
    } catch (error) {
      if (error.response) {
        alert(error.response.data.Exception);
      }
    }
  };

  const fetchBuyerProducts = async () => {
    try {
      const fetchedProducts = await buyerService.getProducts();
      setProducts(fetchedProducts);
    } catch (error) {
      if (error.response) {
        alert(error.response.data.Exception);
      }
    }
  };

  const handleDeleteProduct = async (id) => {
    try {
      await sellerService.deleteProduct(id);
      fetchSellerProducts();
    } catch (error) {
      if (error.response) {
        alert(error.response.data.Exception);
      }
    }
  };

  const updateProductHandler = async (id) => {
    navigator("/update-product/" + id);
  };

  return (
    <div>
      {userType === "Seller" ? (
        <h1 className={classes.title}>My Products</h1>
      ) : (
        <h1 className={classes.title}>MAKE A NEW ORDER</h1>
      )}
      <div className={classes.container}>
        {userType === "Seller" &&
          products.length !== 0 &&
          products.map((product) => (
            <Product
              key={product.id}
              onDeleteProduct={handleDeleteProduct}
              onUpdateProduct={updateProductHandler}
              product={product}
              userType="Seller"
            />
          ))}
        {userType === "Buyer" &&
          products.length !== 0 &&
          products.map((product) => {
            if (
              product.category === 0 ||
              product.category === 1 ||
              ((product.category === 2 || product.category === 3) &&
                authContext.isAdult === "True")
            ) {
              return (
                <Product key={product.id} product={product} userType="Buyer" />
              );
            } else {
              return null;
            }
          })}
      </div>
    </div>
  );
};

export default Products;
