using System.Net.Mime;
using Backend.Dal.Entities;
using Backend.Exceptions;
using Backend.Extensions;
using Backend.Models;
using Backend.Models.Auth;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

    private IUserService UserService { get; }
    private ILogger<UserController> Logger { get; }

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

    [HttpGet]
    [Route("getUser")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<User>> GetUser()
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized("Couldn't authenticate user");
        try
        {
            var user = await UserService.GetUserByIdAsync(userId);
            return Ok(user);
        }
        catch (UserNotFoundException)
        {
            return Unauthorized("Couldn't authenticate user");
        }
        catch (Exception e)
        {
            Logger.LogError("{}",e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }

    [HttpDelete]
    [Route("deleteUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteUser([FromQuery] string userId)
    {
        try
        {
            await UserService.DeleteUserAsync(userId);
            return Ok();
        }
        catch (UserNotFoundException)
        {
            return NotFound("User not found");
        }
        catch (Exception e)
        {
            Logger.LogError("{}",e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    [Route("updateUser")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<User>> UpdateUser(UserChangeRequest user)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        try
        {
            var result = await UserService.UpdateUserAsync(userId, user);
            return Ok(result);
        }
        catch (UserNotFoundException)
        {
            return Unauthorized();
        }
        catch (Exception e)
        {
            Logger.LogError("{}",e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}