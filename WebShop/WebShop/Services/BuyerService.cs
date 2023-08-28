using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using WebShop.DTO;
using WebShop.Enums;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Services
{
    public class BuyerService : IBuyerService
    {
        private double DeliveryFee {get; set;} = 2.99;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BuyerService> _logger;
        public BuyerService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BuyerService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task CreateOrder(CreateOrderDTO orderDTO, int buyerId)
        {
            User? buyer = await _unitOfWork.UsersRepository.Get(buyerId);
            if (buyer == null)
                throw new UnauthorizedException($"Unable to find user with ID: {buyerId}.");

            var orders = await _unitOfWork.OrdersRepository.GetAll();
            var buyerOrders = orders.Where(x => x.BuyerId == buyerId && x.OrderState == OrderState.Preparing).ToList();

            if (buyerOrders.Count > 0 && !buyer.HasFullAccess)
            {
                _logger.LogError($"[CreateOrder] [User: {buyer.Email}] - User without FullAccess trying to purchase another order.");
                throw new BadRequestException("You can have only 1 order at time.");
            }

            var order = _mapper.Map<Order>(orderDTO);
            order.BuyerId = buyerId;
            order.OrderState = OrderState.Preparing;

            double totalPrice = 0;
            var sellerIds = new List<int>();

            foreach (var item in order.Items)
            {
                Product? product = await _unitOfWork.ProductsRepository.Get(item.ProductId);
                if (product == null)
                {
                    _logger.LogError($"[CreateOrder] [User: {buyer.Email}] - Attempted to buy non-existing product.");
                    throw new NotFoundException("Product is non existent.");
                }

                if (item.ProductAmount > product.Amount)
                {
                    _logger.LogError($"[CreateOrder] [User: {buyer.Email}] - Attempted to buy {item.ProductAmount} while only {product.Amount} in stock.");
                    throw new BadRequestException($"Currently there is only {product.Amount} {product.Name}s in stock.");
                }

                if (item.ProductAmount < 0)
                {
                    _logger.LogError($"[CreateOrder] [User: {buyer.Email}] - Attempted to buy negative number of product.");
                    throw new BadRequestException("Can't buy negative number of products.");
                }

                if (buyer.BirthDate.AddYears(18) > DateTime.Now && (product.Category == ProductCategory.Alcohol || product.Category == ProductCategory.Cigarettes))
                {
                    _logger.LogError($"[CreateOrder] [User: {buyer.Email}] - Underage attempted to buy alcohol/cigarettes.");
                    throw new BadRequestException("You can't buy alcohol/cigarettes until you turn 18.");
                }


                product.Amount -= item.ProductAmount;
                item.Name = product.Name;
                item.CurrentPrice = product.Price;

                totalPrice += item.ProductAmount * item.CurrentPrice;
                if (!sellerIds.Contains(product.SellerId)) 
                    sellerIds.Add(product.SellerId);

                _unitOfWork.ProductsRepository.Update(product);
            }

            totalPrice += sellerIds.Count() * DeliveryFee;
            order.TotalPrice = totalPrice;


            if (!buyer.HasFullAccess && totalPrice >= 500)
            {
                _logger.LogError($"[CreateOrder] [User: {buyer.Email}] - User without FullAccess trying to purchase order bigger then 500$.");
                throw new BadRequestException("You can't purchase order bigger then 499.99$.");
            }

            await _unitOfWork.OrdersRepository.Insert(order);
            await _unitOfWork.Save();
        }

        public async Task DeclineOrder(int orderId, int buyerId)
        {
            User? buyer = await _unitOfWork.UsersRepository.Get(buyerId);
            if (buyer == null)
                throw new UnauthorizedException($"Unable to find user with ID: {buyerId}.");

            var orders = await _unitOfWork.OrdersRepository.GetAll();
            var includedOrder = orders.Include(x => x.Items);
            var myOrder = includedOrder.FirstOrDefault(x => x.Id == orderId);
            if (myOrder == null)
            {
                _logger.LogError($"[DeclineOrder] [User: {buyer.Email}] - Attempted to decline invalid order #{orderId}");
                throw new NotFoundException("Order doesn't exist within this user.");
            }
            if (myOrder.OrderState == OrderState.Delievered)
            {
                _logger.LogError($"[DeclineOrder] [User: {buyer.Email}] - Attempted to decline delivered order #{orderId}");
                throw new BadRequestException("Can't cancel already delivered order.");
            }
            if (myOrder.StartTime.AddHours(1) < DateTime.Now)
            {
                _logger.LogError($"[DeclineOrder] [User: {buyer.Email}] - Attempted to decline late order #{orderId}");
                throw new BadRequestException("1 hour already passed, you can't cancel your order.");
            }                

            myOrder.OrderState = OrderState.Canceled;

            foreach (var item in myOrder.Items)
            {
                Product? product = await _unitOfWork.ProductsRepository.Get(item.ProductId);
                if (product == null)
                {
                    _logger.LogError($"[CreateOrder] [User: {buyer.Email}] - Attempted to buy non-existing product.");
                    throw new NotFoundException("Product is non existent.");
                }

                product.Amount += item.ProductAmount;
                _unitOfWork.ProductsRepository.Update(product);
            }

            _unitOfWork.OrdersRepository.Update(myOrder);
            await _unitOfWork.Save();
        }
        public async Task<List<OrderDTO>> GetMyOrders(int buyerId)
        {
            User? buyer = await _unitOfWork.UsersRepository.Get(buyerId);
            if (buyer == null)
                throw new UnauthorizedException($"Unable to find user with ID: {buyerId}.");

            var orders = await _unitOfWork.OrdersRepository.GetAll();
            var includedOrders = orders.Include(x => x.Items).ToList();
            var buyerOrders = includedOrders.Where(x => x.BuyerId == buyerId && (x.OrderState == OrderState.Preparing || x.OrderState == OrderState.Delievered)).ToList();
            
            return _mapper.Map<List<OrderDTO>>(buyerOrders);
        }

        public async Task<List<ProductDTO>> GetAllProducts(int buyerId)
        {
            User? buyer = await _unitOfWork.UsersRepository.Get(buyerId);
            if (buyer == null)
                throw new UnauthorizedException($"Unable to find user with ID: {buyerId}.");

            var products = await _unitOfWork.ProductsRepository.GetAll();
            List<Product> availableProduct = products.Where(x => x.Amount > 0).ToList();
            return _mapper.Map<List<ProductDTO>>(availableProduct);
        }

        public async Task<double> GetTotalPrice(List<CreateItemDTO> items, int buyerId)
        {
            User? buyer = await _unitOfWork.UsersRepository.Get(buyerId);
            if (buyer == null)
                throw new UnauthorizedException($"Unable to find user with ID: {buyerId}.");

            double totalPrice = 0;
            var sellerIds = new List<int>();

            foreach (var item in items)
            {
                Product? product = await _unitOfWork.ProductsRepository.Get(item.ProductId);
                if (product == null)
                {
                    _logger.LogError($"[CreateOrder] [User: {buyer.Email}] - Attempted to buy non-existing product.");
                    throw new NotFoundException("Product is non existent.");
                }

                if (item.ProductAmount > product.Amount)
                {
                    _logger.LogError($"[CreateOrder] [User: {buyer.Email}] - Attempted to buy {item.ProductAmount} while only {product.Amount} in stock.");
                    throw new BadRequestException($"Currently there is only {product.Amount} {product.Name}s in stock.");
                }

                if (item.ProductAmount < 0)
                {
                    _logger.LogError($"[CreateOrder] [User: {buyer.Email}] - Attempted to buy negative number of product.");
                    throw new BadRequestException("Can't buy negative number of products.");
                }

                totalPrice += item.ProductAmount * product.Price;
                if (!sellerIds.Contains(product.SellerId))
                    sellerIds.Add(product.SellerId);
            }

            totalPrice += sellerIds.Count() * DeliveryFee;
            return totalPrice;
        }
    }
}
