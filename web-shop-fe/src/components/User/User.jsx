import React from 'react';
import classes from './User.module.css';

const User = ({ user, onAccept, onDecline }) => {
  user.birthDate = user.birthDate.split("T")[0];

  return (
    <table className={classes.userTable}>
      <tbody>
        <tr>
          <td className={classes.label}>Username:</td>
          <td>{user.username}</td>
        </tr>
        <tr>
          <td className={classes.label}>Email:</td>
          <td>{user.email}</td>
        </tr>
        <tr>
          <td className={classes.label}>Full Name:</td>
          <td>{user.fullName}</td>
        </tr>
        <tr>
          <td className={classes.label}>Birth Date:</td>
          <td>{user.birthDate}</td>
        </tr>
        <tr>
          <td className={classes.label}>Address:</td>
          <td>{user.address}</td>
        </tr>
        <tr>
          <td className={classes.label}>ID:</td>
          <td>{user.id}</td>         
        </tr>
        {user.verificationState === 0 && (
          <tr>
            <td></td>
            <td>
              <button className={classes.verifyButton} onClick={() => onAccept(user.id)}>Verify this user</button>
              <button className={classes.declineButton} onClick={() => onDecline(user.id)}>Decline this user</button>
            </td>
          </tr>
        )}
      </tbody>
    </table>
  );
};

export default User;