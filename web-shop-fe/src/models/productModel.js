class ProductModel {
  constructor(product) {
    this.id = product.id;
    this.name = product.name;
    this.price = product.price;
    this.amount = product.amount;
    this.description = product.description;
    this.image = product.image;
    this.imageFile = product.imageFile;
    this.sellerId = product.sellerId;
    this.category = product.category;
  }
}

export default ProductModel;