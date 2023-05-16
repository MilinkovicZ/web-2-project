﻿using AutoMapper;
using WebShop.DTO;
using WebShop.Exceptions;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProfileDTO> GetUserProfile(int id)
        {
            User? user = await _unitOfWork.UsersRepository.Get(id);
            if (user == null)
                throw new BadRequestException("Error occured with ID. Please try again.");

            return _mapper.Map<ProfileDTO>(user);
        }

        public async Task EditUserProfile(int id, EditUserDTO editUserDTO)
        {
            var users = await _unitOfWork.UsersRepository.GetAll();
            User? user = await _unitOfWork.UsersRepository.Get(id);
            if (user == null)
                throw new BadRequestException("Error occured with ID. Please try again.");

            if (!BCrypt.Net.BCrypt.Verify(editUserDTO.Password, user.Password))
                throw new BadRequestException("You must enter correct current password in order to save changes.");

            if (user.Username != editUserDTO.Username)
            {
                var existingUsername = users.FirstOrDefault(u => u.Username == editUserDTO.Username);
                if (existingUsername != null)
                    throw new BadRequestException("This username is already taken!");
                user.Username = editUserDTO.Username;
            }

            if (user.Email != editUserDTO.Email)
            {
                var existingEmail = users.FirstOrDefault(u => u.Email == editUserDTO.Email);
                if (existingEmail != null)
                    throw new BadRequestException("This email is already taken!");
                user.Email = editUserDTO.Email;
            }

            if (!string.IsNullOrWhiteSpace(editUserDTO.NewPassword))
                user.Password = BCrypt.Net.BCrypt.HashPassword(editUserDTO.NewPassword);

            if (user.FullName != editUserDTO.FullName)
                user.FullName = editUserDTO.FullName;
            if (user.Address != editUserDTO.Address)
                user.Address = editUserDTO.Address;
            if (user.BirthDate != editUserDTO.BirthDate)
                user.BirthDate = editUserDTO.BirthDate;
            user.Image = editUserDTO.Image;

            _unitOfWork.UsersRepository.Update(user);
            await _unitOfWork.Save();
        }
    }
}