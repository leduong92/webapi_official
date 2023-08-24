
using Core.DTO.BaseDto;
using Core.DTO.RequestDto;
using Core.DTO.ResponseDto;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController : BaseApiController
{
    public static User user = new User();
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public AuthController(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<string> GetMe()
    {
        var username = _userService.GetMyName();
        return Ok(username);
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserRequestDto request)
    {
        _userService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        user.Username = request.Username;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;


        return Ok(user);
    }
    [HttpPost("reset")]
    public async Task<ActionResult<User>> Reset(UserRequestDto request)
    {
        _userService.CreatePasswordHash("123", out byte[] passwordHash, out byte[] passwordSalt);

        user.Username = request.Username;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;


        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponsetDto>> Login(UserRequestDto request)
    {
        if (user.Username != request.Username)
        {
            return BadRequest("User not found");
        }
        if (!_userService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Wrong password");
        }

        string token = _userService.CreateToken(user);

        var rsp = new UserResponsetDto()
        {
            Username = request.Username,
            Password = request.Password,
            Token = token
        };
        var refreshToken = _userService.GenerateRefreshToken();
        SetRefreshToken(refreshToken);

        return Ok(rsp);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<string>> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (!user.RefreshToken.Equals(refreshToken))
        {
            return Unauthorized("Invalid Refresh Token.");
        }
        else if (user.TokenExpires < DateTime.Now)
        {
            return Unauthorized("Token expired.");
        }

        string token = _userService.CreateToken(user);
        var newRefreshToken = _userService.GenerateRefreshToken();
        SetRefreshToken(newRefreshToken);

        return Ok(token);
    }
    private void SetRefreshToken(RefreshToken newRefreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.Expires
        };
        Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

        user.RefreshToken = newRefreshToken.Token;
        user.TokenCreated = newRefreshToken.Created;
        user.TokenExpires = newRefreshToken.Expires;
    }

}