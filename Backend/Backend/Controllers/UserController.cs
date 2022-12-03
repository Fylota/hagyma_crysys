using System.Net.Mime;
using Backend.Exceptions;
using Backend.Extensions;
using Backend.Models;
using Backend.Models.Auth;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        UserService = userService;
        Logger = logger;
    }

    private ILogger<UserController> Logger { get; }

    private IUserService UserService { get; }

    [HttpGet]
    [Route("getUsers")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        Logger.LogInformation($"user with id: {User.GetUserId()} requested all users information.");
        return await UserService.GetUsersAsync();
    }

    [HttpGet]
    [Route("getUser")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<User>> GetUser()
    {
        var userId = User.GetUserId();
        if (userId == null) {
            Logger.LogInformation($"Unauthorized user requested user information.");
            return Unauthorized("Couldn't authenticate user"); 
        }
        try
        {
            var user = await UserService.GetUserByIdAsync(userId);
            Logger.LogInformation($"user with id: {User.GetUserId()} requested user information.");
            return Ok(user);
        }
        catch (UserNotFoundException)
        {
            Logger.LogInformation($"Unauthorized user requested user information.");
            return Unauthorized("Couldn't authenticate user");
        }
        catch (Exception e)
        {
            Logger.LogError("{}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    [Route("updateUser")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<User>> UpdateUser(UserChangeRequest user)
    {
        var userId = User.GetUserId();
        if (userId == null) {
            Logger.LogInformation($"Unauthorized user tried to update user information.");
            return Unauthorized(); 
        }
        try
        {
            var result = await UserService.UpdateUserAsync(userId, user);
            Logger.LogInformation($"user with id: {User.GetUserId()} updated user information.");
            return Ok(result);
        }
        catch (UserNotFoundException)
        {
            Logger.LogInformation($"Unauthorized user tried to update user information.");
            return Unauthorized();
        }
        catch (Exception e)
        {
            Logger.LogError("{}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete]
    [Route("deleteUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult> DeleteUser([FromQuery] string userId)
    {
        if (!User.IsInRole(AuthRoles.Admin.ToString()) && User.GetUserId() != userId) {
            Logger.LogInformation($"user with id: {User.GetUserId()} tried to delete user with id: {userId}.");
            return Unauthorized();
        }
        try
        {
            await UserService.DeleteUserAsync(userId);
            Logger.LogInformation($"user with id: {User.GetUserId()} deleted user with id: {userId}.");
            return Ok();
        }
        catch (UserNotFoundException)
        {
            Logger.LogInformation($"user with id: {User.GetUserId()} tried to delete user with id: {userId}.");
            return NotFound("User not found");
        }
        catch (Exception e)
        {
            Logger.LogError("{}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}