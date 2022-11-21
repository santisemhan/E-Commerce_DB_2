﻿using Commerce.Core.DataTransferObjects;
using Commerce.Core.DataTransferObjects.Request;
using Commerce.Core.Models;
using Commerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await userService.GetAllUsers());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        return Ok(await userService.GetUserById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
    {
        if (user == null) { return BadRequest(); }

        await userService.InsertUser(user);

        return Created("Created", true);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        if (user == null) { return BadRequest(); }

        await userService.UpdateUser(user);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        await userService.DeleteUser(id);
        return NoContent();
    }
}
