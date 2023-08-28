import ItemModel from "./itemModel"

class OrderModel {
  constructor(order) {
    this.id = order.id;
    this.deliveryAddress = order.deliveryAddress;
    this.comment = order.comment;
    this.startTime = order.startTime;
    this.deliveryTime = order.deliveryTime;
    this.totalPrice = order.totalPrice;
    this.orderState = order.orderState;
    this.isAccepted = order.isAccepted;
    this.items = order.items.map((i) => new ItemModel(i));
  }
}

export default OrderModel;