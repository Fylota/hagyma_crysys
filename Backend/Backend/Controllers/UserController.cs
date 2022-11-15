using System.Net.Mime;
using Backend.Extensions;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private IUserService UserService { get; }

    public UserController(IUserService userService)
    {
        UserService = userService;
    }

    [HttpGet]
    [Route("getUser")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<User>> GetUser()
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized("Couldn't authenticate user");
        var user = await UserService.GetUserByIdAsync(userId);
        if (user == null) return Unauthorized("Couldn't authenticate user");
        return Ok(user);
    }

    [HttpGet]
    [Route("getUsers")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        return await UserService.GetUsersAsync();
    }

    [HttpPut]
    [Route("updateUser")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UpdateUser(User user)
    {
        return await Task.FromResult(Ok());
    }

    [HttpDelete]
    [Route("deleteUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteUser([FromQuery] string userId)
    {
        return await Task.FromResult(Ok());
    }
}