import React, { useContext } from "react";
import Login from "./components/Login/Login.jsx";
import Register from "./components/Register/Register.jsx";
import { Route, Routes, Navigate } from "react-router-dom";
import Dashboard from "./components/Dashboard/Dashboard.jsx";
import AuthContext from "./store/authContext.jsx";
import Header from "./components/Header/Header.jsx";
import Profile from "./components/Profile/Profile.jsx";
import Products from "./components/Product/Products.jsx";
import Users from "./components/User/Users.jsx";
import Orders from "./components/Order/Orders.jsx";
import CreateProduct from "./components/Product/CreateProduct.jsx"
import UpdateProduct from "./components/Product/UpdateProduct.jsx";
import NewOrder from "./components/Order/NewOrder.jsx";
import Cart from "./components/Cart/Cart.jsx";
import ConfirmPurchase from "./components/ConfirmPurchase/ConfirmPurchase.jsx";
import ProductsMap from "./components/ProductsMap/ProductsMap.jsx";

function App() {
  const context = useContext(AuthContext);
  const token = context.token;
  const role = context.type;

  return (
    <React.Fragment>
      <Header/>
      <Routes>
        <Route
          path="/"
          element={!token ? <Login /> : <Navigate to="/dashboard" />}
        />
        <Route
          path="/register"
          element={!token ? <Register /> : <Navigate to="/dashboard" />}
        />
        <Route
          path="/dashboard"
          element={role ? <Dashboard userType={role} /> : <Navigate to="/" />}
        />
        <Route
          path="/edit-profile"
          element={role ? <Profile /> : <Navigate to="/" />}
        />
        <Route
          path="/users"
          element={role === "Admin" ? <Users/> : <Navigate to="/dashboard"/>}
        />
        <Route
          path="/orders"
          element={role === "Admin" ? <Orders userType="Admin" /> : <Navigate to="/dashboard"/>}
        />        
        <Route
          path="/my-orders"
          element={role === "Seller" ? <Orders userType="Seller" /> : <Navigate to="/dashboard"/>}
        />
        <Route
          path="/products"
          element={role === "Seller" ? <Products userType="Seller"/> : <Navigate to="/dashboard"/>}
        />
        <Route
          path="/new-product"
          element={role === "Seller" ? <CreateProduct/> : <Navigate to="/dashboard"/>}
        />
        <Route
          path="/update-product/:id"
          element={role === "Seller" ? <UpdateProduct/> : <Navigate to="/dashboard"/>}
        />
        <Route
          path="/previous-orders"
          element={role === "Buyer" ? <Orders userType="Buyer" /> : <Navigate to="/dashboard"/>}
        />
        <Route
          path="/new-order"
          element={role === "Buyer" ? <NewOrder/> : <Navigate to="/dashboard"/>}
        />
        <Route
          path="/cart"
          element={role === "Buyer" ? <Cart/> : <Navigate to="/dashboard"/>}
        />
        <Route
          path="/purchase"
          element={role === "Buyer" ? <ConfirmPurchase/> : <Navigate to="/dashboard"/>}
        />
        <Route
          path="packages"
          element={role === "Seller" ? <ProductsMap/> : <Navigate to="/dashboard"/>}/>
      </Routes>
    </React.Fragment>
  );
}

export default App;