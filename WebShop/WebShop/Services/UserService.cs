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

        public async Task<UserProfileDTO> GetUserProfile(int id)
        {
            User? user = await _unitOfWork.UsersRepository.Get(id);
            if (user == null)
                throw new UnauthorizedException($"Unable to find user with ID: {id}.");

            return _mapper.Map<UserProfileDTO>(user);
        }

        public async Task EditUserProfile(int id, EditUserDTO editUserDTO)
        {
            var users = await _unitOfWork.UsersRepository.GetAll();
            User? user = await _unitOfWork.UsersRepository.Get(id);
            if (user == null)
                throw new UnauthorizedException($"Unable to find user with ID: {id}.");

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

            _unitOfWork.UsersRepository.Update(user);
            await _unitOfWork.Save();
        }

        public async Task AddPicture(int id, IFormFile image)
        {
            User? user = await _unitOfWork.UsersRepository.Get(id);
            if (user == null)
                throw new BadRequestException($"There was and error with user ID: {id}");

            using(var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                var imageBytes = ms.ToArray();

                user.Image = imageBytes;
                _unitOfWork.UsersRepository.Update(user);
            }

            await _unitOfWork.Save();
        }

        public async Task<byte[]> GetPicture(int id)
        {
            User? user = await _unitOfWork.UsersRepository.Get(id);
            if (user == null)
                throw new BadRequestException($"There was and error with user ID: {id}");
            if (user.Image == null)
                throw new NotFoundException($"User with ID: {id} doesn't have image");

            return user.Image;
        }
    }
}