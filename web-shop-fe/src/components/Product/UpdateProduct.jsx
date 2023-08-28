import React, { useState, useEffect } from "react";
import sellerService from "../../services/sellerService";
import { useParams, useNavigate } from "react-router-dom";
import classes from "./UpdateProduct.module.css";

const UpdateProduct = () => {
  const { id } = useParams();
  const navigator = useNavigate();

  const [updateProductValues, setUpdateProductValues] = useState({
    name: "",
    price: "",
    amount: "",
    description: "",
    image: "",
    imageForm: null,
    category: 0,
  });

  useEffect(() => {
    fetchProduct(id);
  }, [id]);

  const fetchProduct = async (id) => {
    try {
      const product = await sellerService.getProduct(id);
      setUpdateProductValues({
        name: product.name,
        price: product.price,
        amount: product.amount,
        description: product.description,
        image: product.image,
        category: product.category,
      });
    } catch (error) {
      if (error.response) {
        alert(error.response.data.Exception);
      }
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!updateProductValues.name || updateProductValues.name.trim() === "") {
      alert("Name is required");
      return;
    }

    if (
      isNaN(updateProductValues.price) ||
      parseFloat(updateProductValues.price) < 0
    ) {
      alert("Price must be a positive number");
      return;
    }

    if (
      isNaN(updateProductValues.amount) ||
      parseInt(updateProductValues.amount) < 0
    ) {
      alert("Amount must be a positive integer");
      return;
    }

    try {
      await sellerService.updateProduct(id, updateProductValues);
      navigator("/products");
    } catch (error) {
      if (error.response) alert(error.response.data.Exception);
    }
  };

  const handleImageChange = (e) => {
    const file = e.target.files[0];
    setUpdateProductValues({
      ...updateProductValues,
      imageForm: file,
    });
  };

  return (
    <div className={classes.container}>
      <h1 className={classes.title}>Update Product</h1>
      <form className={classes.form} onSubmit={handleSubmit}>
        <label htmlFor="name">Name:</label>
        <input
          type="text"
          id="name"
          name="name"
          value={updateProductValues.name}
          onChange={(event) =>
            setUpdateProductValues({
              ...updateProductValues,
              name: event.target.value,
            })
          }
          required
        />

        <label htmlFor="price">Price:</label>
        <input
          type="number"
          id="price"
          name="price"
          value={updateProductValues.price}
          onChange={(event) =>
            setUpdateProductValues({
              ...updateProductValues,
              price: event.target.value,
            })
          }
          required
        />

        <label htmlFor="amount">Amount:</label>
        <input
          type="number"
          id="amount"
          name="amount"
          value={updateProductValues.amount}
          onChange={(event) =>
            setUpdateProductValues({
              ...updateProductValues,
              amount: event.target.value,
            })
          }
          required
        />

        <label htmlFor="category">Category:</label>
        <select
          id="category"
          name="category"
          value={updateProductValues.category}
          onChange={(event) =>
            setUpdateProductValues({
              ...updateProductValues,
              category: event.target.value,
            })
          }
        >
          <option value="0">Food</option>
          <option value="1">Drink</option>
          <option value="2">Alcohol</option>
          <option value="3">Cigarettes</option>
        </select>

        <label htmlFor="description">Description:</label>
        <textarea
          id="description"
          name="description"
          value={updateProductValues.description}
          onChange={(event) =>
            setUpdateProductValues({
              ...updateProductValues,
              description: event.target.value,
            })
          }
        />

        <img
          className={classes.img}
          alt="Default"
          src={
            updateProductValues.image
              ? `data:image/jpg;base64,${updateProductValues.image}`
              : "default.jpg"
          }
        />
        <input
          type="file"
          id="image"
          name="image"
          accept="image/*"
          onChange={handleImageChange}
        />

        <button type="submit">Update Product</button>
      </form>
    </div>
  );
};

export default UpdateProduct;
