import React, { useState } from "react";
import sellerService from "../../services/sellerService";
import { useNavigate } from "react-router-dom";
import classes from "./CreateProduct.module.css";

const CreateProduct = () => {
  const navigator = useNavigate();

  const [createProductValues, setCreateProductValues] = useState({
    name: "",
    price: "",
    amount: "",
    description: "",
    imageForm: null,
    category: 0,
  });

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!createProductValues.name || createProductValues.name.trim() === "") {
      alert("Name is required");
      return;
    }

    if (
      isNaN(createProductValues.price) ||
      parseFloat(createProductValues.price) < 0
    ) {
      alert("Price must be a positive number");
      return;
    }

    if (
      isNaN(createProductValues.amount) ||
      parseInt(createProductValues.amount) < 0
    ) {
      alert("Amount must be a positive integer");
      return;
    }

    try {
      await sellerService.createProduct(createProductValues);
      navigator("/products");
    } catch (error) {
      if (error.response) alert(error.response.data.Exception);
    }
  };

  const handleImageChange = (e) => {
    const file = e.target.files[0];
    setCreateProductValues({
      ...createProductValues,
      imageForm: file,
    });
  };

  return (
    <div className={classes.container}>
      <h1 className={classes.title}>Make New Product</h1>
      <form className={classes.form} onSubmit={handleSubmit}>
        <label htmlFor="name">Name:</label>
        <input
          type="text"
          id="name"
          name="name"
          value={createProductValues.name}
          onChange={(event) =>
            setCreateProductValues({
              ...createProductValues,
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
          value={createProductValues.price}
          onChange={(event) =>
            setCreateProductValues({
              ...createProductValues,
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
          value={createProductValues.amount}
          onChange={(event) =>
            setCreateProductValues({
              ...createProductValues,
              amount: event.target.value,
            })
          }
          required
        />

        <label htmlFor="category">Category:</label>
        <select
          id="category"
          name="category"
          value={createProductValues.category}
          onChange={(event) =>
            setCreateProductValues({
              ...createProductValues,
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
          value={createProductValues.description}
          onChange={(event) =>
            setCreateProductValues({
              ...createProductValues,
              description: event.target.value,
            })
          }
        />

        <label htmlFor="image">Image:</label>
        <input
          type="file"
          id="image"
          name="image"
          accept="image/*"
          onChange={handleImageChange}
        />
        <button type="submit">Create Product</button>
      </form>
    </div>
  );
};

export default CreateProduct;
