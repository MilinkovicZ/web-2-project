﻿using WebShop.DTO;
using WebShop.Enums;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<User>> GetAllVerified()
        {
            var users = await _unitOfWork.UsersRepository.GetAllAsync();
            List<User> verified = users.Where(u => u.VerificationState == VerificationState.Accepted && u.UserType == UserType.Seller).ToList();
            return verified;
        }
        public async Task<List<User>> GetAllUnverified()
        {
            var users = await _unitOfWork.UsersRepository.GetAllAsync();
            List<User> verified = users.Where(u => u.VerificationState == VerificationState.Waiting && u.UserType == UserType.Seller).ToList();
            return verified;
        }
        public async Task VerifyUser(UserVerifyDTO userVerifyDTO)
        {
            User? user = await _unitOfWork.UsersRepository.GetAsync(userVerifyDTO.Id);
            if (user == null)
                throw new BadRequestException("Error occured with ID. Please try again.");

            user.VerificationState = userVerifyDTO.verificationState;
            _unitOfWork.UsersRepository.Update(user);
            await _unitOfWork.Save();
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return (await _unitOfWork.OrdersRepository.GetAllAsync()).ToList();
        }
    }
}
