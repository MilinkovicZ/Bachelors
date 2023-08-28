import React, { useEffect, useState } from "react";
import adminService from "../../services/adminService";
import classes from "./Users.module.css";
import User from "./User";

const Users = () => {
  const [verifiedUsers, setVerifiedUsers] = useState([]);
  const [unverifiedUsers, setUnverifiedUsers] = useState([]);
  const [declinedUsers, setDeclinedUsers] = useState([]);
  const [buyerUsers, setBuyerUsers] = useState([]);

  useEffect(() => {
    fetchUsers();
    fetchBuyers();
  }, []);

  const fetchBuyers = async () => {
    try {
      const allbuyerUsers = await adminService.getBuyers();
      setBuyerUsers(allbuyerUsers);
    } catch (error) {
      if (error.response) alert(error.response.data.Exception);
    }
  };

  const fetchUsers = async () => {
    try {
      const allVerified = await adminService.getVerified();
      const allUnverified = await adminService.getUnverified();
      const alldeclinedUsers = await adminService.getDeclined();
      setVerifiedUsers(allVerified);
      setUnverifiedUsers(allUnverified);
      setDeclinedUsers(alldeclinedUsers);
    } catch (error) {
      if (error.response) alert(error.response.data.Exception);
    }
  };

  const acceptUserHandler = async (userId) => {
    await verifyUserHandler({ id: userId, verificationState: 1 });
  };

  const declineUserHandler = async (userId) => {
    await verifyUserHandler({ id: userId, verificationState: 2 });
  };

  const verifyUserHandler = async (data) => {
    try {
      await adminService.verifyUser(data);
      fetchUsers();
    } catch (error) {
      if (error.response) alert(error.response.data.Exception);
    }
  };

  return (
    <div className={classes.usersContainer}>
      <div className={classes.column}>
        <h1 className={classes.title}>Verified Users</h1>
        {verifiedUsers.length === 0 ? (
          <h1>There are no verified users.</h1>
        ) : (
          verifiedUsers.map((user) => (
            <div className={classes.userContainer} key={user.id}>
              <User user={user} />
            </div>
          ))
        )}
      </div>
      <div className={classes.column}>
        <h1 className={classes.title}>Unverified Users</h1>
        {unverifiedUsers.length === 0 ? (
          <h1>There are no unverified users.</h1>
        ) : (
          unverifiedUsers.map((user) => (
            <div className={classes.userContainer} key={user.id}>
              <User
                user={user}
                onAccept={() => acceptUserHandler(user.id)}
                onDecline={() => declineUserHandler(user.id)}
              />
            </div>
          ))
        )}
      </div>
      <div className={classes.column}>
        <h1 className={classes.title}>Declined Users</h1>
        {declinedUsers.length === 0 ? (
          <h1>There are no declined users</h1>
        ) : (
          declinedUsers.map((user) => (
            <div className={classes.userContainer} key={user.id}>
              <User user={user} />
            </div>
          ))
        )}
      </div>
      <div className={classes.column}>
        <h1 className={classes.title}>Buyers</h1>
        {buyerUsers.length === 0 ? (
          <h1>There are no buyer users</h1>
        ) : (
          buyerUsers.map((user) => (
            <div className={classes.userContainer} key={user.id}>
              <User user={user} />
            </div>
          ))
        )}
      </div>
    </div>
  );
};

export default Users;
