class ProfileModel {
  constructor(profile) {
    this.username = profile.username;
    this.email = profile.email;
    this.fullName = profile.fullName;
    this.birthDate = profile.birthDate;
    this.address = profile.address;
    this.image = profile.image;
  }
}

export default ProfileModel;