using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using Backend.Dal;
using Backend.Dal.Entities;
using Backend.Helpers;
using Backend.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers;

[Route("auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    public AuthenticationController(IConfiguration config, SignInManager<DbUserInfo> signInManager, AppDbContext context)
    {
        Config = config;
        SignInManager = signInManager;
        Context = context;
    }

    private AppDbContext Context { get; }
    private IConfiguration Config { get; }
    private SignInManager<DbUserInfo> SignInManager { get; }


    [HttpPost]
    [Route("login")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthUser))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Login([FromBody] LoginRequest loginRequest)
    {
        //TODO Login
        var user = await Context.Users.SingleOrDefaultAsync(u => u.Email == loginRequest.Email);
        if (user == null) return BadRequest("Email or password is incorrect");
        var loginResult = await SignInManager.PasswordSignInAsync(user, loginRequest.Password, false, false);
        if (!loginResult.Succeeded)
            return BadRequest("Email or password is incorrect");
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, Config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim(Constants.UserId, user.Id),
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

    [HttpPost]
    [Route("register")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task Register([FromBody] RegisterRequest registerRequest)
    {
        //TODO Register
        await Task.CompletedTask;
    }

    [HttpGet]
    [Route("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task Logout()
    {
        //TODO Logout
        await Task.CompletedTask;
    }
}