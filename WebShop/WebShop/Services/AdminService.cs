﻿using AutoMapper;
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
        public AdminService(IUnitOfWork unitOfWork, IMailService mailService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _mapper = mapper;

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
        public async Task VerifyUser(UserVerifyDTO userVerifyDTO, int adminId)
        {
            User? admin = await _unitOfWork.UsersRepository.Get(adminId);
            if (admin == null)
                throw new UnauthorizedException($"Unable to find user with ID: {adminId}.");

            User? user = await _unitOfWork.UsersRepository.Get(userVerifyDTO.Id);
            if (user == null)
                throw new BadRequestException("Error occured with ID. Please try again.");

            user.VerificationState = userVerifyDTO.verificationState;

            if (user.VerificationState == VerificationState.Accepted)
                await _mailService.SendEmail("Verification", "Your account is approved. You can now start selling.", user.Email);
            else
                await _mailService.SendEmail("Verification", "Your account declined. Contact us for more information.", user.Email);

            _unitOfWork.UsersRepository.Update(user);
            await _unitOfWork.Save();
        }

        public async Task<List<OrderDTO>> GetAllOrders(int adminId)
        {
            User? admin = await _unitOfWork.UsersRepository.Get(adminId);
            if (admin == null)
                throw new UnauthorizedException($"Unable to find user with ID: {adminId}.");

            var orders = await _unitOfWork.OrdersRepository.GetAll();
            var includedOrders = orders.Include(x => x.Items).ThenInclude(x => x.Product).ToList();
            return _mapper.Map<List<OrderDTO>>(includedOrders); ;
        }
    }
}
