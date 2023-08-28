class UserModel {
  constructor(user) {
    this.id = user.id;
    this.username = user.username;
    this.email = user.email;
    this.fullName = user.fullName;
    this.birthDate = user.birthDate;
    this.address = user.address;
    this.verificationState = user.verificationState;
  }
}

export default UserModel;