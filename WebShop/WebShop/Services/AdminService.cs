using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebShop.DTO;
using WebShop.Enums;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminService> _logger;
        public AdminService(IUnitOfWork unitOfWork, IMailService mailService, IMapper mapper, ILogger<AdminService> logger)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _mapper = mapper;
            this._logger = logger;

        }
        public async Task<List<User>> GetAllVerified(int adminId)
        {
            User? admin = await _unitOfWork.UsersRepository.Get(adminId);
            if (admin == null)
                throw new UnauthorizedException($"Unable to find user with ID: {adminId}.");

            var users = await _unitOfWork.UsersRepository.GetAll();
            List<User> verified = users.Where(u => u.VerificationState == VerificationState.Accepted && u.UserType == UserType.Seller).ToList();
            return verified;
        }

        public async Task<List<User>> GetAllUnverified(int adminId)
        {
            User? admin = await _unitOfWork.UsersRepository.Get(adminId);
            if (admin == null)
                throw new UnauthorizedException($"Unable to find user with ID: {adminId}.");

            var users = await _unitOfWork.UsersRepository.GetAll();
            List<User> verified = users.Where(u => u.VerificationState == VerificationState.Waiting && u.UserType == UserType.Seller).ToList();
            return verified;
        }
        public async Task<List<User>> GetAllDeclined(int adminId)
        {
            User? admin = await _unitOfWork.UsersRepository.Get(adminId);
            if (admin == null)
                throw new UnauthorizedException($"Unable to find user with ID: {adminId}.");

            var users = await _unitOfWork.UsersRepository.GetAll();
            List<User> verified = users.Where(u => u.VerificationState == VerificationState.Denied && u.UserType == UserType.Seller).ToList();
            return verified;
        }

        public async Task<List<User>> GetAllBuyers(int adminId)
        {
            User? admin = await _unitOfWork.UsersRepository.Get(adminId);
            if (admin == null)
                throw new UnauthorizedException($"Unable to find user with ID: {adminId}.");

            var users = await _unitOfWork.UsersRepository.GetAll();
            List<User> verified = users.Where(u=> u.UserType == UserType.Buyer).ToList();
            return verified;
        }
        public async Task VerifyUser(UserVerifyDTO userVerifyDTO, int adminId)
        {
            User? admin = await _unitOfWork.UsersRepository.Get(adminId);
            if (admin == null)
                throw new UnauthorizedException($"Unable to find user with ID: {adminId}.");

            User? user = await _unitOfWork.UsersRepository.Get(userVerifyDTO.Id);
            if (user == null)
            {
                _logger.LogError($"[VerifyUser] [User: {admin.Email}] - Attempted to verify non-existing user.");
                throw new BadRequestException("Error occured with ID. Please try again.");
            }              

            if (user.VerificationState != VerificationState.Waiting)
            {
                _logger.LogError($"[VerifyUser] [User: {admin.Email}] - Attempted to verify already accepted/declined user.");
                throw new BadRequestException("You can only verify waiting users");
            }                

            user.VerificationState = userVerifyDTO.verificationState;

            string message = "";
            message = user.VerificationState == VerificationState.Accepted ? "Your account is approved. You can now start selling." : "Your account declined. Contact us for more information.";
            
            _ = Task.Run(async () => await _mailService.SendEmail("Verification", message, user.Email));
           
            _unitOfWork.UsersRepository.Update(user);
            await _unitOfWork.Save();
        }

        public async Task<List<OrderDTO>> GetAllOrders(int adminId)
        {
            User? admin = await _unitOfWork.UsersRepository.Get(adminId);
            if (admin == null)
                throw new UnauthorizedException($"Unable to find user with ID: {adminId}.");

            var orders = await _unitOfWork.OrdersRepository.GetAll();
            var includedOrders = orders.Include(x => x.Items).ToList();
            return _mapper.Map<List<OrderDTO>>(includedOrders); ;
        }
    }
}
