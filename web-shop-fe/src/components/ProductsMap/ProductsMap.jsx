import React from "react";
import { useState, useEffect } from "react";
import classes from "./ProductsMap.module.css";
import { MapContainer, Marker, TileLayer, Popup } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import sellerService from "../../services/sellerService";
import L from "leaflet";
import Geocode from "react-geocode";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";

const ProductsMap = () => {
  Geocode.setApiKey(process.env.REACT_APP_GEOCODE_KEY);
  Geocode.setLanguage("en");
  Geocode.setRegion("rs");
  Geocode.setLocationType("ROOFTOP");
  Geocode.enableDebug();

  const position = [46.104586472419314, 19.664730942252326];
  const [markersPositions, setMarkersPositions] = useState([]);
  const [orders, setOrders] = useState([]);
  const navigator = useNavigate();

  const markerIcon = new L.Icon({
    iconUrl: "package.png",
    iconSize: [40, 40],
    popupAnchor: [0, -15],
  });

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    const newOrders = await sellerService.getNewOrders();
    setOrders(newOrders);

    const markersData = [];
    for (const o of newOrders) {
      try {
        const response = await Geocode.fromAddress(o.deliveryAddress);
        const { lat, lng } = response.results[0].geometry.location;
        markersData.push({ id: o.id, lat: lat, lon: lng, price:o.totalPrice.toFixed(2), address:o.deliveryAddress});
      } catch (error) {
        console.log(error);
      }
    }

    setMarkersPositions(markersData);
  };

  const handleAcceptOrder = async (orderId) => {
    try {
      await sellerService.acceptOrder(orderId);
      navigator("/my-orders");
    } catch (error) {
      if (error.response) {
        alert(error.response.data.Exception);
      }
    }
  };

  return (
    <MapContainer
      center={position}
      zoom={10}
      style={{ width: "100vw", height: "100vh" }}
    >
      <TileLayer url={process.env.REACT_APP_URL_KEY} />
      {markersPositions.map((marker) => (
        <div key={marker.id}>
          <Marker position={[marker.lat, marker.lon]} icon={markerIcon}>
            <Popup>
              <Link to="/my-orders" className={classes.orderNumber}>Order number: {marker.id}</Link>              
              <p className={classes.address}>Address: {marker.address}</p>
              <p className={classes.price}>Price: ${marker.price}</p>
              {!orders.find((order) => order.id === marker.id).isAccepted && (
                <div className={classes.container}>
                  <button
                    className={classes.acceptButton}
                    onClick={() => handleAcceptOrder(marker.id)}
                  >
                    Accept
                  </button>
                </div>
              )}
            </Popup>
          </Marker>
        </div>
      ))}
    </MapContainer>
  );
};

export default ProductsMap;
