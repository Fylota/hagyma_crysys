using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using Backend.Dal.Entities;
using Backend.Exceptions;
using Backend.Helpers;
using Backend.Models.Auth;
using Backend.Services.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers;

[Route("auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    public AuthenticationController(IConfiguration config, SignInManager<DbUserInfo> signInManager,
        UserManager<DbUserInfo> userManager, IUserService userService, ILogger<AuthenticationController> logger)
    {
        Config = config;
        SignInManager = signInManager;
        UserManager = userManager;
        UserService = userService;
        Logger = logger;
    }

    private IConfiguration Config { get; }
    private ILogger<AuthenticationController> Logger { get; }
    private IUserService UserService { get; }
    private SignInManager<DbUserInfo> SignInManager { get; }
    private UserManager<DbUserInfo> UserManager { get; }


    [HttpPost]
    [Route("login")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Login([FromBody] LoginRequest loginRequest)
    {
        try
        {
            var user = await UserService.GetUserByEmailAsync(loginRequest.Email);
            var loginResult = await SignInManager.PasswordSignInAsync(user, loginRequest.Password, false, false);
            if (!loginResult.Succeeded)
                return BadRequest("Email or password is incorrect");
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, Config["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToEpochTime().ToString()),
                new Claim(Constants.UserId, user.Id),
                new Claim(Constants.RegistrationDate, user.RegistrationDate.ToEpochTime().ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                Config["Jwt:Issuer"],
                Config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signIn);
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
        catch (UserNotFoundException)
        {
            return Unauthorized();
        }
        catch (Exception e)
        {
            Logger.LogError("{}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [Route("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        //Useless
        await SignInManager.SignOutAsync();
        return Ok();
    }

    [HttpPost]
    [Route("register")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<ActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        var registerResult =
            await UserManager.CreateAsync(
                new DbUserInfo {Email = registerRequest.Email, UserName = registerRequest.Username, RegistrationDate = DateTime.Now},
                registerRequest.Password);
        if (registerResult.Succeeded) return Ok();
        return BadRequest(registerResult.Errors);
    }
}