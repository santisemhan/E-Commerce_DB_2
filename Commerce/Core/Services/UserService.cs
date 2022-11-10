﻿using Cart.Core.DataTransferObjects;
using Commerce.Core.Exceptions;
using Commerce.Core.Repositories.Interfaces;
using Commerce.Core.Services.Interfaces;
using System.Net;

namespace Commerce.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task DeleteUser(string id)
    {
        await GetUserById(id);
        await userRepository.Delete(id);
    }

    public async Task<List<UserDTO>> GetAllUsers()
    {
        return await userRepository.GetAll();
    }

    public async Task<UserDTO> GetUserById(string id)
    {
        var user = await userRepository.GetById(id);
        if (user is null)
        {
            throw new AppException("El usuario no existe", HttpStatusCode.NotFound);
        }
        return user;
    }

    public async Task InsertUser(UserDTO user)
    {
        await userRepository.Insert(user);
    }

    public async Task UpdateUser(UserDTO user, string id)
    {
        await GetUserById(id);
        user.UserId = new MongoDB.Bson.ObjectId(id);
        await userRepository.Update(user);
    }
}
